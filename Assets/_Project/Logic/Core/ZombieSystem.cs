using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static System.Threading.Tasks.Task;
using static System.TimeSpan;
using static _Project.Logic.Core.WaveType;
using static UnityEngine.Mathf;
using static UnityEngine.Random;
using Random = UnityEngine.Random;

namespace _Project.Logic.Core
{
    public class ZombieSystem : IDisposable
    {
        private const float PERCENT_ZOMBIES_IN_WAVES = 0.6f;
        
        private ZombiePool _zombiePool;
        private ZombieRepository _zombieRepository;
        private IReadOnlyList<GameObject> _spawnPoints;
        private Transform _parent;
        private (string ZombieId, int Count)[] _zombiesInLevel;
        private WaveCounter _waveCounter;
        private CompositeDisposable _disposables = new();
        
        public ZombieSystem(ZombieRepository zombieRepository, ZombieSpawnRepository zombieSpawnRepository, 
            ZombiesContainer zombiesContainer, WaveCounter waveCounter)
        {
            _zombieRepository = zombieRepository;
            _spawnPoints = zombieSpawnRepository.SpawnPoints;
            _parent = zombieSpawnRepository.Parent;
            _waveCounter = waveCounter;

            int length = zombiesContainer.ZombieConfigs.Length;
            _zombiesInLevel = new (string ZombieId, int Count)[length];
            for (int i = 0; i < length; i++)
            {
                _zombiesInLevel[i] = (zombiesContainer.ZombieConfigs[i].ZombieId,
                    zombiesContainer.ZombieConfigs[i].Count);
            }

            _zombiePool = new(_zombiesInLevel);
        }

        public void Prepare()
        {
            _zombiePool.Prepare();
            
            for (int i = 0; i < _zombiesInLevel.Length; i++)
            {
                //как
                for (int j = 0; j < 2; j++)
                {
                    Zombie zombie = _zombiePool.Get(i);
                    
                    zombie.transform.SetParent(_parent, false);
                    zombie.transform.localPosition = new(Range(-100, 100), Range(-400, 400));
                }
            }

            int zombiesSum = 0;
            foreach ((string _, int count) in _zombiesInLevel)
                zombiesSum += count;
            
            float countZombiesInWaves = zombiesSum * PERCENT_ZOMBIES_IN_WAVES;
            float countZombiesInDefault = zombiesSum * (1 - PERCENT_ZOMBIES_IN_WAVES);
            int zombiesInWave = Mathf.RoundToInt(countZombiesInWaves / _waveCounter.WaveCount);
            int zombiesInDefault = Mathf.RoundToInt(countZombiesInDefault / _waveCounter.WaveCount);
            
            _waveCounter.OnSpawn
                .Skip(1)
                .Where(waveType => waveType == Default)
                .Subscribe(_ => CreateZombies(zombiesInDefault))
                .AddTo(_disposables);
            
            _waveCounter.OnSpawn
                .Skip(1)
                .Where(waveType => waveType == Wave)
                .Subscribe(_ => CreateZombies(zombiesInWave))
                .AddTo(_disposables);
        }

        private async void CreateZombies(int countForSpawn)
        {
            int typeZombie = 0;
            int countZombies = 0;
            for (int i = 0; i < countForSpawn; i++)
            {
                for (int j = 0;  j < _zombiesInLevel.Length; j++)
                    if (countZombies <= _zombiesInLevel[j].Count)
                    {
                        typeZombie = j;
                        countZombies = _zombiesInLevel[j].Count;
                    }
                
                Zombie zombie = _zombiePool.Get(typeZombie);
                SetupZombie(zombie, typeZombie);
                
                await Delay(FromSeconds(Random.Range(0, 1.5f)));
            }
        }
        
        private void SetupZombie(Zombie zombie, int typeZombie)
        {
            float floatLine = Round(Range(0, 5));
            Line line = (Line)floatLine;
            zombie.transform.SetParent(_parent.transform, false);
            zombie.transform.position = _spawnPoints[(int)floatLine].transform.position;
            
            _zombiesInLevel[typeZombie].Count -= 1;
            _zombieRepository.Register(line, zombie);
            
            zombie.IsDead += Destroy;
            void Destroy()
            {
                zombie.IsDead -= Destroy;
                _zombieRepository.Unregister(line, zombie);
                _zombiePool.Release(typeZombie, zombie);
            }
        }

        public void Dispose() => 
            _disposables?.Dispose();
    }
}
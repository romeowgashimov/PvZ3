using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
using static UnityEngine.Random;

namespace _Project.Logic.Core
{
    public class ZombieSystem
    {
        private ZombiePool _zombiePool;
        private ZombieRepository _zombieRepository;
        private IReadOnlyList<GameObject> _spawnPoints;
        private Transform _parent;
        private (string ZombieId, int Count)[] _zombiesInLevel;

        public ZombieSystem(ZombieRepository zombieRepository, ZombieSpawnRepository zombieSpawnRepository, ZombiesContainer zombiesContainer, Transform canvas)
        {
            _zombieRepository = zombieRepository;
            _spawnPoints = zombieSpawnRepository.SpawnPoints;
            _parent = canvas;
            
            int length = zombiesContainer.ZombieConfigs.Length;
            _zombiesInLevel = new (string ZombieId, int Count)[length];
            int[] zombiesCount = new int[length];
            for (int i = 0; i < length; i++)
            {
                _zombiesInLevel[i] = (zombiesContainer.ZombieConfigs[i].ZombieId,
                    zombiesContainer.ZombieConfigs[i].Count);
                zombiesCount[i] = zombiesContainer.ZombieConfigs[i].Count;
            }

            _zombiePool = new(zombiesCount);
        }

        public void Prepare()
        {
            string[] zombiesIds = new string[_zombiesInLevel.Length];
            for (int i = 0; i < _zombiesInLevel.Length; i++)
                zombiesIds[i] = _zombiesInLevel[i].ZombieId;
            
            _zombiePool.Prepare(zombiesIds);

            for (int i = 0; i < _zombiesInLevel.Length; i++)
            {
                //Какое количество показывать зомбаков в начале в зависимости от их типа?
                for (int j = 0; j < _zombiesInLevel[i].Count; j++)
                {
                    Zombie zombie = _zombiePool.Get(i);
                    
                    //Они могут настакаться друг на друга
                    float floatLine = Round(Range(0, 5));
                    Line line = (Line)floatLine;
                    zombie.transform.SetParent(_parent.transform, false);
                    zombie.transform.position = _spawnPoints[(int)floatLine].transform.position;
                }
            }
        }

        public void Run()
        {

        }

        private void SetupZombie(Zombie zombie, int indexForReduce)
        {
            float floatLine = Round(Range(1, 5));
            Line line = (Line)floatLine;
            zombie.transform.position = _spawnPoints[(int)floatLine].transform.position;
            _zombieRepository.Register(line, zombie);
            
            zombie.IsDead += Destroy;
            void Destroy()
            {
                zombie.IsDead -= Destroy;
                _zombieRepository.Unregister(line, zombie);
                _zombiesInLevel[indexForReduce].Count -= 1;
                _zombiePool.Release(1, zombie);
            }
        }
    }
}
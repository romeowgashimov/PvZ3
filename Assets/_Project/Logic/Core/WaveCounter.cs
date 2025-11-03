using UniRx;
using UnityEngine;
using static _Project.Logic.Core.WaveType;
using static UnityEngine.Time;

namespace _Project.Logic.Core
{
    public class WaveCounter
    {
        private float _timeForDefaultSpawn;
        private float _timeForWaveSpawn;

        private ReactiveProperty<WaveType> _onSpawn = new(None);

        public float CurrentLevelTime { get; private set; }
        public int WaveCount { get; private set; }
        public IReadOnlyReactiveProperty<WaveType> OnSpawn => _onSpawn;
    
        public WaveCounter(float timeLevelInMinuts, int waveCount)
        {
            _timeForDefaultSpawn = timeLevelInMinuts * 60 /  waveCount / 2;
            _timeForWaveSpawn = timeLevelInMinuts * 60 / waveCount;
            WaveCount = waveCount;
        }

        public void Run()
        {
            if (WaveCount == 0) 
                return;
            
            CurrentLevelTime += fixedDeltaTime;
            
            if (CurrentLevelTime >= _timeForDefaultSpawn)
            {
                _onSpawn.Value = Default;
                _timeForDefaultSpawn += _timeForWaveSpawn;
            }
            
            if (CurrentLevelTime >= _timeForWaveSpawn)
            {
                _onSpawn.Value = Wave;
                _timeForWaveSpawn += _timeForWaveSpawn;
                WaveCount--;
            }
        }
    }
}
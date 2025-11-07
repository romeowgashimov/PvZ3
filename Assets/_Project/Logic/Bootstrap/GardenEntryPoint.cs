using System.Collections.Generic;
using _Project.Logic.Core;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class GardenEntryPoint : IInitializable, ITickable
    {
        private readonly WindowsSystem _windowsSystem;
        private readonly SlotsSystem _slotsSystem;
        private readonly ZombieSystem _zombieSystem;
        private WaveCounter _waveCounter;
        private ZombieRepository _zombieRepository;
        private PlantsRepository _plantsRepository;
        private RunableRepository _runableRepository;
        private bool _isPaused = true;

        public void Pause() => _isPaused = true;
        public void Unpause() => _isPaused = false;
        
        public GardenEntryPoint(WindowsSystem windowsSystem, SlotsSystem slotsSystem, 
            ZombieSystem zombieSystem, WaveCounter waveCounter, ZombieRepository zombieRepository,
            RunableRepository runableRepository, PlantsRepository plantsRepository)
        {
            _windowsSystem = windowsSystem;
            _slotsSystem = slotsSystem;
            _zombieSystem = zombieSystem;
            _waveCounter = waveCounter;
            _zombieRepository = zombieRepository;
            _runableRepository = runableRepository;
            _plantsRepository = plantsRepository;

            _windowsSystem.Setup(this);
        }

        public void Initialize()
        {
            _slotsSystem.Prepare();
            _zombieSystem.Prepare();
            _windowsSystem.Prepare();
        }

        public void Tick()
        {
            if (!_isPaused)
            {
                _waveCounter.Run();
                
                foreach (KeyValuePair<Line, List<Zombie>> line in _zombieRepository.ZombiesInScene)
                    foreach (Zombie zombie in line.Value)
                        zombie?.Run();

                foreach (Plant plant in _plantsRepository.PlantsInScene)
                    plant.Run();
                
                for (int i = 0; i < _runableRepository.Runables.Count; ++i)
                    _runableRepository.Runables[i].Run();
            }
        }
    }
}
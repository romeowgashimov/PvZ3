using _Project.Logic.Core;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class GardenEntryPoint : IInitializable, ITickable
    {
        private readonly WindowsSystem _windowsSystem;
        private readonly SlotsSystem _slotsSystem;
        private readonly ZombieSystem _zombieSystem;
        private bool _isPaused = true;

        public void Pause() => _isPaused = true;
        public void Unpause() => _isPaused = false;
        
        public GardenEntryPoint(WindowsSystem windowsSystem, SlotsSystem slotsSystem, ZombieSystem zombieSystem)
        {
            _windowsSystem = windowsSystem;
            _slotsSystem = slotsSystem;
            _zombieSystem = zombieSystem;

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

            }
        }
    }
}
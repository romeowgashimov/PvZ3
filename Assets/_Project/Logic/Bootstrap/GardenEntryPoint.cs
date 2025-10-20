using _Project.Logic.Core;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class GardenEntryPoint : IInitializable, ITickable
    {
        private readonly WindowsSystem _windowsSystem;
        private readonly SlotsSystem _slotsSystem;
        private bool _isPaused = true;

        public void Pause() => _isPaused = true;
        public void Unpause() => _isPaused = false;
        
        public GardenEntryPoint(WindowsSystem windowsSystem, SlotsSystem slotsSystem)
        {
            _windowsSystem = windowsSystem;
            _slotsSystem = slotsSystem;
            _windowsSystem.Setup(this);
        }

        public void Initialize()
        {
            _slotsSystem.Prepare();
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
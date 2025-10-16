using _Project.Logic.Core;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class GardenEntryPoint : IInitializable, ITickable
    {
        private readonly WindowsSystem _windowsSystem;
        
        public GardenEntryPoint(WindowsSystem windowsSystem)
        {
            _windowsSystem = windowsSystem;
        }
        
        public void Initialize()
        {
            _windowsSystem.Activate();
        }

        public void Tick()
        {
            
        }
    }
}
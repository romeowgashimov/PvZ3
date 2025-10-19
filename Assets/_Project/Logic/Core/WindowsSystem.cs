using _Project.Logic.Bootstrap;

namespace _Project.Logic.Core
{
    public abstract class WindowsSystem
    {
        protected WindowsFactory _windowsFactory;
        protected GardenEntryPoint _gardenEntryPoint;
        
        public void Setup(GardenEntryPoint gardenEntryPoint) => 
            _gardenEntryPoint = gardenEntryPoint;
        
        public abstract void Prepare();
        
    }
}
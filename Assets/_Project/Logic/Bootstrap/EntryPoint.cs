using _Project.Logic.Core;
using _Project.Logic.Implementation.ViewModels;
using _Project.Logic.Implementation.Views;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class EntryPoint : IInitializable, ITickable
    {
        private readonly WindowsFactory _windowsFactory;
        private readonly WindowsSwitcher _windowsSwitcher;
        
        public EntryPoint(WindowsFactory windowsFactory, WindowsSwitcher windowsSwitcher)
        {
            _windowsFactory = windowsFactory;
            _windowsSwitcher = windowsSwitcher;
        }
        
        public void Initialize()
        {
            _windowsSwitcher.Switch<PlantChoiceView, PlantChoiceViewModel>();
        }

        public void Tick()
        {
            
        }
    }
}
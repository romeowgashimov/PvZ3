using _Project.Logic.Core;
using _Project.Logic.Implementation.ViewModels;
using _Project.Logic.Implementation.Views;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class EntryPoint : IInitializable, ITickable
    {
        private readonly WindowsSystem _windowsSystem;
        private DiContainer _diContainer;
        
        public EntryPoint(WindowsSystem windowsSystem, DiContainer diContainer)
        {
            _windowsSystem = windowsSystem;
            _diContainer = diContainer;
        }
        
        public void Initialize()
        {
            CurrentPlantsView currentPlantsView  = _windowsSystem.CreateAndShow<CurrentPlantsView, CurrentPlantsViewModel>();
            CardAnimator cardAnimator = new(currentPlantsView.Parent);
            _diContainer.Bind<CardAnimator>().FromInstance(cardAnimator);
            
            PlantChoiceView plantChoiceView = _windowsSystem.CreateAndShow<PlantChoiceView, PlantChoiceViewModel>();
        }

        public void Tick()
        {
            
        }
    }
}
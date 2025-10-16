using _Project.Logic.Implementation.ViewModels;
using _Project.Logic.Implementation.Views;
using Zenject;

namespace _Project.Logic.Core
{
    public class ChoiceWindowsSystem : WindowsSystem
    {
        private DiContainer _diContainer;

        public ChoiceWindowsSystem(WindowsFactory windowsFactory, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _windowsFactory = windowsFactory;
        }
        
        public override void Activate()
        {
            CurrentPlantsView currentPlantsView  = _windowsFactory.Create<CurrentPlantsView, CurrentPlantsViewModel>();
            CardAnimator cardAnimator = new(new(currentPlantsView.Positions));
            _diContainer.Bind<CardAnimator>().FromInstance(cardAnimator);
            
            PlantChoiceView plantChoiceView = _windowsFactory.Create<PlantChoiceView, PlantChoiceViewModel>();
            
            currentPlantsView.Show();
            plantChoiceView.Show();
        }
    }
}
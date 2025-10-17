using _Project.Logic.Implementation.ViewModels;
using _Project.Logic.Implementation.Views;
using static Cysharp.Threading.Tasks.UniTask;

namespace _Project.Logic.Core
{
    public class ChoiceWindowsSystem : WindowsSystem
    {
        private const float DELAY_FOR_LOADING_CARD_POSITIONS = .25f;

        public ChoiceWindowsSystem(WindowsFactory windowsFactory)
        {
            _windowsFactory = windowsFactory;
        }

        public override async void Activate()
        {
            CurrentPlantsView currentPlantsView  = _windowsFactory.Create<CurrentPlantsView, CurrentPlantsViewModel>();

            PlantChoiceView plantChoiceView = _windowsFactory.Create<PlantChoiceView, PlantChoiceViewModel>();

            await WaitForSeconds(DELAY_FOR_LOADING_CARD_POSITIONS);
            
            CardAnimator cardAnimator = new(new(currentPlantsView.Positions), new(plantChoiceView.CardsPositions));
            
            plantChoiceView.Setup(cardAnimator);
            currentPlantsView.Show();
            plantChoiceView.Show();
        }
    }
}
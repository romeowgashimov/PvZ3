using _Project.Logic.Implementation.ViewModels;
using _Project.Logic.Implementation.Views;
using DG.Tweening;
using UnityEngine;
using static System.Threading.Tasks.Task;
using static System.TimeSpan;

namespace _Project.Logic.Core
{
    public class ChoiceWindowsSystem : WindowsSystem
    {
        private const float DELAY_FOR_LOADING_CARD_POSITIONS = .1f;
        private const float TIME_FOR_START = 3f;
        
        private CurrentPlantsView _currentPlantsView;
        private PlantChoiceView _plantChoiceView;
        private Transform _garden;
        private float _durationForAnimation;
        private float _endValueForAnimation;
        
        public ChoiceWindowsSystem(WindowsFactory windowsFactory, Transform garden, 
            float endValueForAnimation = -2.09f, float durationForAnimation = 2.4f)
        {
            _garden = garden;
            _endValueForAnimation = endValueForAnimation;
            _durationForAnimation = durationForAnimation;
            _windowsFactory = windowsFactory;
        }

        public override async void Prepare()
        {
            _currentPlantsView  = _windowsFactory.Create<CurrentPlantsView, CurrentPlantsViewModel>();

            _plantChoiceView = _windowsFactory.Create<PlantChoiceView, PlantChoiceViewModel>();
            _plantChoiceView.Setup(this);
            
            await Delay(FromSeconds(DELAY_FOR_LOADING_CARD_POSITIONS));
            
            CardAnimator cardAnimator = new(new(_currentPlantsView.Positions), new(_plantChoiceView.CardsPositions));
            
            _plantChoiceView.Setup(cardAnimator);

            _garden.DOMoveX(_endValueForAnimation, _durationForAnimation)
                   .SetEase(Ease.OutExpo);
            await Delay(FromSeconds(_durationForAnimation));

            _currentPlantsView.Show();
            _plantChoiceView.Show();
        }

        public async void Activate()
        {
            _plantChoiceView.Hide();
            await Delay(FromSeconds(_plantChoiceView.DurationForAnimation));
            
            _garden.DOMoveX(-_endValueForAnimation, _durationForAnimation)
                   .SetEase(Ease.OutExpo);
            await Delay(FromSeconds(_durationForAnimation));
            //Text with countdown
            await Delay(FromSeconds(TIME_FOR_START));
            
            _gardenEntryPoint.Unpause();
        }
    }
}
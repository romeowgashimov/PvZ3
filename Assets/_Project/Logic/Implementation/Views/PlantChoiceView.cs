using System.Collections.Generic;
using _Project.Logic.Core;
using _Project.Logic.Implementation.ViewModels;
using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Threading.Tasks.Task;
using static System.TimeSpan;

namespace _Project.Logic.Implementation.Views
{
    public class PlantChoiceView : Window<PlantChoiceViewModel>
    {
        private const float DELAY_FROM_CLICK = 75f;
        private const double WAIT_FOR_VIEWMODEL_SETUP = 0.01f;

        [SerializeField] private Button _startButton;
        [SerializeField] private Transform _content;
        [SerializeField] private Image _currentPlantImage;
        [SerializeField] private TextMeshProUGUI _description;

        private CardAnimator _cardAnimator;
        private ChoiceWindowsSystem _rootSystem;

        public IReadOnlyDictionary<string, Transform> CardsPositions;
        public float DurationForAnimation => _windowAnimator.Duration;
        
        public new async void Awake()
        {
            base.Awake();
            
            await Delay(FromSeconds(WAIT_FOR_VIEWMODEL_SETUP));
            
            CardsPositions = _viewModel.CardsPositions;
            
            for (int i = 0; i < _viewModel.Length; i++)
            {
                (Transform cardParentTransform, Card card) = _viewModel.AddCardFromContainer(i);
                cardParentTransform.SetParent(_content, false);

                card.OnClickedProperty
                    .Skip(1)
                    .Throttle(FromMilliseconds(DELAY_FROM_CLICK))
                    .Select(_ => card)
                    .Subscribe(Draw)
                    .AddTo(this);
            }
            
            _startButton.OnClickAsObservable()
                .Where(_ => _viewModel.ReadyForStart)
                .Subscribe(_ => _rootSystem.Activate())
                .AddTo(this);
            
            _startButton.OnClickAsObservable()
                .Where(_ => _viewModel.ReadyForStart)
                .Subscribe(_ => _viewModel.LoadPlants())
                .AddTo(this);
        }

        protected override void Block()
        {
            
        }

        protected override void Unblock()
        {
            
        }

        public void Setup(CardAnimator cardAnimator)
        {
            _cardAnimator = cardAnimator;
            
            _viewModel.CardIn
                .Skip(1)
                .Subscribe(x =>_cardAnimator.Animate(x.card))
                .AddTo(this);
        }

        public void Setup(ChoiceWindowsSystem rootSystem) => 
            _rootSystem = rootSystem;

        private void Draw(Card card)
        {
            _currentPlantImage.color = card.PlantImage.color;
            _description.text = $"{card.PlantId}\n\n{card.PlantDescription}";
        }
    }
}
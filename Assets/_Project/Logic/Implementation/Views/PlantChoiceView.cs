using _Project.Logic.Core;
using _Project.Logic.Implementation.ViewModels;
using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.TimeSpan;

namespace _Project.Logic.Implementation.Views
{
    public class PlantChoiceView : Window<PlantChoiceViewModel>
    {
        private const float DELAY_FROM_CLICK = 75f;
        
        [SerializeField] private Button _startButton;
        [SerializeField] private Transform _content;
        [SerializeField] private Image _currentPlantImage;
        [SerializeField] private TextMeshProUGUI _description;
        
        private CardAnimator _cardAnimator;

        private void Start()
        {
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
        }

        protected override void Block()
        {
            
        }

        protected override void Unblock()
        {
            
        }

        private void Draw(Card card)
        {
            _currentPlantImage.color = card.PlantImage.color;
            _description.text = $"{card.PlantId}\n\n{card.PlantDescription}";
        }
    }
}
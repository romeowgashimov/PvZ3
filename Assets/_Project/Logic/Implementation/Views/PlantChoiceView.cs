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
        [SerializeField] private Canvas _canvas;
        
        private CardAnimator _cardAnimator;

        private void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            _cardAnimator = new();
            _cardAnimator.Setup(_canvas.scaleFactor);
            
            for (int i = 0; i < _viewModel.Length; i++)
            {
                Card card = _viewModel.AddCardFromContainer(i);
                card.transform.SetParent(_content, false);

                card.OnClickedProperty
                    .Skip(1)
                    .Throttle(FromMilliseconds(DELAY_FROM_CLICK))
                    .Select(_ => card)
                    .Subscribe(Draw)
                    .AddTo(this);
                
                SetupCardAnimator(card);
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

        private void SetupCardAnimator(Card card)
        {
            card.OnBeginDragProperty
                .Skip(1)
                .Subscribe(_ => _cardAnimator.OnBeginDrag(card.OnBeginDragProperty.Value, card.RectTransform))
                .AddTo(this);
            
            card.OnDragProperty
                .Skip(1)
                .Subscribe(_cardAnimator.OnDrag)
                .AddTo(this);
            
            card.OnEndDragProperty
                .Skip(1)
                .Subscribe(_cardAnimator.OnEndDrag)
                .AddTo(this);
        }
    }
}
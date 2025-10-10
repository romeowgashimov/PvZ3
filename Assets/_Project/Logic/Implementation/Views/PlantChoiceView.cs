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
        
        private void Start()
        {
            int lenght = _viewModel.Length;
            for (int i = 0; i < lenght; i++)
            {
                Card card = _viewModel.AddCardFromContainer(i);
                card.transform.SetParent(_content, false);

                card.OnClicked
                    .Skip(1)
                    .Throttle(FromMilliseconds(DELAY_FROM_CLICK))
                    .Select(_ => card)
                    .Subscribe(Draw)
                    .AddTo(this);
            }
        }

        private void Draw(Card tempCard)
        {
            _currentPlantImage.color = tempCard.PlantImage.color;
            _description.text = $"{tempCard.PlantId}\n\n{tempCard.PlantDescription}";
        }

        protected override void Block()
        {
            
        }

        protected override void Unblock()
        {
            
        }
    }
}
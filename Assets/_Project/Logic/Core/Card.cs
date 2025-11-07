using UniRx;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Logic.Core
{
    public class Card : MonoBehaviour, IUseableContainer, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    { 
        [SerializeField] private Image _outline;
        
        private CardViewModel _cardViewModel;

        [field: SerializeField, ReadOnly] public string Id { get; private set; }
        [field: SerializeField] public string PlantName { get; private set; }
        [field: SerializeField] public Image PlantImage { get; protected set; }

        public string PlantDescription { get; private set; }
        public ReactiveProperty<bool> OnClickedProperty { get; private set; } = new(false);

        public IUseable Useable { get; set; }

        public void OnPointerClick(PointerEventData eventData) =>
            OnClickedProperty.Value = !OnClickedProperty.Value;

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnClickedProperty.Value = !OnClickedProperty.Value;

            if (Useable is not Plant plant) 
                return;
            
            plant.transform.position = transform.position;
            plant.CanvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Useable is Plant plant)
                plant.RectTransform.anchoredPosition += eventData.delta / plant.Canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Useable is Plant plant)
                plant.transform.position = transform.position;
        }

        public void Setup(CardViewModel cardViewModel)
        {
            _cardViewModel = cardViewModel;

            OnClickedProperty
                             .Skip(1)
                             .Select(_ => this)
                             .Subscribe(cardViewModel.ChangeSelectedCard)
                             .AddTo(this);
        }

        public void SetupPlant(string id, string plantName, Color cardColor, string cardDescription)
        {
            Id = id;
            PlantName = plantName;
            PlantImage.color = cardColor;
            PlantDescription = cardDescription;
        }

        public void DrawOutline(bool enable) =>
            _outline.enabled = enable;
    }
}
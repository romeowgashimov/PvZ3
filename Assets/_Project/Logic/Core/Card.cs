using UniRx;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Logic.Core
{
    public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField] private Image _outline;
        [SerializeField] private Transform _transform;
        
        [field: SerializeField, ReadOnly] public string PlantId { get; private set; }
        [field: SerializeField] public Image PlantImage { get; protected set; }
        
        private PlantFactory _plantFactory;
        private CardViewModel _cardViewModel;
        
        public string PlantDescription { get; private set; }
        public RectTransform RectTransform { get; private set; }
        
        public ReactiveProperty<bool> OnClickedProperty = new(false);
        public ReactiveProperty<bool> OnBeginDragProperty = new(false);
        public ReactiveProperty<Vector2> OnDragProperty = new();
        public ReactiveProperty<bool> OnEndDragProperty = new(false);

        private void Start() => 
            RectTransform = GetComponent<RectTransform>();

        public void OnBeginDrag(PointerEventData eventData) => 
            OnBeginDragProperty.Value = !OnBeginDragProperty.Value;

        public void OnDrag(PointerEventData eventData) => 
            OnDragProperty.Value = eventData.delta;

        public void OnEndDrag(PointerEventData eventData) => 
            OnEndDragProperty.Value = !OnEndDragProperty.Value;

        public void OnPointerClick(PointerEventData eventData) => 
            OnClickedProperty.Value = !OnClickedProperty.Value;

        public void Setup(CardViewModel cardViewModel) => 
            _cardViewModel = cardViewModel;
        
        public void AddPlant(string plantId, Color cardColor, string cardDescription)
        {
            PlantId = plantId;
            PlantImage.color = cardColor;
            PlantDescription = cardDescription;
        }
    }
}
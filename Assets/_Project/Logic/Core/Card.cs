using _Project.Logic.Implementation.Views;
using UniRx;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Logic.Core
{
    public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [field: SerializeField, ReadOnly] public string PlantId { get; private set; }
        [field: SerializeField] public Image PlantImage { get; protected set; }
        [SerializeField] private Image _outline;
        [SerializeField] private Transform _transform;
        
        private PlantFactory _plantFactory;
        
        public string PlantDescription { get; private set; }
        public ReactiveProperty<bool> OnClicked { get; private set; } = new(false);

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Plant Call
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Plant Call
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Plant Call
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.clickCount)
            {
                case 1:
                    OnClicked.Value = !OnClicked.Value;
                    _outline.enabled = OnClicked.Value;
                    break;
                case 2:
                    //Animator & Choice Call
                    break;
            }
        }

        public void Setup(string plantId, Color cardColor, string cardDescription)
        {
            PlantId = plantId;
            PlantImage.color = cardColor;
            PlantDescription = cardDescription;
        }
    }
}
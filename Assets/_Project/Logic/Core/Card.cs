using System;
using UniRx;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Logic.Core
{
    public class Card : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        [SerializeField] private Image _outline;
        [SerializeField] private Transform _transform;

        private CardViewModel _cardViewModel;
        
        [field: SerializeField, ReadOnly] public string Id { get; private set; }
        [field: SerializeField] public string PlantName { get; private set; }
        [field: SerializeField] public Image PlantImage { get; protected set; }

        public string PlantDescription { get; private set; }
        public ReactiveProperty<bool> OnClickedProperty { get; private set; } = new(false);
        public ReactiveProperty<Vector2> OnDraga = new();
        
        public void OnPointerClick(PointerEventData eventData) => 
            OnClickedProperty.Value = !OnClickedProperty.Value;

        public void Setup(CardViewModel cardViewModel)
        {
            _cardViewModel = cardViewModel;
            
            OnClickedProperty
                             .Skip(1)  
                             .Select(_ => this)
                             .Subscribe(cardViewModel.ChangeSelectedCard)
                             .AddTo(this);
            
            OnDraga
                .Skip(1)
                .Select(_ => OnDraga.Value)
                .Subscribe(cardViewModel.Drag)
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

        public void OnDrag(PointerEventData eventData)
        {
            OnDraga.Value = eventData.delta;
        }
    }
}
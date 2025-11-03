using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using static _Project.Logic.Core.SlotType;

namespace _Project.Logic.Core
{
    public abstract class Plant : MonoBehaviour, IUseable
    {
        [SerializeField] public CanvasGroup _canvasGroup;
        [Inject] private Canvas _canvas;
        
        protected SlotType _slotType = Free;
        protected Line _line;
        protected bool _haveEnemy;
        
        public RectTransform _rectTransform;

        public event Action<Line> OnPlaced;
        public event Action<Line> OnDead;

        private void Awake() => 
            _rectTransform = GetComponent<RectTransform>();

        public void Use(Slot slot)
        {
            if (slot.SlotType != _slotType)
                return;
                
            slot.ChangeCurrentPlant(this, WithPlant);
            _line = slot.Line;
            
            OnDead += Release;

            void Release(Line _)
            {
                OnDead -= Release;
                slot.ChangeCurrentPlant(null, Free);
            }
            
            transform.SetParent(slot.transform, false);
            transform.localPosition = Vector3.zero;
            
            OnPlaced?.Invoke(slot.Line);
        }

        public void Death(Line line) => 
            OnDead?.Invoke(line);

        public void SetEnemy(bool zombieSpawned) => 
            _haveEnemy = zombieSpawned;

        public abstract void Run();

        public abstract void Boost();

        public void OnBeginDrag(PointerEventData eventData) => 
            _canvasGroup.blocksRaycasts = false;

        public void OnDrag(PointerEventData eventData) =>
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
}
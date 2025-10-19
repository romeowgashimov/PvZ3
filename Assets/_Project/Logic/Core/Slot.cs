using _Project.Logic.Bootstrap;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static _Project.Logic.Bootstrap.SlotType;

namespace _Project.Logic.Core
{
    public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField, ReadOnly] private SlotType _slotType = Free;
        private Useable _current;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
        
        public void ChangeType(SlotConfig slotConfig)
        {
            if (slotConfig.SlotType != Free && slotConfig.SlotType != NonFree)
                _current = slotConfig.Useable;
            
            _slotType = slotConfig.SlotType;
        }
    }
}
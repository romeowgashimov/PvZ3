using _Project.Logic.Bootstrap;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static _Project.Logic.Core.SlotType;

namespace _Project.Logic.Core
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        [SerializeField, ReadOnly] private SlotType _slotType = Free;
        private Useable _current;

        public void OnDrop(PointerEventData eventData)
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
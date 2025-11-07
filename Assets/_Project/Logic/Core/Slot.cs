using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static _Project.Logic.Core.SlotType;

namespace _Project.Logic.Core
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        [field: SerializeField] public SlotType SlotType { get; private set; } = Free;
        [field: SerializeField] public Line Line { get; private set; }
        [field: SerializeField] public Plant Current { get; private set; }

        public void ChangeCurrentPlant(Plant plant, SlotType slotType)
        {
            Current = plant;
            SlotType = slotType;
            
            if (Current == null)
                SlotType = Free;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (SlotType == NonFree) 
                return;
            
            IUseableContainer container = eventData.pointerDrag.GetComponent<IUseableContainer>();
            container.Useable.Use(this);
        }

        public void Setup(SlotConfig slotConfig, Line line)
        {
            if (slotConfig != null)
                if (slotConfig.SlotType != Free && slotConfig.SlotType != NonFree)
                {
                    SlotType = slotConfig.SlotType;
                    Current = slotConfig.Useable;
                    Current.transform.position = transform.position;
                }
            
            Line = line;
        }
    }
}
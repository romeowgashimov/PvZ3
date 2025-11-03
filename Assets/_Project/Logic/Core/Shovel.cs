using UnityEngine;
using static _Project.Logic.Core.SlotType;

namespace _Project.Logic.Core
{
    public class Shovel : MonoBehaviour, IUseable
    {
        public void Use(Slot slot)
        {
            if (slot.SlotType == WithPlant)
                slot.ChangeCurrentPlant(null, Free);
        }
    }
}
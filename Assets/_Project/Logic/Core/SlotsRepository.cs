using UnityEngine;

namespace _Project.Logic.Core
{
    public class SlotsRepository : MonoBehaviour
    {
        [field: SerializeField] public Slot[] Slots { get; private set; }
    }
}
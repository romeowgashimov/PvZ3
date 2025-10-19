using UnityEngine;

namespace _Project.Logic.Core
{
    [CreateAssetMenu(fileName = "SlotsContainer", menuName = "Create SlotsContainer", order = 0)]
    public class SlotsContainer : ScriptableObject
    {
        [field: SerializeField] public SlotConfig[] Slots { get; private set; }
    }
}
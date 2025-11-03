using UnityEngine;

namespace _Project.Logic.Core
{
    public class Leaf : MonoBehaviour, IUseable
    {
        public void Use(Slot slot) => 
            slot.Current?.Boost();
    }
}
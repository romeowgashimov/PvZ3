using UnityEngine;

namespace _Project.Logic.Core
{
    public abstract class Useable : MonoBehaviour
    {
        public abstract Useable Use(Slot slot);
    }
}
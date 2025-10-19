using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Logic.Core
{
    public abstract class WindowAnimator : MonoBehaviour
    {
        [field: SerializeField] public float Duration { get; private set; }
        
        public abstract void Prepare();
        
        public abstract UniTask Show();
        
        public abstract UniTask Hide();
    }
}
using System;
using UnityEngine;

namespace _Project.Logic.Core
{
    internal abstract class Bullet : MonoBehaviour, IRunable
    {
        [SerializeField] protected float _size = 1;
        
        protected int _damage;
        protected Line _line;

        public Vector3 Position => transform.position;
        
        public event Action OnTriggerEnter;

        public void CallTriggerEvent() =>
            OnTriggerEnter?.Invoke();

        public abstract void Prepare(Plant plant);

        public abstract void Run();
    }
}
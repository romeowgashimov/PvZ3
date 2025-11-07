using System;
using UnityEngine;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Logic.Core
{
    public class Zombie : MonoBehaviour
    {
        [SerializeField] protected float _speed = 0.5f;
        [SerializeField] protected float _health = 50;

        [field: SerializeField] public float Damage { get; private set; }
        public Vector3 Position => transform.position;
        
        public event Action IsDead;

        public void Run()
        {
            transform.position += Vector3.left * (fixedDeltaTime * _speed);
        }

        public void GetDamage(int damage)
        {
            if (_health > 0) 
                _health = Clamp(_health - damage, 0,_health - damage);
            
            if ( _health <= 0)
                IsDead?.Invoke();
        }
    }
}
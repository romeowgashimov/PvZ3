using System;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Logic.Core
{
    public class Zombie : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        
        public event Action IsDead;

        public void Run()
        {
            transform.position += Vector3.left * fixedDeltaTime;
        }
    }
}
using UnityEngine;

namespace _Project.Logic.ECS
{
    public struct ShootComponent : IComponent
    {
        public Entity Entity { get; set; }
        public Vector3 Direction;
        public float Speed;
        public float Damage;
    }
}
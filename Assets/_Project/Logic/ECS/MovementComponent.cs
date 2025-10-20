using UnityEngine;

namespace _Project.Logic.ECS
{
    public struct MovementComponent : IComponent
    {
        public Entity Entity { get; set; }
        public Transform Transform;
        public Vector3 Velocity;
        public float Speed;
    }
}
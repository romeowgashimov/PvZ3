using static UnityEngine.Time;

namespace _Project.Logic.ECS
{
    public struct MovementSystem : ISystem<MovementComponent>
    {
        public void Run(MovementComponent component)
        {
            component.Transform.position += component.Velocity * component.Speed * deltaTime;
        }
    }
}
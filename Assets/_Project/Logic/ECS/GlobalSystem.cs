using System.Collections.Generic;

namespace _Project.Logic.ECS
{
    public struct GlobalSystem
    {
        public MovementSystem MovementSystem;
        public ShootSystem ShootSystem;

        public List<MovementComponent> MovementComponents;
        public List<ShootComponent> ShootComponents;
        
        public void Run()
        {
            foreach (MovementComponent component in MovementComponents)
                MovementSystem.Run(component);
            foreach (ShootComponent component in ShootComponents)
                ShootSystem.Run(component);
        }
    }
}
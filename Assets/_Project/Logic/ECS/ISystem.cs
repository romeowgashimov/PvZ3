namespace _Project.Logic.ECS
{
    public interface ISystem<in T> where T : IComponent
    {
        void Run(T component);
    }
}
using static UnityEngine.Object;
using static UnityEngine.Resources;

namespace _Project.Logic.Core
{
    public class ZombieFactory
    {
        public Zombie Create(string idZombie)
        {
            Zombie resource = Load<Zombie>($"Zombies Prefabs/{idZombie}");
            Zombie instance = Instantiate(resource);
            
            return instance;
        }
    }
}
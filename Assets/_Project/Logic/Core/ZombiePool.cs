using System.Collections.Generic;

namespace _Project.Logic.Core
{
    public class ZombiePool
    {
        private const int COUNT_INIT_ZOMBIES = 5;
        
        private Stack<Zombie>[] _zombiePool;
        private string[] _zombiesIds;
        private ZombieFactory _zombieFactory;

        public ZombiePool((string zombiesId, int count)[] zombiesInScene)
        {
            _zombieFactory = new();
            _zombiesIds = new string[zombiesInScene.Length];
            _zombiePool = new Stack<Zombie>[zombiesInScene.Length];
            for (int i = 0; i < zombiesInScene.Length; i++)
            {
                _zombiePool[i] = new(zombiesInScene[i].count / 2);
                _zombiesIds[i] = zombiesInScene[i].zombiesId;
            }
        }

        public void Prepare()
        {
            for (int i = 0; i < _zombiesIds.Length; i++)
                for (int j = 0; j < COUNT_INIT_ZOMBIES; j++)
                {
                    Zombie zombie = _zombieFactory.Create(_zombiesIds[i]);
                    _zombiePool[i].Push(zombie);
                }
        }

        public Zombie Get(int typeZombie)
        {
            Zombie zombie = _zombiePool[typeZombie].Count != 0 
                ? _zombiePool[typeZombie].Pop() 
                : _zombieFactory.Create(_zombiesIds[typeZombie]);

            zombie.gameObject.SetActive(true);
            return zombie;
        }

        public void Release(int typeZombie, Zombie zombie)
        {
            zombie.gameObject.SetActive(false);
            _zombiePool[typeZombie].Push(zombie);
        }
    }
}
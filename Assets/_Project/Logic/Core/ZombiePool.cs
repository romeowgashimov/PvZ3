using System.Collections.Generic;

namespace _Project.Logic.Core
{
    public class ZombiePool
    {
        //Словарик лучше?
        private Stack<Zombie>[] _zombiePool;
        private string[] _zombiesIds;
        private ZombieFactory _zombieFactory;

        public ZombiePool(params int[] counts)
        {
            _zombieFactory = new();
            _zombiesIds = new string[counts.Length];
            _zombiePool = new Stack<Zombie>[counts.Length];
            for (int i = 0; i < counts.Length; i++)
                _zombiePool[i] = new(counts[i] / 2);
        }

        public void Prepare(params string[] zombiesIds)
        {
            for (int i = 0; i < zombiesIds.Length; i++)
            {
                _zombiesIds[i] = zombiesIds[i];
                //Надо как-то указывать начальное количество зомби на начальном экране,
                //их должно быть мало, но по-разному мало
                for (int j = 0; j < 5; j++)
                {
                    Zombie zombie = _zombieFactory.Create(zombiesIds[i]);
                    _zombiePool[i].Push(zombie);
                }
            }
        }

        public Zombie Get(int typeZombie)
        {
            Zombie zombie = _zombiePool[typeZombie].Peek() != null 
                ? _zombiePool[typeZombie].Pop() 
                : _zombieFactory.Create(_zombiesIds[typeZombie]);

            zombie.enabled = true;
            return zombie;
        }

        public void Release(int typeZombie, Zombie zombie)
        {
            zombie.enabled = false;
            _zombiePool[typeZombie].Push(zombie);
        }
    }
}
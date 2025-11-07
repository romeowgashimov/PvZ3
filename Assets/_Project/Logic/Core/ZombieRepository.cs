using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static _Project.Logic.Core.Line;
using static UnityEngine.Vector3;

namespace _Project.Logic.Core
{
    public class ZombieRepository
    {
        private Dictionary<Line, List<Zombie>> _zombiesInScene = new()
        {
            [First] =  new(10),
            [Second] =  new(10),
            [Third] =  new(10),
            [Fourth] =  new(10),
            [Fifth] =  new(10)
        };
        
        public readonly Action<Zombie>[] OnZombieSpawned = new Action<Zombie>[5];
        
        public IReadOnlyDictionary<Line, List<Zombie>> ZombiesInScene => _zombiesInScene;
        
        public void Register(Line line, Zombie zombie)
        {
            _zombiesInScene[line].Add(zombie);
            
            OnZombieSpawned[(int)line]?.Invoke(zombie);
        }

        public void Unregister(Line line, Zombie zombie)
        {
            if (!_zombiesInScene.TryGetValue(line, out List<Zombie> zombies))
                throw new($"List dont exist in {line} line");
            
            zombies.Remove(zombie);

            TryFindZombie(line, out Zombie findZombie);
            OnZombieSpawned[(int)line]?.Invoke(findZombie);
        }

        public bool TryFindZombie(Line line, out Zombie zombie)
        {
            if (!_zombiesInScene.TryGetValue(line, out List<Zombie> zombies))
                throw new($"List dont exist in {line} line");

            zombie = zombies.FirstOrDefault(x => x != null);
            return zombie != null;
        }

        public bool TryFindClosestZombie(Line line, Vector3 position, out Zombie closest)
        {
            if (!_zombiesInScene.TryGetValue(line, out List<Zombie> zombies))
                throw new($"List dont exist in {line} line");

            closest = null;
            float closestDistance = 100;
            foreach (Zombie zombie in zombies)
            {
                float currentDistance = Distance(position, zombie.Position);

                if (closestDistance > currentDistance)
                {
                    closestDistance = currentDistance;
                    closest = zombie;
                }
            }

            return closest != null;
        }
    }
}
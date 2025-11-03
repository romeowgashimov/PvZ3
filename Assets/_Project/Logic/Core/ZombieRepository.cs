using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static _Project.Logic.Core.Line;

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
        
        public readonly Action<bool>[] OnZombieSpawned = new Action<bool>[5];
        
        public IReadOnlyDictionary<Line, List<Zombie>> ZombiesInScene => _zombiesInScene;
        
        public void Register(Line line, Zombie zombie)
        {
            _zombiesInScene[line].Add(zombie);
            
            OnZombieSpawned[(int)line]?.Invoke(true);
        }

        public void Unregister(Line line, Zombie zombie)
        {
            if (!_zombiesInScene.TryGetValue(line, out List<Zombie> zombies))
                throw new($"List dont exist in {line} line");

            int index = zombies.FindIndex(x => x == zombie);
            zombies[index] = null;

            bool lastZombie = TryFindZombie(line, out Zombie _);
            OnZombieSpawned[(int)line]?.Invoke(lastZombie);
        }

        public bool TryFindZombie(Line line, out Zombie zombie)
        {
            if (!_zombiesInScene.TryGetValue(line, out List<Zombie> zombies))
                throw new($"List dont exist in {line} line");

            zombie = zombies.FirstOrDefault(x => x != null);
            return zombie != null;
        }
    }
}
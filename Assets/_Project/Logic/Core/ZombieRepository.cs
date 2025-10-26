using System.Collections.Generic;
using System.Linq;
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

        public void Register(Line line, Zombie zombie) => 
            _zombiesInScene[line].Add(zombie);

        public void Unregister(Line line, Zombie zombie)
        {
            if (!_zombiesInScene.TryGetValue(line, out List<Zombie> zombies))
                throw new($"List dont exist in {line} line");

            int index = zombies.FindIndex(x => x == zombie);
            zombies[index] = null;
        }

        public bool TryFindZombie(Line line, out Zombie zombie)
        {
            if (!_zombiesInScene.TryGetValue(line, out List<Zombie> zombies))
                throw new($"List dont exist in {line} line");

            zombie = zombies.First(x => x != null);
            return zombie != null;
        }
    }
}
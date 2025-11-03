using System.Collections.Generic;

namespace _Project.Logic.Core
{
    public class PlantsRepository
    {
        private List<Plant> _plants = new(20);
        
        public IReadOnlyList<Plant> Plants => _plants;

        public void Register(Plant plant) => 
            _plants.Add(plant);
        
        public void Unregister(Plant plant) => 
            _plants.Remove(plant);
    }
}
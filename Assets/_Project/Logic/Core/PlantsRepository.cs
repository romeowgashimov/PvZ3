using System.Collections.Generic;

namespace _Project.Logic.Core
{
    public class PlantsRepository
    {
        private List<Plant> _plantsInScene = new(20);
        
        public IReadOnlyList<Plant> PlantsInScene => _plantsInScene;

        public void Register(Plant plant) => 
            _plantsInScene.Add(plant);
        
        public void Unregister(Plant plant) => 
            _plantsInScene.Remove(plant);
    }
}
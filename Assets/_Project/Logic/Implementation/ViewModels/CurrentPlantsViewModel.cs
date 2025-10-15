using _Project.Logic.Core;
using UniRx;

namespace _Project.Logic.Implementation.ViewModels
{
    public class CurrentPlantsViewModel
    {
        private CurrentPlants _currentPlants;
        
        public IReadOnlyReactiveCollection<Card> Cards => _currentPlants.Cards;

        public CurrentPlantsViewModel(CurrentPlants currentPlants) => 
            _currentPlants = currentPlants;

        
    }
}
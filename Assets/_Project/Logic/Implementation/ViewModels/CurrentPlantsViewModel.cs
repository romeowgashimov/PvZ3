using _Project.Logic.Core;
using UniRx;
using UnityEngine.EventSystems;

namespace _Project.Logic.Implementation.ViewModels
{
    public class CurrentPlantsViewModel
    {
        private CurrentPlants _currentPlants;

        public CurrentPlantsViewModel(CurrentPlants currentPlants) => 
            _currentPlants = currentPlants;

        public void DragCurrentPlant(PointerEventData eventData)
        {
            
        }
    }
}
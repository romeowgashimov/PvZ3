using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Logic.Core
{
    public class CardViewModel
    {
        private CurrentPlants _currentPlants;
        private Plant _currentPlant;
        private Card _selectedCard;
        private PointerEventData _eventData;
        //private SunSystem _sunSystem
        
        public CardViewModel(CurrentPlants currentPlants)
        {
            _currentPlants = currentPlants;
        }

        public void Drag(Vector2 dragPosition)
        {
            _currentPlant._rectTransform.anchoredPosition += dragPosition;
        }
        
        public void ChangeSelectedCard(Card card)
        {
            Plant plant = _currentPlants.GetPlant(card.Id);
            _currentPlant = plant;
            //plant._canvasGroup.blocksRaycasts = false;
            plant.transform.SetAsLastSibling();
            Debug.Log($"Selected {card.Id}");
            plant.transform.position = card.transform.position;
            
            EventSystem.current.SetSelectedGameObject(plant.gameObject);
        }
    }
}
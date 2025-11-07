using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Logic.Core
{
    public class CardViewModel
    {
        private CurrentPlants _currentPlants;
        private Card _selectedCard;
        private PointerEventData _eventData;
        //private SunSystem _sunSystem
        
        public CardViewModel(CurrentPlants currentPlants)
        {
            _currentPlants = currentPlants;
        }

        public void Drag(Vector2 dragPosition)
        {
            
        }
        
        public void ChangeSelectedCard(Card card)
        {
            if (card != _selectedCard)
            {
                Plant plant = _currentPlants.GetPlant(card.Id);
                card.Useable = plant;
                plant.OnPlaced += Unsubscribe;

                void Unsubscribe(Line _)
                {
                    plant.OnPlaced -= Unsubscribe;
                    card.Useable = null;
                    _selectedCard = null;
                }
                
                _selectedCard = card;
            }       
        }
    }
}
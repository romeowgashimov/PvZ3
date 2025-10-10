using static UnityEngine.Object;

namespace _Project.Logic.Core
{
    public class CardFactory
    {
        private readonly Card _prefab;
        
        public CardFactory(Card prefab) => 
            _prefab = prefab;

        public Card Create(PlantConfig card)
        {
            Card instance = Instantiate(_prefab);
            instance.Setup(card.PlantId, card.Color, card.Description);
            
            return instance;
        }
    }
}
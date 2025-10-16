using UniRx;

namespace _Project.Logic.Core
{
    public class CurrentPlants
    {
        private ReactiveCollection<Card> _cards = new();
        private int _cardsCount;

        public readonly IReadOnlyReactiveCollection<Card> Cards;

        public CurrentPlants(int cardsCount)
        {
            _cardsCount = cardsCount;
            Cards = _cards;
        }

        public bool TryAddCard(Card card)
        {
            if (_cards.Count >= _cardsCount) 
                return false;
            
            _cards.Add(card);
            return true;
        }

        public bool TryRemoveCard(Card card) 
        {
            if (!_cards.Contains(card)) 
                return false;
            
            _cards.Remove(card);
            return true;
        }

        public void LoadPlant(Card card)
        {

        }
    }
}
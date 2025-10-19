using UniRx;

namespace _Project.Logic.Core
{
    public class CurrentPlants
    {
        private ReactiveCollection<Card> _cards = new();

        public int CardsCount { get; private set; }
        public readonly IReadOnlyReactiveCollection<Card> Cards;

        public CurrentPlants(int cardsCount)
        {
            CardsCount = cardsCount;
            Cards = _cards;
        }

        public bool TryAddCard(Card card)
        {
            if (_cards.Count >= CardsCount) 
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

        public void LoadPlants()
        {

        }
    }
}
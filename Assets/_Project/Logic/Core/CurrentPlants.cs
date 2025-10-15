using UniRx;

namespace _Project.Logic.Core
{
    public class CurrentPlants
    {
        private ReactiveCollection<Card> _cards = new();
        public readonly IReadOnlyReactiveCollection<Card> Cards;

        public CurrentPlants() => 
            Cards = _cards;

        public void AddCard(Card card) => 
            _cards.Add(card);

        public void RemoveCard(Card card) => 
            _cards.Remove(card);

        public void LoadPlant(Card card)
        {

        }
    }
}
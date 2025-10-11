using UniRx;

namespace _Project.Logic.Core
{
    internal class CurrentPlants
    {
        public ReactiveCollection<Card> Cards { get; } = new();

        public void LoadPlant(Card card)
        {
            
        }
    }
}
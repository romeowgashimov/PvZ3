using System;
using _Project.Logic.Core;
using UniRx;

namespace _Project.Logic.Implementation.ViewModels
{
    public class PlantChoiceViewModel : IDisposable
    {
        private CurrentPlants _currentPlants;
        private PlantsContainer _plantsContainer;
        private CardFactory _cardFactory;
        private CompositeDisposable _disposable;

        public int Length => 
            _plantsContainer.Plants.Length;

        public PlantChoiceViewModel(Card prefab, PlantsContainer plantsContainer)
        {
            _cardFactory = new(prefab);
            _plantsContainer = plantsContainer;
        }

        public Card AddCardFromContainer(int index)
        {
            Card card = _cardFactory.Create(_plantsContainer.Plants[index]);
            return card;
        }

        public void Dispose() => 
            _disposable.Dispose();
    }
}
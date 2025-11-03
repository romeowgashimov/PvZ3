using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Logic.Core;
using UniRx;
using UnityEngine;

namespace _Project.Logic.Implementation.ViewModels
{
    public class PlantChoiceViewModel : IDisposable
    {
        private CurrentPlants _currentPlants;
        private PlantsContainer _plantsContainer;
        private CardFactory _cardFactory;
        private CompositeDisposable _disposable = new();
        private ReactiveProperty<Card> _selectedCard = new();
        private ReactiveProperty<(Card, bool)> _cardIn = new();
        private Dictionary<string, Transform> _cardsPositions = new();

        public IReadOnlyDictionary<string, Transform> CardsPositions => _cardsPositions;
        public IReadOnlyReactiveProperty<(Card card, bool add)> CardIn => _cardIn;
        public bool ReadyForStart => _currentPlants.Cards.Count == _currentPlants.CardsCount;
        public int Length => _plantsContainer.Plants.Length;

        public PlantChoiceViewModel(GameObject cardPrefab, PlantsContainer plantsContainer, CurrentPlants currentPlants)
        {
            _cardFactory = new(cardPrefab);
            _plantsContainer = plantsContainer;
            _currentPlants = currentPlants;
        }

        public (Transform, Card) AddCardFromContainer(int index)
        {
            GameObject instance = _cardFactory.Create(_plantsContainer.Plants[index]);
            Card card = instance.GetComponentInChildren<Card>();
            _cardsPositions.Add(card.PlantName, instance.transform);
            
            card.OnClickedProperty
                .Skip(1)
                .Where(_ => card == _selectedCard.Value)
                .Select(_ => card)
                .Subscribe(ControlCard)
                .AddTo(_disposable);
            
            card.OnClickedProperty
                .Skip(1)
                .Select(_ => card)
                .Subscribe(ChangeSelectedCard)
                .AddTo(_disposable);
            
            return (instance.transform, card);
        }

        private void ControlCard(Card card)
        {
            bool condition = _currentPlants.Cards.Contains(card);
            if (!condition)
                _currentPlants.TryAddCard(card);
            else
                _currentPlants.TryRemoveCard(card);
            
            _cardIn.Value = (card, condition);
        }

        private void ChangeSelectedCard(Card card)
        {
            if (card == _selectedCard.Value)
                return;
            
            _selectedCard.Value?.DrawOutline(false);
            card.DrawOutline(true);
            _selectedCard.Value = card;
        }

        public void LoadPlants()
        {
            Dispose();
            _selectedCard.Value?.DrawOutline(false);
            _currentPlants.LoadPlants();
        }
        
        public void Dispose() => 
            _disposable.Dispose();
    }
}
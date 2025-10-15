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
        private CardAnimator _cardAnimator;
        private Dictionary<string, Transform> _cardsPositions = new();
        
        public int Length => _plantsContainer.Plants.Length;

        public PlantChoiceViewModel(GameObject cardPrefab, PlantsContainer plantsContainer, CurrentPlants currentPlants, CardAnimator cardAnimator)
        {
            _cardFactory = new(cardPrefab);
            _plantsContainer = plantsContainer;
            _currentPlants = currentPlants;
            _cardAnimator = cardAnimator;
            _cardAnimator.InitializeNextPosition();
        }

        public (Transform, Card) AddCardFromContainer(int index)
        {
            GameObject instance = _cardFactory.Create(_plantsContainer.Plants[index]);
            Card card = instance.GetComponentInChildren<Card>();
            _cardsPositions.Add(card.PlantId, instance.transform);
            
            card.OnClickedProperty
                .Skip(1)
                .Where(_ => card == _selectedCard.Value)
                .Select(_ => card)
                .Subscribe(AddCard)
                .AddTo(_disposable);
            
            card.OnClickedProperty
                .Skip(1)
                .Select(_ => card)
                .Subscribe(ChangeSelectedCard)
                .AddTo(_disposable);
            
            return (instance.transform, card);
        }

        private async void AddCard(Card card)
        {
            if (!_currentPlants.Cards.Contains(card))
            {
                await _cardAnimator.Animate(card.transform);

                _currentPlants.AddCard(card);

                _cardAnimator.Reset();
            }
            else
            {
                await _cardAnimator.Animate(card.transform, _cardsPositions[card.PlantId]);
                
                _currentPlants.RemoveCard(card);
                card.transform.SetParent(_cardsPositions[card.PlantId], false);
                card.transform.localPosition = Vector3.zero;
                
                _cardAnimator.Reset();
            }
        }
        
        private void ChangeSelectedCard(Card card)
        {
            if (card == _selectedCard.Value)
                return;
            
            _selectedCard.Value?.DrawOutline(false);
            card.DrawOutline(true);
            _selectedCard.Value = card;
        }

        public void Dispose() => 
            _disposable.Dispose();
    }
}
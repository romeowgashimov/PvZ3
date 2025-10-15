using System;
using _Project.Logic.Core;
using _Project.Logic.Implementation.ViewModels;
using UniRx;
using UnityEngine;

namespace _Project.Logic.Implementation.Views
{
    public class CurrentPlantsView : Window<CurrentPlantsViewModel>, IDisposable
    {
        [field:SerializeField] public Transform Parent { get; private set; }
        private CompositeDisposable _disposables = new();
        
        private void Start()
        {
            _viewModel.Cards
                .ObserveAdd()
                .Select(x => x.Value)
                .Subscribe(AddCard)
                .AddTo(_disposables);
            
            _viewModel.Cards
                .ObserveRemove()
                .Select(x => x.Value)
                .Subscribe(RemoveCard)
                .AddTo(_disposables);
        }

        private void AddCard(Card card) => 
            card.transform.SetParent(Parent, false);
        
        private void RemoveCard(Card card) => 
            card.transform.SetParent(null, false);

        protected override void Block()
        {
            
        }

        protected override void Unblock()
        {
            
        }

        public void Dispose() => 
            _disposables.Dispose();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Logic.Core;
using _Project.Logic.Implementation.ViewModels;
using UniRx;
using UnityEngine;
using static System.TimeSpan;

namespace _Project.Logic.Implementation.Views
{
    public class CurrentPlantsView : Window<CurrentPlantsViewModel>, IDisposable
    {
        private const float DELAY_FOR_ANIMATOR = 0.4f;
        
        [SerializeField] private RectTransform[] _arrayPositions;
        private CompositeDisposable _disposables = new();
        private Dictionary<Transform, bool> _positions = new();

        public IReadOnlyDictionary<Transform, bool> Positions => _positions;


        public void Awake()
        {
            foreach (RectTransform position in _arrayPositions)
                _positions.Add(position, false);
        }

        private void Start()
        {
            _viewModel.Cards
                .ObserveAdd()
                .Delay(FromSeconds(DELAY_FOR_ANIMATOR))
                .Select(x => x.Value)
                .Subscribe(AddCard)
                .AddTo(_disposables);
            
            _viewModel.Cards
                .ObserveRemove()
                .Select(x => x.Value)
                .Subscribe(RemoveCard)
                .AddTo(_disposables);
        }

        private void AddCard(Card card)
        {
            (Transform key, _) = _positions.First(x => !x.Value);
            card.transform.SetParent(key);
            _positions[key] = true;
        }

        private void RemoveCard(Card card) => 
            _positions[card.transform.parent] = false;

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
using System.Collections.Generic;
using _Project.Logic.Core;
using _Project.Logic.Implementation.ViewModels;
using UnityEngine;

namespace _Project.Logic.Implementation.Views
{
    public class CurrentPlantsView : Window<CurrentPlantsViewModel>
    {
        [SerializeField] private RectTransform[] _arrayPositions;
        private Dictionary<Transform, bool> _positions = new();

        public IReadOnlyDictionary<Transform, bool> Positions => _positions;
        
        public void Awake()
        {
            foreach (RectTransform position in _arrayPositions)
                _positions.Add(position, false);
        }

        protected override void Block()
        {
            
        }

        protected override void Unblock()
        {
            
        }
    }
}
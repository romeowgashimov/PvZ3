using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using static UnityEngine.Object;
using static UnityEngine.Resources;

namespace _Project.Logic.Core
{
    public class PlantsFactory
    {
        private Dictionary<string, Plant> _resource;
        private Transform _parent;
        private IInstantiator _instantiator;

        public PlantsFactory(Transform parent, IInstantiator instantiator)
        {
            _parent = parent;
            _instantiator = instantiator;
        }

        public void LoadResources(string[] ids)
        {
            _resource = new();

            foreach (string id in ids)
                _resource[id] = Load<Plant>($"Plants Prefabs/{id}");
        }
        
        public Plant Create(string plantId)
        {
            Plant instance = _instantiator.InstantiatePrefabForComponent<Plant>(_resource[plantId], _parent);
            
            return instance;
        }
    }
}
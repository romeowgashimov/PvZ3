using System;
using UnityEngine;

namespace _Project.Logic.Core
{
    [Serializable]
    public class PlantConfig
    {
        [field: SerializeField] public string PlantId { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
    }
}
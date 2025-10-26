using System;
using UnityEngine;

namespace _Project.Logic.Core
{
    [Serializable]
    public struct ZombieConfig
    {
        [field: SerializeField] public string ZombieId { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}
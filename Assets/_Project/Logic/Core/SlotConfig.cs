using System;
using _Project.Logic.Bootstrap;
using UnityEngine;

namespace _Project.Logic.Core
{
    [Serializable]
    public class SlotConfig
    {
        [field: SerializeField] public int SlotIndex { get; private set; }
        [field: SerializeField] public SlotType SlotType { get; private set; }
        [field: SerializeField] public Useable Useable { get; private set; }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Logic.ECS
{
    public struct Entity
    {
        public List<IComponent> Components;
        
        public GameObject Visual;
    }
}

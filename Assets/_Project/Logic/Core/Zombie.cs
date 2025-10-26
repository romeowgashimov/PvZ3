using System;
using UnityEngine;

namespace _Project.Logic.Core
{
    public class Zombie : MonoBehaviour
    {
        public event Action IsDead;
    }
}
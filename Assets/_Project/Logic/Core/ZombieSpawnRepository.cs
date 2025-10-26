using System.Collections.Generic;
using UnityEngine;

namespace _Project.Logic.Core
{
    public class ZombieSpawnRepository : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _spawnPoints;
        
        [field: SerializeField] public Transform Parent { get; private set; }
        public IReadOnlyList<GameObject> SpawnPoints => _spawnPoints;
    }
}
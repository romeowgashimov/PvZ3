using UnityEngine;

namespace _Project.Logic.Core
{
    [CreateAssetMenu(fileName = "ZombiesContainer", menuName = "Create ZombiesContainer")]
    public class ZombiesContainer : ScriptableObject
    {
        [field: SerializeField] public ZombieConfig[] ZombieConfigs { get; private set; }
        [field: SerializeField] public float TimeLevelInMinuts { get; private set; }
        [field: SerializeField] public int WaveCount { get; private set; }
    }
}
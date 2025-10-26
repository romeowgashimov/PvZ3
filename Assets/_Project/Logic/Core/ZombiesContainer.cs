using UnityEngine;

namespace _Project.Logic.Core
{
    [CreateAssetMenu(fileName = "ZombiesContainer", menuName = "Create ZombiesContainer")]
    public class ZombiesContainer : ScriptableObject
    {
        [field: SerializeField] public ZombieConfig[] ZombieConfigs { get; private set; }
    }
}
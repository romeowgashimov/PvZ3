using UnityEngine;

namespace _Project.Logic.Core
{
    [CreateAssetMenu(fileName = "PlantsContainer", menuName = "Create PlantsContainer", order = 0)]
    public class PlantsContainer : ScriptableObject
    {
        [field: SerializeField] public PlantConfig[] Plants { get;  private set; }
    }
}
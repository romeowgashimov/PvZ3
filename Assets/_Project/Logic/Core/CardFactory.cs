using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Object;

namespace _Project.Logic.Core
{
    public class CardFactory
    {
        private readonly GameObject _prefab;
        
        public CardFactory(GameObject prefab) => 
            _prefab = prefab;

        public GameObject Create(PlantConfig card)
        {
            GameObject instance = Instantiate(_prefab);
            Card child = instance.GetComponentInChildren<Card>();
            Image background = instance.GetComponentInChildren<Image>();
            
            child.SetupPlant(card.Id, card.Name, card.Color, card.Description);
            background.color = card.Color * Color.gray;
            
            return instance;
        }
    }
}
using System.Collections.Generic;

namespace _Project.Logic.Core
{
    public class CurrentPlants
    {
        private List<Card> _cards;
        private PlantsFactory _plantsFactory;
        private PlantsRepository _plantsRepository;
        private ZombieRepository _zombieRepository;
        private Dictionary<string, Stack<Plant>> _plantsPool;
        
        public int CardsCount { get; }
        public IReadOnlyCollection<Card> Cards => _cards;
        
        public CurrentPlants(int cardsCount, PlantsFactory plantsFactory, ZombieRepository zombieRepository, PlantsRepository plantsRepository)
        {
            CardsCount = cardsCount;
            _plantsFactory = plantsFactory;
            _zombieRepository = zombieRepository;
            _plantsRepository = plantsRepository;
            _cards = new(cardsCount);
            _plantsPool = new(cardsCount);
        }

        public bool TryAddCard(Card card)
        {
            if (_cards.Count >= CardsCount) 
                return false;
            
            _cards.Add(card);
            return true;
        }

        public bool TryRemoveCard(Card card) 
        {
            if (!_cards.Contains(card)) 
                return false;
            
            _cards.Remove(card);
            return true;
        }

        public void LoadPlants()
        {
            string[] ids = new string[CardsCount];
            for (int i = 0; i < CardsCount; i++)
            {
                ids[i] = _cards[i].Id;
                _plantsPool[_cards[i].Id] = new(6);
            }
                
            _plantsFactory.LoadResources(ids);

            CardViewModel cardViewModel = new(this);
            
            foreach (Card card in _cards)
                card.Setup(cardViewModel);
        }

        public Plant GetPlant(string id)
        {
            Plant plant;
            
            if (_plantsPool[id].Count != 0)
            {
                plant = _plantsPool[id].Pop();
                plant.gameObject.SetActive(true);
            }
            else
            {
                plant = _plantsFactory.Create(id);
                
                plant.OnPlaced += Register;
                
                void Register(Line line)
                {
                    plant.OnPlaced -= Register;
                    
                    //Нужно находить расстояние между растением и зомби
                    bool startCheck = _zombieRepository.TryFindZombie(line,  out Zombie _);
                    plant.SetEnemy(startCheck);
                    
                    _zombieRepository.OnZombieSpawned[(int)line] += plant.SetEnemy;
                    _plantsRepository.Register(plant);
                }
            
                plant.OnDead += Unregister;

                void Unregister(Line line)
                {
                    plant.OnDead -= Unregister;
                    
                    _zombieRepository.OnZombieSpawned[(int)line] -= plant.SetEnemy;
                    _plantsRepository.Unregister(plant);
                }
            }

            return plant;
        }

        public void Release(string id, Plant currentPlant)
        {
            currentPlant.gameObject.SetActive(false);
            _plantsPool[id].Push(currentPlant);
        }
    }
}
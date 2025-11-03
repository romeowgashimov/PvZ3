using _Project.Logic.Core;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class GardenInstaller : MonoInstaller
    {
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private PlantsContainer _plantsContainer;
        [SerializeField] private SlotsRepository _slotsRepository;
        [SerializeField] private SlotsContainer _slotsContainer;
        [SerializeField] private Transform _garden;
        [SerializeField] private ZombieSpawnRepository _zombieSpawnRepository;
        [SerializeField] private ZombiesContainer _zombiesContainer;
        [SerializeField] private Canvas _canvasForScaleFactor;
        //[SerializeField] private LevelConfig _levelConfig;
        //[SerializeField] private playerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<WaveCounter>().AsSingle().WithArguments(_zombiesContainer.TimeLevelInMinuts, _zombiesContainer.WaveCount);
            Container.Bind<ZombieRepository>().AsSingle();
            Container.Bind<PlantsRepository>().AsSingle();
            Container.Bind<WindowsFactory>().AsSingle().WithArguments(_rootTransform);
            Container.Bind<SlotsSystem>().AsSingle().WithArguments(_slotsRepository, _slotsContainer);
            Container.Bind<ZombieSystem>().AsSingle().WithArguments(_zombieSpawnRepository, _zombiesContainer);
            Container.Bind<PlantsFactory>().AsSingle().WithArguments(_garden);
            Container.Bind<Canvas>().FromInstance(_canvasForScaleFactor);
            
            //if (levelConfig.OtherSettings == ChoicePlantLevel)
            Container.Bind<WindowsSystem>().To<ChoiceWindowsSystem>().AsSingle().WithArguments(_garden);
            //else
            //Container.Bind<WindowsSystem>().To<OtherWindowsSystem>().AsSingle();
            
            //3 = playerConfig.CardsCount
            Container.Bind<CurrentPlants>().AsSingle().WithArguments(1);
            Container.Bind<GameObject>().FromInstance(_cardPrefab);
            Container.Bind<PlantsContainer>().FromInstance(_plantsContainer);
            
            Container.BindInterfacesTo<GardenEntryPoint>().AsSingle();
        }
    }
}
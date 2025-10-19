using _Project.Logic.Core;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class GardenInstaller : MonoInstaller
    {
        [SerializeField] private Transform _canvas;
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private PlantsContainer _plantsContainer;
        [SerializeField] private SlotsRepository _slotsRepository;
        [SerializeField] private SlotsContainer _slotsContainer;
        [SerializeField] private Transform _garden;
        //[SerializeField] private LevelConfig _levelConfig;
        //[SerializeField] private playerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<WindowsFactory>().AsSingle().WithArguments(_canvas);
            Container.Bind<SlotsSystem>().AsSingle().WithArguments(_slotsRepository, _slotsContainer);
            
            
            //if (levelConfig.OtherSettings == ChoicePlantLevel)
            Container.Bind<WindowsSystem>().To<ChoiceWindowsSystem>().AsSingle().WithArguments(_garden);
            //else
            //Container.Bind<WindowsSystem>().To<OtherWindowsSystem>().AsSingle();
            
            //5 = playerConfig.CardsCount
            Container.Bind<CurrentPlants>().AsSingle().WithArguments(3);
            Container.Bind<GameObject>().FromInstance(_cardPrefab);
            Container.Bind<PlantsContainer>().FromInstance(_plantsContainer);
            
            Container.BindInterfacesTo<GardenEntryPoint>().AsSingle();
        }
    }
}
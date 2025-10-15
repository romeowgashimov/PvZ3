using _Project.Logic.Core;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class GardenSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _canvas;
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private PlantsContainer _plantsContainer;

        public override void InstallBindings()
        {
            Container.Bind<WindowsFactory>().AsSingle().WithArguments(_canvas);
            Container.Bind<WindowsSystem>().AsSingle();
            Container.Bind<CurrentPlants>().AsSingle();
            Container.Bind<GameObject>().FromInstance(_cardPrefab);
            Container.Bind<PlantsContainer>().FromInstance(_plantsContainer);
            
            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }
    }
}
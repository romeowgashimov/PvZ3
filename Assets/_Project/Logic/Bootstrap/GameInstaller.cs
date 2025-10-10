using _Project.Logic.Core;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Bootstrap
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _canvas;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private PlantsContainer _plantsContainer;

        public override void InstallBindings()
        {
            Container.Bind<WindowsFactory>().AsSingle().WithArguments(_canvas);
            Container.Bind<WindowsSwitcher>().AsSingle();
            Container.Bind<Card>().FromInstance(_cardPrefab);
            Container.Bind<PlantsContainer>().FromInstance(_plantsContainer);
            
            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }
    }
}
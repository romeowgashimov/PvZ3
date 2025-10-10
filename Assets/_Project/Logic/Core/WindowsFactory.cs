using UnityEngine;
using Zenject;

namespace _Project.Logic.Core
{
    public class WindowsFactory
    {
        private readonly Transform _canvas;
        private readonly IInstantiator _instantiator;

        public WindowsFactory(Transform canvas, IInstantiator instantiator)
        {
            _canvas = canvas;
            _instantiator = instantiator;
        }

        public Window<TViewModel> Create<TView, TViewModel>() where TView : Window<TViewModel>
        {
            TView window = _instantiator.InstantiatePrefabResourceForComponent<TView>($"UI Prefabs/{typeof(TView).Name}", _canvas); 
            TViewModel viewModel = _instantiator.Instantiate<TViewModel>();
            window.Setup(viewModel);

            return window;
        }
    }
}
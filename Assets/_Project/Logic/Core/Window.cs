using _Project.Logic.Implementation.WindowAnimators;
using UnityEngine;

namespace _Project.Logic.Core
{
    public abstract class Window<T> : MonoBehaviour 
    {
        [SerializeField] protected WindowAnimator _windowAnimator;

        protected T _viewModel;

        protected void Awake()
        {
            gameObject.SetActive(false);
            
            if (TryGetComponent(out _windowAnimator))
                return;

            _windowAnimator = gameObject.AddComponent<DefaultWindowAnimator>();
        }
        
        public void Setup(T viewModel) => 
            _viewModel = viewModel;

        public async void Show()
        {
            _windowAnimator.Prepare();

            Block();

            await _windowAnimator.Show();
            
            Unblock();
        }

        public async void Hide()
        {
            Block();

            await _windowAnimator.Hide();
        }
        
        protected abstract void Block();

        protected abstract void Unblock();
    }
}
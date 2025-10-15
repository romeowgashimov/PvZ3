namespace _Project.Logic.Core
{
    public class WindowsSystem
    {
        private readonly WindowsFactory _windowsFactory;

        public WindowsSystem(WindowsFactory windowsFactory)
        {
            _windowsFactory = windowsFactory;
        }

        public TView CreateAndShow<TView, TViewModel>() where TView : Window<TViewModel>
        {
            Window<TViewModel> view = _windowsFactory.Create<TView,  TViewModel>();      
            view.Show();
            
            return (TView)view;
        }
    }
}
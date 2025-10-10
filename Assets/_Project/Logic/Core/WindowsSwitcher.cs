namespace _Project.Logic.Core
{
    public class WindowsSwitcher
    {
        private readonly WindowsFactory _windowsFactory;

        public WindowsSwitcher(WindowsFactory windowsFactory)
        {
            _windowsFactory = windowsFactory;
        }

        public void Switch<TView, TViewModel>() where TView : Window<TViewModel>
        {
            Window<TViewModel> view = _windowsFactory.Create<TView,  TViewModel>();      
            view.Show();
        }
    }
}
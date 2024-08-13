using TestingSystem.Navigation;

namespace TestingSystem
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            InitializeComponent();
            MainPage = new NavigationPage();
            navigationService.NavigateToMainPageAsync();
        }
        protected override Window CreateWindow(IActivationState activationState) =>
     new Window(MainPage)
     {
         MaximumHeight = 700,
         MaximumWidth = 1200,
         MinimumHeight = 700,
         MinimumWidth = 1200,

     };
    }
}

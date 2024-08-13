using TestingSystem.ViewModel;

namespace TestingSystem
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _mainViewModel;
    
        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            BindingContext = mainViewModel;
            _mainViewModel = mainViewModel;
        }
    }

}

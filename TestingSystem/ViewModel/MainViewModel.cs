using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Navigation;
using TestingSystem.Service.Interface;
using TestingSystem.ViewModel.Base;

namespace TestingSystem.ViewModel
{
    public sealed partial class MainViewModel:ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;

        public MainViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
        }
    }
}

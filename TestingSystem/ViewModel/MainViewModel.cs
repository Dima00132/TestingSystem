using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Model;
using TestingSystem.Navigation;
using TestingSystem.Service.Interface;
using TestingSystem.ViewModel.Base;

namespace TestingSystem.ViewModel
{
    public sealed partial class MainViewModel:ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;

        [ObservableProperty]
        public TestDisplayer _testDisplayer;

        public MainViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            TestDisplayer = localDbService.GetTestDisplayer();
        }

        public RelayCommand AddQuestionTestCommand => new(async () =>
        {
            await _navigationService.NavigateByViewModelAsync<AddQuestionTestViewModel>(_testDisplayer);
        });
        public RelayCommand<QuestionTest> TapCommand => new(async (questionTest) =>
        {
            await _navigationService.NavigateByViewModelAsync<PassingTestViewModel>(questionTest);
        });

 
    }
}

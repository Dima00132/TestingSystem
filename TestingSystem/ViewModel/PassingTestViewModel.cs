using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TestingSystem.Model;
using TestingSystem.Navigation;
using TestingSystem.Service.Interface;
using TestingSystem.ViewModel.Base;

namespace TestingSystem.ViewModel
{
    public sealed partial class PassingTestViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;
        private Test _test;

        [ObservableProperty]
        public Stack<QuestionTest> _questionTests;

        [ObservableProperty]
        private QuestionTest _questionTest;

        [ObservableProperty]
        public bool _isIsVisibleQuestion = true;


        public PassingTestViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;

        }

        
        [RelayCommand]
        public void NextQuestion()
        {
            if (QuestionTests.Count != 0)
            {
                QuestionTest = QuestionTests.Pop();
            }
            IsIsVisibleQuestion = false;
        }

        public override Task OnNavigatingToAsync(object parameter, object parameterSecond = null)
        {
            if (parameter is Test test)
            {
                _test = test;
                QuestionTests = new Stack<QuestionTest>( test.QuestionTests);
                QuestionTest = QuestionTests.Pop();
            }
            return base.OnNavigatingToAsync(parameter, parameterSecond);
        }

    }
}
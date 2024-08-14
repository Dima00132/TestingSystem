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
        public Stack<QuestionTest> _stackQuestionTests;

        [ObservableProperty]
        private QuestionTest _questionTest;

        [ObservableProperty]
        public string _statistics;

        [ObservableProperty]
        public int _questionNumber = 0;
        [ObservableProperty]
        public int _questionTestsCount;
        
        public PassingTestViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;

        }

        
        [RelayCommand]
        public void NextQuestion()
        {
            if (StackQuestionTests.Count != 0)
            {
                QuestionTest = StackQuestionTests.Pop();
                QuestionNumber++;
                return;
                
            }
            Statistics= _test.GetStatistics();
        }

        public override Task OnNavigatingToAsync(object parameter, object parameterSecond = null)
        {
            if (parameter is Test test)
            {
                _test = test;
                StackQuestionTests = new Stack<QuestionTest>( test.QuestionTests);
                QuestionTestsCount = test.QuestionTests.Count;
                NextQuestion();
            }
            return base.OnNavigatingToAsync(parameter, parameterSecond);
        }

    }
}
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TestingSystem.Model;
using TestingSystem.Navigation;
using TestingSystem.Service.Interface;
using TestingSystem.ViewModel.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestingSystem.ViewModel
{
    public sealed partial class AddQuestionTestViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;
        private readonly IPopupService _popupService;

        private TestDisplayer _testDisplayer;


        [ObservableProperty]
        private QuestionTest _question = new();

        [ObservableProperty]
        private ObservableCollection<QuestionTest> _questionTests = [];

      
        public AddQuestionTestViewModel(INavigationService navigationService, ILocalDbService localDbService,IPopupService popupService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            _popupService = popupService;
        }

    
        [RelayCommand]
        public  void DeleteAnswerOptions(AnswerOption answerOption)
        {
            Question.AnswerOptions.Remove(answerOption);
        }


        [RelayCommand]
        public void DeleteQuestionTest(QuestionTest questionTest)
        {
            QuestionTests.Remove(questionTest);
        }


        
        [RelayCommand]
        public void  AddAnswerOptions()
        {
            Question.AnswerOptions.Add(new AnswerOption());
        }

        [RelayCommand]
        public void AddQuestionTest()
        {
            if(string.IsNullOrEmpty(Question.Question))
            {
                Application.Current.MainPage.DisplayAlert("", $"Заполните поле \"Вопрос\"", "ОK");
                return;
            }    
            QuestionTests.Add(Question);
            Question = new QuestionTest();
        }


        [RelayCommand]  
        
        public async Task Save()
        {
            if (QuestionTests.Count == 0)
            {
                Application.Current.MainPage.DisplayAlert("", $"Добавьте \"Вопрос\"", "ОK");
                return;
            }

            await _popupService
             .ShowPopupAsync<SavingTestViewModel>(onPresenting: viewModel => viewModel.AddTest( QuestionTests, _localDbService, _testDisplayer))
             .ConfigureAwait(false);
            await _navigationService.NavigateBackAsync();
            
        }

        public override Task OnNavigatingToAsync(object parameter, object parameterSecond = null)
        {
            if (parameter is TestDisplayer testDisplayer)
            {
                _testDisplayer = testDisplayer;
            }
            return base.OnNavigatingToAsync(parameter, parameterSecond);
        }

    }
}
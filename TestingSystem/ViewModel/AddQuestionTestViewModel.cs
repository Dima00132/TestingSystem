using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TestingSystem.Model;
using TestingSystem.Navigation;
using TestingSystem.Service.Interface;
using TestingSystem.ViewModel.Base;


namespace TestingSystem.ViewModel
{
    public sealed partial class AddQuestionTestViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;
        private readonly IPopupService _popupService;
        private TestDisplayer _testDisplayer;
        [ObservableProperty]
        private QuestionTest _question = new() { AnswerOptions = [new AnswerOption(),new AnswerOption()] };
        [ObservableProperty]
        private ObservableCollection<QuestionTest> _questionTests = [];
        public bool IsAnswerOptionsAreFilledIn => CheckingQuestionsFilledOut();
        public int CountAnswerOptions => Question.AnswerOptions.Count;


        public AddQuestionTestViewModel(INavigationService navigationService, ILocalDbService localDbService,IPopupService popupService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            _popupService = popupService;
        }

        [RelayCommand]
        public  void DeleteAnswerOptions(AnswerOption answerOption)
        {
            if (CountAnswerOptions <= 2)
            {
                Application.Current.MainPage.DisplayAlert("Предупреждение", $"Минимальное количество вариантов ответа 2", "ОK");
                return;
            }
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
            QuestionTests.Add(Question);
            Question =  new() { AnswerOptions = [new AnswerOption(), new AnswerOption()] };
        }

        private bool CheckingQuestionsFilledOut()
        {
            return CountAnswerOptions >= 2 && Question.AnswerOptions.Count(x => !string.IsNullOrEmpty(x.Answer)) == CountAnswerOptions;
        }

        private void CheckingAnswerOptionForCompletion(ObservableCollection<QuestionTest> questionTests)
        {
            for (int i = 0; i < questionTests.Count; i++)
                if (string.IsNullOrEmpty(questionTests[i].Question))
                    questionTests.RemoveAt(i);
        }

        [RelayCommand]  
        public async Task Save(ContentPage contentPage)
        {
            if (QuestionTests.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("", $"Добавьте \"Вопрос\"", "ОK");
                return;
            }
            CheckingAnswerOptionForCompletion(QuestionTests);
             await _popupService
             .ShowPopupAsync<SavingTestViewModel>(onPresenting: viewModel => viewModel.AddTest( QuestionTests, _localDbService, _testDisplayer))
             .ConfigureAwait(false);
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
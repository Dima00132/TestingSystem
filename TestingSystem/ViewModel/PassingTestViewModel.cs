using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TestingSystem.Model;
using TestingSystem.Navigation;
using TestingSystem.Service.ExcelServise;
using TestingSystem.Service.Interface;
using TestingSystem.ViewModel.Base;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Alerts;

namespace TestingSystem.ViewModel
{
    public sealed partial class PassingTestViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IRecordExcel _recordXlsx;
        private readonly ILocalDbService _localDbService;
        private Test _test;
        [ObservableProperty]
        public Stack<QuestionTest> _stackQuestionTests;
        [ObservableProperty]
        private ObservableCollection<QuestionTest> _passedTests = [];
        [ObservableProperty]
        private QuestionTest _questionTest;
        [ObservableProperty]
        public string _statistics;
        [ObservableProperty]
        private bool _isTestPassed = false;
        [ObservableProperty]
        public int _questionNumber = 0;
        [ObservableProperty]
        public int _questionTestsCount;

        [ObservableProperty]
        private int _correctAnswer = 0;
        [ObservableProperty]
        private int _wrongAnswer = 0;


        public PassingTestViewModel(INavigationService navigationService, IRecordExcel recordXlsx, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _recordXlsx = recordXlsx;
            _localDbService = localDbService;
        }    

        [RelayCommand]
        public void Exit()
        {
            _navigationService.NavigateBack();
        }

        [RelayCommand]
        public async void ExportToExcel()
        {

            var fullName =  await Application.Current.MainPage.DisplayPromptAsync("", $"Введите Ваше имя и фамилию", "ОK","Отмена");
            if (string.IsNullOrEmpty(fullName))
                return;
            RecordXlsx(fullName);
        }

        private async void RecordXlsx(string fullName)
        {
            using var stream = new MemoryStream();
            var fileSaveResult = await FileSaver.Default.SaveAsync("NameFile.xlsx", stream);
            if (fileSaveResult.IsSuccessful)
            {
                try
                {
                    _recordXlsx.Record(null, fileSaveResult.FilePath);
                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "Ок");
                }
                Application.Current.MainPage.DisplayAlert("Файл сохранен!", $"{fileSaveResult.FilePath}", "Ок");
            }
        }


        [RelayCommand]
        public void NextQuestion()
        {
            if (StackQuestionTests.Count != 0)
            {
                QuestionTest = StackQuestionTests.Pop();
                PassedTests.Add(QuestionTest);
                QuestionNumber++;
                return;  
            }
            IsTestPassed = true;
            CorrectAnswer = _test.QuestionTests.Count(x => x.DetermineWhetherAnswerIsCorrectOrNot());
            var count = QuestionTestsCount;
            WrongAnswer = count - CorrectAnswer;
        }

        public override Task OnNavigatingToAsync(object parameter, object parameterSecond = null)
        {
            if (parameter is Test test)
            {
                _test = test;
                StackQuestionTests = new Stack<QuestionTest>(test.QuestionTests);
                QuestionTestsCount = test.QuestionTests.Count;
                NextQuestion();
            }
            return base.OnNavigatingToAsync(parameter, parameterSecond);
        }

    }
}
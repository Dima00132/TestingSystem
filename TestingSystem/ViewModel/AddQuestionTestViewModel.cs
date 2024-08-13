﻿using CommunityToolkit.Mvvm.ComponentModel;
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

        private TestDisplayer _testDisplayer;


        [ObservableProperty]
        private QuestionTest _question = new QuestionTest();

        [ObservableProperty]
        private ObservableCollection<QuestionTest> _questionTests = new ObservableCollection<QuestionTest>();

      
        public AddQuestionTestViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService; 
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
            //var newTest = new Test()
            //_testDisplayer.Tests.Add()


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
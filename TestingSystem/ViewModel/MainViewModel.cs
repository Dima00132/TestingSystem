using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly TestDisplayer _testDisplayer;


        [ObservableProperty]
        private ObservableCollection<Test> _tests;

        [ObservableProperty]
        private ObservableCollection<object> _selected = [];

        [ObservableProperty]
        private ObservableCollection<string> _categorys;
        
        [ObservableProperty]
        private Category _currentCategory;

        public MainViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;
            _testDisplayer = localDbService.GetTestDisplayer();
            SortTests(_testDisplayer);
            Categorys = _testDisplayer.Categorys.Select(x=>x.NameCategory).ToObservableCollection();
        }


        public RelayCommand<string> PerformSearchCommand => new(async (string testName) => await OnSearchTextChangedAsync(testName));
        private async Task OnSearchTextChangedAsync(object keyword)
        {
            var testName = keyword as string;
            if (string.IsNullOrEmpty(testName))
            {
                SortTests(_testDisplayer);
                return;
            }
            if (!string.IsNullOrEmpty(testName) && testName.Length >= 1)
            {
                var data = await Task.FromResult(_testDisplayer.FindsNameTestByRequest(testName)).ConfigureAwait(false);
                if (data is not null)
                    Tests = new ObservableCollection<Test>(data);
            }
        }

        public RelayCommand AddQuestionTestCommand => new(async () =>
        {
            await _navigationService.NavigateByViewModelAsync<AddQuestionTestViewModel>(_testDisplayer);
            SortTests(_testDisplayer);
        });

        public RelayCommand<Test> DeleteCommand => new((test) =>
        {
            _testDisplayer.Tests.Remove(test);
            SortTests(_testDisplayer);
            _localDbService.Update(_testDisplayer);
        });


        public RelayCommand RunSelectedTestsCommand => new(async () =>
        {
            if (Selected.Count == 0)
                return;
            var newTest = new Test() { NameTest = "Сборка тестов" };
            var selectedTests = Selected.OfType<Test>().SelectMany(x=>x.QuestionTests).ToObservableCollection();
            foreach (var item in selectedTests)
                newTest.AddQuestionTest(item);
            await _navigationService.NavigateByViewModelAsync<PassingTestViewModel>(newTest.Clone());
        });
        public RelayCommand<Test> TapCommand => new(async (test) =>
        {
            await _navigationService.NavigateByViewModelAsync<PassingTestViewModel>(test.Clone());
        });

        private void SortTests(TestDisplayer testDisplayer)
        {
            Tests = _testDisplayer.SortedTestById().GetTests();
        }
    }
}

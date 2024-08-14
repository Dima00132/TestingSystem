using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestingSystem.Model;
using TestingSystem.Navigation;
using TestingSystem.Service.Interface;
using TestingSystem.ViewModel.Base;

namespace TestingSystem.ViewModel
{
    public sealed partial class SavingTestViewModel : ViewModelBase
    {
        private readonly IPopupService _popupService;
        private readonly INavigationService _navigationService;
        private ILocalDbService _localDbService;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCommand))]
        private string _nameTest;
        [ObservableProperty]
        private string _category;
        [ObservableProperty]
        private ObservableCollection<Category> _categorys;
        private ObservableCollection<QuestionTest> _questionTests;
        private TestDisplayer _testDisplayer;

        public SavingTestViewModel(IPopupService popupService,INavigationService navigationService)
        {
            _popupService = popupService;
            _navigationService = navigationService;
          
        }
     
        [RelayCommand(CanExecute = nameof(CheckName))]
        public void Add(Popup popup)
        {
            var newCategory = new Category(Category);
            var newTest = new Test(newCategory, NameTest);
            newTest.QuestionTests = _questionTests;
            _testDisplayer.AddCategory(newCategory);
            _testDisplayer.Tests.Add(newTest);
            SaveBb(newCategory, newTest);
            popup.Close();
            _navigationService.NavigateBack();
        }

        private void SaveBb(Category newCategory,Test newTest)
        {
            _localDbService.Create(newCategory);
            _localDbService.Create(newTest);
            _localDbService.Update(_testDisplayer);
        }
        public bool CheckName() => !string.IsNullOrEmpty(NameTest);
        
        public void AddTest(ObservableCollection<QuestionTest> questionTests, ILocalDbService localDbService, TestDisplayer testDisplayer)
        {
            _localDbService = localDbService;
            _questionTests = questionTests;
            _testDisplayer = testDisplayer;
            Categorys = testDisplayer.Categorys;
        }
    }
}

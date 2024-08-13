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

        private TestDisplayer _testDisplayer;

        public ObservableCollection<QuestionTest> QuestionTests = new ObservableCollection<QuestionTest>();
        public AddQuestionTestViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService; 
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
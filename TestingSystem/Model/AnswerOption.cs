using CommunityToolkit.Mvvm.ComponentModel;

namespace TestingSystem.Model
{
    public sealed partial class AnswerOption:ObservableObject
    {
        public AnswerOption(string answer, bool isCorrect = false)
        {
            Answer = answer;
            IsCorrect = isCorrect;
        }

        public AnswerOption():this("")
        {
        }

        [ObservableProperty]
        private string _answer;

        [ObservableProperty]
        private bool _isCorrect;

    }
}

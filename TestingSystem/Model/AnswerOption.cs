using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace TestingSystem.Model
{
    public enum AnswerChoice
    {
        Correct,
        Incorrect,
        NotSelected
    }


    [Table(nameof(AnswerOption))]
    public sealed partial class AnswerOption:ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int AnswerOptionId { get; set; }
        public string SomethingAdditional { get; set; }

        [Column("QuestionTest_id")]
        [ForeignKey(typeof(QuestionTest))]
        public int QuestionTestId { get; set; }

        public AnswerOption(string answer, AnswerChoice correct = AnswerChoice.NotSelected)
        {
            Answer = answer;
            Correct = correct;
            Selected = AnswerChoice.NotSelected;
        }

        public AnswerOption():this("")
        {
        }

        [ObservableProperty]
        private string _answer;


        [ObservableProperty]
        private AnswerChoice _correct;

        [ObservableProperty]
        private AnswerChoice _selected;


        //[ObservableProperty]
        //private bool _isCorrect;

        //[ObservableProperty]
        //private bool _isSelected;



        public bool IsCorrectAnswer=> Correct == AnswerChoice.Correct &  Selected == AnswerChoice.Correct;
      

    }
}

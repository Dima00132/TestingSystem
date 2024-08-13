using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace TestingSystem.Model
{
    [Table(nameof(AnswerOption))]
    public sealed partial class AnswerOption:ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int AnswerOptionId { get; set; }

        [Column("QuestionTest_id")]
        [ForeignKey(typeof(QuestionTest))]
        public int QuestionTestId { get; set; }

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

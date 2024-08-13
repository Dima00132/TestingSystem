using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace TestingSystem.Model
{
    [Table(nameof(QuestionTest))]
    public sealed partial class QuestionTest : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int QuestionTestId { get; set; }

        [Column("test_id")]
        [ForeignKey(typeof(Test))]
        public int TestId { get; set; }

        private ObservableCollection<AnswerOption> _answerOptions = [];

        [Column("answer_options")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<AnswerOption> AnswerOptions
        {
            get => _answerOptions;
            set => SetProperty(ref _answerOptions, value);
        }

        [ObservableProperty]
        private string _question;

    }
}

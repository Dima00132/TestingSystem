using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace TestingSystem.Model
{


    [Table(nameof(AnswerOption))]
    public sealed partial class AnswerOption:ObservableObject, ICloneable<AnswerOption>
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int AnswerOptionId { get; set; }
        public string SomethingAdditional { get; set; }

        [Column("QuestionTest_id")]
        [ForeignKey(typeof(QuestionTest))]
        public int QuestionTestId { get; set; }

        [ObservableProperty]
        private string _answer;

        [ObservableProperty]
        private Selector _correct;

        [ObservableProperty]
        private Selector _selected = Selector.NoValueSelected;

        private Dictionary<Selector, Func<Selector, Selector, Selector>> keyValuePairs = new()
        {
            [Selector.CorrectValue] = (Correct, Selected) => Selector.CorrectValue,
            [Selector.NoValueSelected] = (Correct, Selected) => Selected == Selector.CorrectValue ? Selector.IncorrectValue : Selector.NoValueSelected
        };

        public Selector IsCorrectAnswer
        {
            get
            {
                return keyValuePairs[Correct].Invoke(Correct, Selected);
            }
            set { }
        }

        public bool IsCorrect
        {
            get
            {
                return Correct == Selector.CorrectValue;
            }
            set { }
        }

        public AnswerOption(string answer, Selector correct = Selector.NoValueSelected)
        {
            Answer = answer;
            Correct = correct;
        }

        public AnswerOption() : this("")
        {
        }

        public AnswerOption Clone()
        {
            return new AnswerOption(Answer, Correct) { Selected = Selected };
        }
    }
}

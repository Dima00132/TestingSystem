using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Model
{

    public interface ICloneable<T> where T : class
    {
        public T Clone();
    }


    [Table(nameof(Test))]
    public sealed partial class Test : ObservableObject,ICloneable<Test>
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int TestId { get; set; }
        public string SomethingAdditional { get; set; }

        [Column("test_displayer_id")]
        [ForeignKey(typeof(TestDisplayer))]
        public int TestDisplayerId { get; set; }


        private Category _category = new();


        [Column("category")]
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }



        public Test(Category category,string nameTest)
        {
            Category = category;
            _nameTest = nameTest;
        }

        public Test()
        {
        }

      
        private string _nameTest;
        [Column("NameTest")]
        public string NameTest
        {
            get => _nameTest;
            set => SetProperty(ref _nameTest, value);
        }

        private ObservableCollection<QuestionTest> _questionTests = [];
        [Column("question_tests")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<QuestionTest> QuestionTests
        {
            get => _questionTests;
            set => SetProperty(ref _questionTests, value);
        }

        public void AddQuestionTest(QuestionTest questionTest)
        {
            QuestionTests.Insert(0, questionTest);
        }

        public Test Clone()
        {
            var testClone = new Test() { Category = Category ,NameTest =NameTest , QuestionTests = new ObservableCollection<QuestionTest>() };
        
            foreach (var item in QuestionTests)
                testClone.QuestionTests.Add(item.Clone());

            return testClone;
        }

        public string GetStatistics()
        {
            var correctAnswer = 0;
            var wrongAnswer = 0;
            foreach (var item in QuestionTests)
            {
                if(item.DetermineWhetherAnswerIsCorrectOrNot())
                {
                    correctAnswer++;
                    continue;
                }
                wrongAnswer++;
            }
            return $"Всего вопросов было {QuestionTests.Count} из них правильных {correctAnswer} не правильных {wrongAnswer}";
        }

    }
}

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


    [Table("test")]
    public sealed partial class Test : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [ObservableProperty]
        public string _category;

        [Column("test_displayer_id")]
        [ForeignKey(typeof(TestDisplayer))]
        public int TestDisplayerId { get; set; }

        public Test(string category)
        {
            Category = category;
        }

        public Test()
        {
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
            QuestionTests.Add(questionTest);
        }


    }
}

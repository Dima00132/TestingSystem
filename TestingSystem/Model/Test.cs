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


    [Table(nameof(Test))]
    public sealed partial class Test : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int TestId { get; set; }

        [Column("test_displayer_id")]
        [ForeignKey(typeof(TestDisplayer))]
        public int TestDisplayerId { get; set; }


        private Category _category;


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
            QuestionTests.Add(questionTest);
        }


    }
}

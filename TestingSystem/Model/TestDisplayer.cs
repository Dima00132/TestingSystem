using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace TestingSystem.Model
{
    [Table("Category")]
    public sealed partial class Category : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("test_displayer_id")]
        [ForeignKey(typeof(TestDisplayer))]
        public int TestDisplayerId { get; set; }

        public Category(string nameCategory)
        {
            NameCategory = nameCategory;
        }

        public string NameCategory { get; set; }
    }

    [Table("test_displayer")]
    public sealed partial class TestDisplayer : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [ObservableProperty]
        private string _nameTest;



   

        public TestDisplayer()
        {
        }

        private ObservableCollection<Test> _tests = [];

        [Column("_tests")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Test> Tests
        {
            get => _tests;
            set => SetProperty(ref _tests, value);
        }


        [Column("Categorys")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Category> Categorys { get; set; }
       
    

    public void AddCategory(Category nameCategory)
        {
            if (Categorys.Contains(nameCategory))
                return;
            Categorys.Add(nameCategory);
        }

    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace TestingSystem.Model
{

    [Table(nameof(TestDisplayer))]
    public sealed partial class TestDisplayer : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int TestDisplayerId { get; set; }
        public string SomethingAdditional { get; set; }
        public TestDisplayer()
        {
        }



        [Column("tests")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Test> Tests { get; set; } = [];


        [Column("categorys")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Category> Categorys { get; set; } = [];



        public void AddCategory(Category nameCategory)
        {
            if (Categorys.Contains(nameCategory))
                return;
            Categorys.Add(nameCategory);
        }

    }
}

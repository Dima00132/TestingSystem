using CommunityToolkit.Maui.Core.Extensions;
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


        public TestDisplayer SortedTestById()
        {
            Tests = Tests.OrderByDescending((x) => x.TestId).ToObservableCollection();
            return this;
        }

        public IEnumerable<Test> FindsNameTestByRequest(string request)
        {
            var result = Tests
                .Where(x => x.NameTest.Length >= request.Length)
                .Where(x => CompareUser(x.NameTest, request));
            return result;
        }
        private bool CompareUser(string name, string request)
            => String.Compare(name, 0, request, 0, request.Length, StringComparison.OrdinalIgnoreCase) == 0;
        public void Remove(Test test) => Tests?.Remove(test);
        public ObservableCollection<Test> GetTests() => Tests;
        public void AddCategory(Category nameCategory)
        {
            if (Categorys.Contains(nameCategory))
                return;
            Categorys.Add(nameCategory);
        }

    }
}

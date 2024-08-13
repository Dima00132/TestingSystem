using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TestingSystem.Model
{
    [Table(nameof(Category))]
    public sealed partial class Category : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int CategoryId { get; set; }

        [Column("test_displayer_id")]
        [ForeignKey(typeof(TestDisplayer))]
        public int TestDisplayerId { get; set; }

        [Column("test_id")]
        [ForeignKey(typeof(Test))]
        public int TestId { get; set; }

        [ObservableProperty]
        private string _nameCategory;

        public Category(string nameCategory)
        {
            NameCategory = nameCategory;
        }

        
    }
}

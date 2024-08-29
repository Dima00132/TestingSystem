using Aspose.Cells;

namespace TestingSystem.Service.ExcelServise
{
    public interface IRecordExcel
    {
        public void Record(List<string[]> strings, Stream stream);
        public void Record(List<string[]> strings, string filePath);
    }
}
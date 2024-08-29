using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Threading.Tasks;

namespace TestingSystem.Service.ExcelServise
{   
    public sealed class RecordExcel: IRecordExcel

    {
        public void Record(List<string[]> strings, Stream stream)
        {
            using Workbook workbook = new Workbook(stream);
            RecordXlsxFile(strings, workbook);
            workbook.Save(stream, SaveFormat.Xlsx);
        }
        public void Record(List<string[]> strings, string filePath)
        {
            using Workbook workbook = new Workbook(filePath);
            RecordXlsxFile(strings, workbook);
            workbook.Save(filePath, SaveFormat.Xlsx);
        }

        private void RecordXlsxFile(List<string[]> strings, Workbook workbook)
        {
            Worksheet sheet = workbook.Worksheets[0];
            for (int i = 0; i < strings.Count; i++)
            {
                for (int y = 0; y < strings[i].Length; y++)
                {
                    Aspose.Cells.Cell cell = sheet.Cells[i, y];
                    cell.PutValue(strings[i][y]);
                }
            }
        }


    }

}

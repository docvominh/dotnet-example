using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excel_closedXML
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create excel file in memory
            XLWorkbook file = new XLWorkbook();
            // Add Sheet
            file.Worksheets.Add("First Sheet");
            // Get Sheet
            IXLWorksheet sheet = file.Worksheet("First Sheet");
            // Add cell value
            sheet.Cell("A1").Value = "I am A1";
            sheet.Cell("A3").Value = "I am A3";
            // Add some style
            sheet.ColumnWidth = 30;
            sheet.Row(1).Style.Fill.BackgroundColor = XLColor.Green;

            // Save file to hard disk
            file.SaveAs(@"D:\closeXML-example.xlsx");

            // Release resource
            sheet.Dispose();
            file.Dispose();

            // Open excel file
            XLWorkbook fileOpen = new XLWorkbook(@"D:\closeXML-example.xlsx");
            IXLWorksheet openSheet = file.Worksheet("First Sheet");

            Console.WriteLine(string.Format("Total used row (Contain blank row) = {0}", openSheet.LastRowUsed().RowNumber()));
            Console.WriteLine(string.Format("Total used row  = {0}", openSheet.RowsUsed().Count()));
            Console.WriteLine(string.Format("Value of Cell A1  = {0}", openSheet.Cell("A1").Value));


            Console.Read();
        }
    }
}

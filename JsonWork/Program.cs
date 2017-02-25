using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace JsonWork
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Instrument> bloombergList = new List<Instrument>();
            List<Instrument> manunalList = new List<Instrument>();

            CultureInfo sg = new CultureInfo("zh-SG");
            string format = "yyyy-MM-dd";

            string content = File.ReadAllText("data.json");
            var listBloomBergJson = JsonConvert.DeserializeObject<List<InstrumentJson>>(content);

            foreach (var item in listBloomBergJson)
            {
                bloombergList.Add(
                    new Instrument
                    {
                        Date = DateTime.ParseExact(item.Date, format, sg),
                        Data = item.Data
                    });
            }

            // Open excel file
            XLWorkbook fileOpen = new XLWorkbook("manual.xlsx");
            IXLWorksheet openSheet = fileOpen.Worksheet(1);
            string Sformat = string.Empty;
            string date = string.Empty;
            foreach (var row in openSheet.RowsUsed())
            {
                date = row.Cell(1).Value.ToString();
                if (date.IndexOf("AM") > 0)
                {
                    Sformat = "d/M/yyyy";
                    date = date.Substring(0, date.IndexOf(" 12"));
                }
                else
                {
                    Sformat = "dd/MM/yyyy";
                    //date = row.Cell(1).Value.ToString();
                }

                manunalList.Add(
                   new Instrument
                   {
                       Date = DateTime.ParseExact(date, Sformat, sg, DateTimeStyles.None),
                       Data = row.Cell(2).Value.ToString()
                   });
            }

            WriteExcel(manunalList, bloombergList, sg);
            Console.WriteLine(bloombergList.Count);
            Console.WriteLine(manunalList.Count);
            Console.Read();
        }

        private static void WriteExcel(List<Instrument> manualList, List<Instrument> bloomBergList, CultureInfo sg)
        {
            List<CompareInstrument> list = new List<CompareInstrument>();
            // Create excel file in memory
            XLWorkbook file = new XLWorkbook();
            // Add Sheet
            file.Worksheets.Add("First Sheet");
            // Get Sheet
            IXLWorksheet sheet = file.Worksheet("First Sheet");
            // Add cell value
            int i = 2;

            // Add header
            sheet.Cell("A1").Value = "Date";
            sheet.Cell("B1").Value = "Manual";
            sheet.Cell("C1").Value = "Bloomberg";
            sheet.Cell("D1").Value = "Is False";

            DateTime endDate = DateTime.ParseExact("01/03/2017", "dd/MM/yyyy", sg);
            for (DateTime d = DateTime.ParseExact("01/01/1990", "dd/MM/yyyy", sg); endDate.CompareTo(d) > 0; d = d.AddDays(1.0))
            {
                if (manualList.Any(x => x.Date.Date == d.Date) || bloomBergList.Any(x => x.Date.Date == d.Date))
                {
                    list.Add(new CompareInstrument
                    {
                        Date = d.ToString("dd/MM/yyyy"),
                        ManualData = manualList.FirstOrDefault(x => x.Date.Date == d.Date.Date) != null ? manualList.FirstOrDefault(x => x.Date.Date == d.Date.Date).Data : "",
                        BloombergData = bloomBergList.FirstOrDefault(x => x.Date.Date == d.Date.Date) != null ? bloomBergList.FirstOrDefault(x => x.Date.Date == d.Date.Date).Data : ""
                    });
                }

            }

            foreach (var item in list)
            {
                sheet.Cell("A" + i).Value = "'"+item.Date;
                sheet.Cell("B" + i).Value = item.ManualData;
                sheet.Cell("C" + i).Value = item.BloombergData;

                if(string.IsNullOrEmpty(item.ManualData) && string.IsNullOrEmpty(item.BloombergData))
                {
                    if(item.ManualData != item.BloombergData)
                    {
                        sheet.Cell("B" + i).Style.Fill.BackgroundColor = XLColor.Red;
                        sheet.Cell("C" + i).Style.Fill.BackgroundColor = XLColor.Red;
                        sheet.Cell("D" + i).Value = "FALSE";
                    }
                }

                i++;
            }

            // Save file to hard disk
            file.SaveAs(@"D:\closeXML-example.xlsx");

            // Release resource
            sheet.Dispose();
            file.Dispose();
        }
    }

    public class InstrumentJson
    {
        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("Data")]
        public string Data { get; set; }
    }

    public class Instrument
    {
        public DateTime Date { get; set; }

        public string Data { get; set; }
    }

    public class CompareInstrument
    {
        public string Date { get; set; }

        public string ManualData { get; set; }

        public string BloombergData { get; set; }
    }
}
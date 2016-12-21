using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace excel_closedXML_findValue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.textBox1.Text = @"C:\Users\MinhPD.NTTDV\Desktop\GBS\GBS France Templates-selected";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "FINDING...";
            string path = textBox1.Text;

            string[] input = textBox2.Text.Split(',');
            List<string> inputList = new List<string>(input);

            string value = textBox2.Text.Trim();

            path = path.Replace("\\\\", "\\");
            StringBuilder result = new StringBuilder();
            int totalFile = Directory.GetFiles(path, "*").Count();

            label2.Text = "FINDING...";

            foreach (string fileName in Directory.GetFiles(path, "*").Select(Path.GetFileName))
            {
                result.AppendLine("--------------" + fileName + "--------------");
                XLWorkbook file = new XLWorkbook(string.Format("{0}/{1}", path, fileName));

                foreach (var sheet in file.Worksheets)
                {
                    //result.AppendLine(sheet.Name + ":" + sheet.CellsUsed().Count());
                    foreach (IXLCell cell in sheet.CellsUsed())
                    {
                        if (cell != null && cell.FormulaA1 == "" && cell.FormulaR1C1 == "" && cell.Value != null)
                        {
                            if (value.Equals(cell.Value.ToString()))
                            {
                                result.AppendLine(string.Format("Found in \"{0}\", Sheet \"{1}\", Cell \"{2}\"", fileName, sheet.Name, cell.Address.ToString()));
                            }
                        }
                    }
                }

                file.Dispose();
            }
            label2.Text = string.Format("Total file found : {0}", totalFile);
            richTextBox1.Text = result.ToString();
        }
    }
}
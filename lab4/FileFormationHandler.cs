using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Style.XmlAccess;

namespace lab4
{
    class FileFormationHandler
    {
        static string path;

        static ExcelPackage package;
       
        static ExcelWorksheet sheet;

        static ExcelNamedStyleXml styleXml;

        static int row = 1;

        public static void WriteToExcel(Dictionary<string, string> dict)
        {
            try
            {
                if (path == null)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\MyFile.xlsx";

                    package = new ExcelPackage();

                    sheet = package.Workbook.Worksheets.Add("firstSheet");

                    styleXml = package.Workbook.Styles.CreateNamedStyle("Page");

                    styleXml.Style.Font.Bold = true;
                    styleXml.Style.Font.Size = 16;
                    styleXml.Style.Font.Color.SetColor(System.Drawing.Color.Blue);

                    sheet.Column(1).Width = 155;
                    sheet.Column(2).Width = 55;

                    sheet.Cells["A1"].StyleName = "Page";

                    package.SaveAs(new FileInfo(path));
                }

                sheet.Cells["A" + row].StyleName = "Page";
                sheet.Cells["A" + row].Value = Utils.CurrentURI;
                sheet.Cells["A" + row++].Hyperlink = new ExcelHyperLink(Utils.CurrentURI);

                sheet.Row(row).OutlineLevel = 0;

                foreach (var value in dict)
                {
                    sheet.Cells["A" + row].Value = value.Key;
                    sheet.Cells["B" + row].Value = value.Value;

                    sheet.Row(row).OutlineLevel = 1;

                    row++;
                }
                row++;
                package.Save();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\t\u001b[31m============Закройте .xlsx файл!============\u001b[0m\n");
                
                Environment.Exit(0);
            }
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;

namespace lab4
{
    class FileFormationHandler
    {
        static string path;

        static ExcelPackage package;
        static ExcelWorksheet sheet;

        static int row = 2;

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

                    sheet.Cells["A1"].Value = "Ссылка";
                    sheet.Cells["A1"].Style.Font.Bold = true;
                    sheet.Cells["A1"].Style.Font.Size = 16; 
                    
                    sheet.Cells["B1"].Value = "Описание";
                    sheet.Cells["B1"].Style.Font.Bold = true;
                    sheet.Cells["B1"].Style.Font.Size = 16;

                    sheet.Column(1).Width = 125;
                    sheet.Column(2).Width = 55;

                    package.SaveAs(new FileInfo(path));
                }
                
                foreach(var value in dict) 
                {
                    sheet.Cells["A" + row].Value = value.Key;
                    sheet.Cells["B" + row].Value = value.Value;

                    row++;
                }

                row++;

                package.Save();
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("\t\u001b[31m============Закройте .xlsx файл!============\u001b[0m\n");
                Environment.Exit(0);
            }
        }
    }
}
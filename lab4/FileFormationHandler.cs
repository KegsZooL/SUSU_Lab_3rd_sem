using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml; //Библиотека для .xlsx файлов EPPlus - https://github.com/EPPlusSoftware/EPPlus?ysclid=lnj723wvzy926316877
using OfficeOpenXml.Style.XmlAccess;

namespace lab4
{
    class FileFormationHandler
    {
        static string path;

        static ExcelPackage package;
       
        static ExcelWorksheet sheet;

        static ExcelNamedStyleXml styleXml;

        static int row = 1; //Переменная определяющая смещение строки

        public static void WriteToExcel(Dictionary<string, string> dict)
        {
            try
            {
                if (path == null)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    //Определение пути к файлу на рабочем столе
                    path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\MyFile.xlsx";

                    package = new ExcelPackage();

                    sheet = package.Workbook.Worksheets.Add("firstSheet");

                    //Основной стиль для текста в определенной ячейке
                    styleXml = package.Workbook.Styles.CreateNamedStyle("Page");

                    styleXml.Style.Font.Bold = true;
                    styleXml.Style.Font.Size = 16;
                    styleXml.Style.Font.Color.SetColor(System.Drawing.Color.Blue);

                    //Ширина ячеек
                    sheet.Column(1).Width = 155;
                    sheet.Column(2).Width = 55;

                    sheet.Cells["A1"].StyleName = "Page";

                    package.SaveAs(new FileInfo(path)); //Сохраняем файл по пути
                }

                sheet.Cells["A" + row].StyleName = "Page";
                sheet.Cells["A" + row].Value = Utils.CurrentURI;

                //Для текущей ячейки назначаем гипперссылку 
                sheet.Cells["A" + row++].Hyperlink = new ExcelHyperLink(Utils.CurrentURI);

                sheet.Row(row).OutlineLevel = 0;

                foreach (var value in dict)
                {
                    sheet.Cells["A" + row].Value = value.Key;
                    sheet.Cells["B" + row].Value = value.Value;

                    sheet.Row(row).OutlineLevel = 1; //Определяем уровень outline для .png\.jpg ссылок

                    row++;
                }
                row++;
                package.Save(); //Сохраняем текущий .xlsx файл
            }
            catch (InvalidOperationException) //Обработка исключения, если программа запущена с открытым .xlsx файлом
            {
                Console.WriteLine("\t\u001b[31m============Закройте .xlsx файл!============\u001b[0m\n");
                
                Environment.Exit(0);
            }
        }
    }
}
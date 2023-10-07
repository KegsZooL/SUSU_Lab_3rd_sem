using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace lab4
{
    class ImgHandler : IHandler
    {
        const string URL_REG_EX_PATTERN = "<img\\s+[^>]*?src=(\"|')([^\"']+)\\1";

        public void Process(Uri uri, int currentDepth)
        {
            string page = Utils.GetPageByURI(uri);

            List<string> parametrsURI = Regex.Matches(page, URL_REG_EX_PATTERN).Cast<Match>().
                    Select((key) => key.Value.Replace("<img ", "").Replace(">", "")).ToList();

            Console.WriteLine($"Page: \x1b[31m{uri}\x1b[0m\n");

            File.WriteAllLines("C:\\Users\\KegsZooL\\Desktop\\1.txt", parametrsURI);

            for (int i = 0; i < parametrsURI.Count; i++)
            {
                List<string> splitedCurrentLine = parametrsURI[i].Split(' ').ToList();

                string newLine = "";

                if (parametrsURI[i].StartsWith("alt"))
                {
                    newLine += splitedCurrentLine[0];

                    for (int q = 1; q < splitedCurrentLine.Count && splitedCurrentLine.Count > 2; q++)
                    {
                        if (splitedCurrentLine[q].Contains("\""))
                        {
                            newLine += $" {splitedCurrentLine[q]}";
                            break;
                        }
                        newLine += $" {splitedCurrentLine[q]}";
                    }
                }

                newLine += $" {splitedCurrentLine[splitedCurrentLine.Count - 1]}";

                parametrsURI[i] = newLine.Replace("alt=", "").Replace("src=", "").Replace("\"", "").Trim();
            }

            OutputHandler.Print(ref parametrsURI);
        }
    }
}
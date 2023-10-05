using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab4
{
    class ImgHandler : IHandler
    {
        const string URL_REG_EX_PATTERN = "<img\\s+[^>]*?src=(\"|')([^\"']+)\\1";

        public void Process(Uri uri)
        {   
            string page = Utils.GetPageByURI(uri);

            List<string> parametrsInTag = Regex.Matches(page, URL_REG_EX_PATTERN).Cast<Match>().
                    Select((key) => key.Value.Replace("<img ", "").Replace(">", "")).ToList();

            Console.WriteLine($"Page: \x1b[31m{uri}\x1b[0m\n");

            for (int i = 0; i < parametrsInTag.Count; i++)
            {
                List<string> splitedCurrentLine = parametrsInTag[i].Split(' ').ToList();

                string newLine = "";

                if (parametrsInTag[i].StartsWith("alt")) 
                {
                    newLine += splitedCurrentLine[0];

                    for (int q = 1; q < splitedCurrentLine.Count && splitedCurrentLine.Count >2; q++)
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

                parametrsInTag[i] = newLine.Replace("alt=", "").Replace("src=", "").Replace("\"", "").Trim(); 
            }

            OutputHandler.Print(ref parametrsInTag);
        }
    }
}
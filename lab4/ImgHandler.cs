using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab4
{
    class ImgHandler : IHandler
    {
        const string URL_REG_EX_PATTERN = "<img\\s+[^>]*?src=(\"|')([^\"']+)\\1";

        public void Process(Uri uri, int currentDepth)
        {
            Console.WriteLine($"Page: \x1b[31m{uri}\x1b[0m\n");
            
            string page = Utils.GetPageByURI(uri);

            List<string> parametrsURI = Regex.Matches(page, URL_REG_EX_PATTERN).Cast<Match>().
                    Select((key) => key.Value.Replace("<img ", "").Replace(">", "")).ToList();

            for (int i = 0; i < parametrsURI.Count; i++)
            {
                List<string> splitedCurrentLine = parametrsURI[i].Split(' ').ToList();

                if(splitedCurrentLine.Count == 1 && !splitedCurrentLine[0].Contains(".jpg") && !splitedCurrentLine[0].Contains(".png")) 
                {
                    parametrsURI.Remove(parametrsURI[i]);

                    --i;
                    continue;
                }

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

                if (splitedCurrentLine[splitedCurrentLine.Count - 1] != "\"")
                {
                    newLine += $" {splitedCurrentLine[splitedCurrentLine.Count - 1]}";
                }
                else
                {
                    newLine += $" {splitedCurrentLine[splitedCurrentLine.Count - 2]}";
                }

                parametrsURI[i] = newLine.Replace("alt=", "").Replace("src=", "").Replace("\"", "").Trim();
            }

            OutputHandler.Print(ref parametrsURI);
        }
    }
}
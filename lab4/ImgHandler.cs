using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab4
{
    class ImgHandler : IHandler
    {
        const string URL_REG_EX_PATTERN = "<img\\s+[^>]*?src=(\"|')([^\"']+)\\1";

        readonly string[] banWords = { "class=", "typeof=", "itemprop=", 
                                       "align=", "border=", "srcset=" 
                                     };
        public void Process(Uri uri)
        {
            string page = Utils.GetPageByURI(uri);

            List<string> parametrsInTag = Regex.Matches(page, URL_REG_EX_PATTERN).Cast<Match>().
                    Select((key) => key.Value.Replace("<img ", "").Replace(">", "")).ToList();

            Console.WriteLine($"Page: \x1b[31m{uri}\x1b[0m\n");

            for (int i = 0; i < parametrsInTag.Count; i++)
            {
                List<string> splitedCurrentLine = parametrsInTag[i].Split(' ').ToList();

                for (int j = 0; j < splitedCurrentLine.Count; j++)
                {
                    if (splitedCurrentLine[j] == "alt=\"\"") 
                    {
                        splitedCurrentLine.Remove(splitedCurrentLine[j]);
                    }

                    if (splitedCurrentLine.Count > 1)
                    {
                        int flag = 0;

                        for (int q = 0; q < banWords.Length; q++)
                        {
                            if (splitedCurrentLine[j].Contains(banWords[q]))
                            {
                                splitedCurrentLine.Remove(splitedCurrentLine[j]);
                                flag = 1;
                            }

                            if (splitedCurrentLine[j].Contains("\"") && flag == 1 && !splitedCurrentLine[j].Contains("src="))
                            {
                                splitedCurrentLine.Remove(splitedCurrentLine[j]);
                                flag = 0;
                                q = -1;
                            }
                        }
                    }

                    splitedCurrentLine[j] = splitedCurrentLine[j].Replace("src=\"", "").Replace("alt=\"", "").Replace("\"", "");

                    for (int q = 0; q < splitedCurrentLine.Count; q++)
                    {
                        string url = splitedCurrentLine[q];

                        if (splitedCurrentLine[q].StartsWith("/")) 
                        {
                            splitedCurrentLine[q] = $"https://www.susu.ru/{url}";
                        }

                        else if (splitedCurrentLine[q].StartsWith("https"))
                        {
                            splitedCurrentLine[q] = url;
                        }
                    }
                }
                parametrsInTag[i] = string.Join("  ", splitedCurrentLine).Trim();
            }
            OutputHandler.Print(parametrsInTag);
        }
    }
}
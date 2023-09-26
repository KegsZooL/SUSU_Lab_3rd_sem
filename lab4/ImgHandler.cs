using lab4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace cyberdemon
{
    class ImgHandler : IHandler
    {
        public void Process(Uri uri)
        {
            string page = Utils.GetPageByURI(uri);

            const string URLRegExPattern = "<img\\s+[^>]*?src=(\"|')([^\"']+)\\1";

            string[] banWords = { "class=", "typeof=", "itemprop=", "align=", "border=" };

            List<string> parametrsInTag = Regex.Matches(page, URLRegExPattern).Cast<Match>().
                Select((key) => key.Value.Replace("<img ", "").Replace(">", "")).ToList();

            for (int i = 0; i < parametrsInTag.Count; i++)
            {
                List<string> splitedCurrentLine = parametrsInTag[i].Split(' ').ToList();

                for (int j = 0; j < splitedCurrentLine.Count; j++)
                {
                    if (splitedCurrentLine[j] == "alt=\"\"")
                        splitedCurrentLine.Remove(splitedCurrentLine[j]);

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
                            splitedCurrentLine[q] = $"https://www.susu.ru/{url}";

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

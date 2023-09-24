using System;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

namespace lab4
{
    class ParserHTML    
    {
        const string URI = "https://www.susu.ru/ru/structure/arhitekturno-stroitelnyy-institut";
        public void GetLinksToPNG()
        {
            WebClient webClient = new WebClient();

            webClient.Headers["User-Agent"] = "Mozila/5.0";
            webClient.Encoding = Encoding.UTF8;

            string page = webClient.DownloadString(new Uri(URI)),
                URLRegExPattern = "<img\\s+[^>]*?src=(\"|')([^\"']+)\\1";

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

            //Comparison<string> lengthCompare = (string x, string y) => x.Length.CompareTo(x.Length);

            //parametrsInTag.Sort(lengthCompare);   

            OutputHandler.Print(parametrsInTag);
        }
    }
}
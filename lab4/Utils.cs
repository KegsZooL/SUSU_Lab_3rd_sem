using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace lab4
{
    static class Utils
    {   
        public static string Domain { get; private set; }
        public static string CurrentURI { get; private set; }

        public static string GetPageByURI(Uri uri) 
        {
            if (Domain == null)
            {
                int countOfSlashes = 0;
                
                string currentUri = uri.ToString();

                for (int i = 0; i < currentUri.Length; i++)
                {
                    if (currentUri[i] == '/') 
                        ++countOfSlashes;

                    if (countOfSlashes == 3) 
                        break;

                    Domain += currentUri[i];
                }
            }

            CurrentURI = uri.ToString();

            WebClient webClient = new WebClient();

            webClient.Headers["User-Agent"] = "Mozila/5.0";
            webClient.Encoding = Encoding.UTF8;

            return webClient.DownloadString(uri);
        }

        public static List<string> GetParametrsURI(ref string page) 
        {
            const string URL_REG_EX_PATTERN = "a\\s+[^>]*href=\"([^\"]*)\"[^>]*";

            List<string> parametrsURI = Regex.Matches(page, URL_REG_EX_PATTERN).Cast<Match>()
            .Select(key => key.Value).ToList(), currentSplitedLine, newLine = new List<string>();

            for (int i = 0; i < parametrsURI.Count; i++)
            {
                currentSplitedLine = parametrsURI[i].Split(' ').ToList();

                int countQuotes = 0, startIndexOfLink = 0, finalIndexOfLink = 0;

                for (int j = 0; j < currentSplitedLine.Count; j++)
                {
                    if (currentSplitedLine[j].Contains("href="))
                    {
                        for (int q = 0; q < currentSplitedLine[j].Length; q++)
                        {
                            if (currentSplitedLine[j][q] == '\"' && countQuotes == 0)
                            {
                                startIndexOfLink = q + 1;
                                ++countQuotes;

                                continue;
                            }

                            else if (currentSplitedLine[j][q] == '\"' && countQuotes == 1)
                            {
                                finalIndexOfLink = q;

                                parametrsURI[i] = currentSplitedLine[j].Substring(startIndexOfLink, finalIndexOfLink - startIndexOfLink);
                                parametrsURI[i] = parametrsURI[i].StartsWith("/") ? $"{Utils.Domain}{parametrsURI[i]}" : parametrsURI[i];

                                break;
                            }
                        }
                    }
                }
            }
            return parametrsURI;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace lab4
{
    class HrefHandler : IHandler
    {   
        readonly HashSet<Uri> passedLinks = new HashSet<Uri>();

        int MaxNumberOfPages { get; }

        string Domain { get; set; }

        public HrefHandler(int maxNumberOfPages) => MaxNumberOfPages = maxNumberOfPages;

        public void Process(Uri uri)
        {
            if (passedLinks.Contains(uri))
                return;

            if (MaxNumberOfPages == 0)
                return;

            if (passedLinks.Count == 0)
                Domain = uri.ToString();

            const string URL_REG_EX_PATTERN = "a\\s+[^>]*href=\"([^\"]*)\"[^>]*";

            string page = Utils.GetPageByURI(uri);
    
            string[] keyWords = { "href=", "title="};

            List<string> parametrsInTag = Regex.Matches(page, URL_REG_EX_PATTERN).Cast<Match>().
                Select(key => key.Value).ToList();

            List<string> currentSplitedLine, newLine = new List<string>();  
            
            for (int i = 0; i < parametrsInTag.Count; i++)
            {
                currentSplitedLine = parametrsInTag[i].Split(' ').ToList();

                for (int j = 0; j < currentSplitedLine.Count; j++)
                {
                    for (int q = 0; q < keyWords.Length; q++)
                    {
                        if (currentSplitedLine[j].Contains(keyWords[q])) 
                        {
                            newLine.Add(currentSplitedLine[j].Replace("href=", "").Replace("\"", ""));
                        }
                    }
                }
                parametrsInTag[i] = string.Join(" ", newLine);

                if (parametrsInTag[i].StartsWith("/ru/structure"))
                    parametrsInTag[i] = $"{Domain.Replace("/structure", "")}{parametrsInTag[i]}";

                else if (!parametrsInTag[i].StartsWith("https:")) 
                {
                    parametrsInTag[i] = $"{Domain.Replace("/structure", "")}{parametrsInTag[i]}";
                }

                newLine.Clear();
            }
            //File.WriteAllLines("C:\\Users\\KegsZooL\\Desktop\\afterHrefs.txt", parametrsInTag);

            //File.WriteAllLines("C:\\Users\\KegsZooL\\Desktop\\beforeHrefs.txt", parametrsInTag);
        }
    }
}
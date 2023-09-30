using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab4
{
    class HrefHandler : IHandler
    {   
        int MaxNumberOfPages { get; }

        int Count { get; set; }

        string Domain { get; set; }

        readonly HashSet<Uri> passedLinks = new HashSet<Uri>();

        List<string> mainPageLinks;

        const string URL_REG_EX_PATTERN = "a\\s+[^>]*href=\"([^\"]*)\"[^>]*";

        readonly string[] keyWords = { "href=", "title=" };

        public HrefHandler(int maxNumberOfPages) => MaxNumberOfPages = maxNumberOfPages;

        public void Process(Uri uri)
        {
            if (passedLinks.Contains(uri) || Count > MaxNumberOfPages) 
            {
                return;
            }

            if (Domain == null) 
            {
                Domain = uri.ToString();
            }

            string page = Utils.GetPageByURI(uri);

            List<string> parametrsInTag = Regex.Matches(page, URL_REG_EX_PATTERN).Cast<Match>()
                .Select(key => key.Value).ToList(), currentSplitedLine, newLine = new List<string>();  
            
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
                { 
                    parametrsInTag[i] = $"{Domain.Replace("/structure", "")}{parametrsInTag[i]}";
                }

                else if (!parametrsInTag[i].StartsWith("https:")) 
                {
                    parametrsInTag[i] = $"{Domain.Replace("/structure", "")}{parametrsInTag[i]}";
                }

                newLine.Clear();
            }
            passedLinks.Add(uri);

            if (mainPageLinks == null)
                mainPageLinks = parametrsInTag;

            RequestEvent.Notify(new Uri(mainPageLinks[Count++].Split(' ')[0]));            
        }
    }
}
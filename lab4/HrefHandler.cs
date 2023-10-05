using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace lab4
{
    class HrefHandler : IHandler
    {   
        int MaxNumberOfPages { get; }

        int MainPageLinksID { get; set; }
        
        int MaxDepth { get; set; }

        int CurrentDepth { get; set; }

        string BaseURI { get; set; }

        const string URL_REG_EX_PATTERN = "a\\s+[^>]*href=\"([^\"]*)\"[^>]*";

        List<string> mainPageLinks;

        readonly HashSet<Uri> passedLinks = new HashSet<Uri>();

        public HrefHandler(int maxNumberOfPages, int maxDepth) 
        {
            MainPageLinksID = -1;
            
            MaxNumberOfPages = maxNumberOfPages;
            
            MaxDepth = maxDepth;
        } 
        
        public void Process(Uri uri) 
        {
            if (MaxNumberOfPages <= 0)
            { 
                return;
            }

            if(BaseURI == null) 
            {
                BaseURI = uri.ToString();
            }

            passedLinks.Add(uri);
            
            string currentPage = Utils.GetPageByURI(uri);

            List<string> parametrsURI = Regex.Matches(currentPage, URL_REG_EX_PATTERN).Cast<Match>()
                .Select(key => key.Value).ToList(), currentSplitedLine, newLine = new List<string>();

            File.WriteAllLines("C:\\Users\\KegsZooL\\Desktop\\1.txt", parametrsURI);

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

                            if (currentSplitedLine[j][q] == '\"' && countQuotes == 1)
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

            if (mainPageLinks == null)
                mainPageLinks = parametrsURI;

            //File.WriteAllLines("C:\\Users\\KegsZooL\\Desktop\\1.txt", mainPageLinks);

            while (passedLinks.Count < MaxNumberOfPages)
            {
                ++MainPageLinksID;

                try 
                {
                    if (!passedLinks.Contains(new Uri(mainPageLinks[MainPageLinksID])))
                    {
                        if (mainPageLinks[MainPageLinksID].StartsWith(BaseURI)) 
                        {
                            RequestEvent.Notify(new Uri(mainPageLinks[MainPageLinksID]));
                        }
                    }
                }
                catch 
                {
                    continue;
                }
            }
        }
    }
}
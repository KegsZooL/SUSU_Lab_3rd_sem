using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace lab4
{
    class HrefHandler : IHandler
    {

        public void Process(Uri uri)
        {
            string page = Utils.GetPageByURI(uri);

            const string URLRegExPattern = "a\\s+[^>]*href=\"([^\"]*)\"[^>]*";

            string[] banWords = { "a", "class=\\", "class=","data-target=", 
                                  "data-toggle=", "data-target=", "role=",
                                  "rel=", "id="
                                };

            List<string> parametrsInTag = Regex.Matches(page, URLRegExPattern).Cast<Match>().
                Select(key => key.Value).ToList();

            List<string> currentSplitedLine;  
            
            for (int i = 0; i < parametrsInTag.Count; i++)
            {
                currentSplitedLine = parametrsInTag[i].Split(' ').ToList();

                for (int j = 0; j < currentSplitedLine.Count; j++)
                {

                    for (int q = 0; q < banWords.Length; q++)
                    {
                        if (currentSplitedLine[j].Contains(banWords[q])) 
                        {
                            currentSplitedLine.Remove(currentSplitedLine[j]);
                            j = 0;
                        }
                    }

                    parametrsInTag[i] = string.Join(" ", currentSplitedLine);
                }
            }

            File.WriteAllLines("C:\\Users\\KegsZooL\\Desktop\\afterHrefs.txt", parametrsInTag);
        }
    }
}
    


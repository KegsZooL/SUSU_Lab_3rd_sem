using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{   
    class OutputHandler
    {   
        public static void Print(ref List<string> parametrs) 
        {   
            Dictionary<string, string> linksAndDescriptions = new Dictionary<string, string>();

            List<string> currentSplitedLine;

            string description = "";

            for (int i = 0; i < parametrs.Count; i++)
            {
                currentSplitedLine = parametrs[i].Split(' ').ToList();

                for (int j = currentSplitedLine.Count - 1; j >= 0; j--)
                {
                    int lastIndex = currentSplitedLine.Count - 1;
                    
                    string currentURI = ChangeTheURI(currentSplitedLine[lastIndex]);

                    if (!linksAndDescriptions.Keys.Contains(currentURI)) 
                    {
                        if (currentSplitedLine.Count == 1) 
                        {   
                            Console.WriteLine($"Ref: \u001b[36m{currentURI}\u001b[0m\n\t" +
                                $"Decrtiption: \u001b[33mno\u001b[0m\n");
                        }
                        else 
                        {
                            for (int q = 0; q < lastIndex; q++)
                            {   
                                if(q == lastIndex - 1)
                                    description += $"{currentSplitedLine[q]}";
                                else
                                    description += $"{currentSplitedLine[q]} ";
                            }
                            Console.WriteLine($"Ref: \u001b[36m{currentURI}\u001b[0m\n\t" + 
                                $"Decrtiption: \u001b[33m{description}\u001b[0m\n");
                        }

                        linksAndDescriptions.Add(currentURI, description);
                        description = "";
                    }   
                }
            }

            linksAndDescriptions = linksAndDescriptions.OrderByDescending((key) => key.Value.Length).ToDictionary((key) => key.Key, key => key.Value);
            FileFormationHandler.WriteToExcel(dict: linksAndDescriptions);
        }

        static string ChangeTheURI(string currentUri)
        {
            if (currentUri.StartsWith("/"))
            {
                return $"{Utils.Domain}{currentUri}";
            }
            return currentUri;
        }
    }
}
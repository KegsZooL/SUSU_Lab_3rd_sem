using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{   
    class OutputHandler
    {   
        public static void Print(ref List<string> parametrs) 
        {   
            HashSet<string> passedLinks = new HashSet<string>();

            List<string> currentSplitedLine;

            string description = "";

            for (int i = 0; i < parametrs.Count; i++)
            {
                currentSplitedLine = parametrs[i].Split(' ').ToList();

                for (int j = currentSplitedLine.Count - 1; j >= 0; j--)
                {
                    int lastIndex = currentSplitedLine.Count - 1;
                    
                    string currentUri = ChangeTheURI(currentSplitedLine[lastIndex]);

                    if (!passedLinks.Contains(currentUri)) 
                    {
                        if (currentSplitedLine.Count == 1) 
                        {   
                            Console.WriteLine($"Ref: \u001b[36m{currentUri}\u001b[0m\n\t" +
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

                            Console.WriteLine($"Ref: \u001b[36m{currentUri}\u001b[0m\n\t" + 
                                $"Decrtiption: \u001b[33m{description}\u001b[0m\n");

                            description = "";
                        }
                        passedLinks.Add(currentUri);
                    }   
                }
            }
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
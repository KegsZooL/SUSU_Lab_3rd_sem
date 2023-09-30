using System;
using System.Collections.Generic;

namespace lab4
{   
    class OutputHandler
    {   
        //Код хуйня, нужен рефакторинг 
        public static void Print(List<string> parametrs) 
        {   
            HashSet<string> allLinks = new HashSet<string>();

            string[] currentSplitedLine; string description = "";

            int[] withoutDescriptionID = new int[parametrs.Count]; 
            int count = 0, flag = 0;

            for (int i = 0; i < parametrs.Count; i++)
            {
                currentSplitedLine = parametrs[i].Split(' ');

                for (int j = 0; j < currentSplitedLine.Length; j++)
                {
                    if (currentSplitedLine[j].Contains("https"))
                    {   
                        if(currentSplitedLine.Length == 1 && !allLinks.Contains(currentSplitedLine[j])) 
                        {
                            withoutDescriptionID[count++] = i;
                            
                            allLinks.Add(currentSplitedLine[j]);

                            flag = 1;

                            continue;
                        }
                        allLinks.Add(currentSplitedLine[j]);
                    }
                }

                if (flag == 1) 
                {
                    flag = 0;
                    continue;
                }

                for (int q = 0; q < currentSplitedLine.Length; q++)
                {
                    if (!currentSplitedLine[q].Contains("https"))
                    {
                        if (description.Contains("\n"))
                        {
                            description = "no";
                        }

                        description += $"{currentSplitedLine[q]} ";
                    }
                    else if (!allLinks.Contains(currentSplitedLine[q]))
                    {
                        Console.WriteLine($"Ref: \x1b[36m{currentSplitedLine[q]}\x1b[0m\n\t" +
                            $"Decrtiption: \x1b[33m{description}\u001b[0m\n");

                        description = "";
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"Ref: \u001b[36m{parametrs[withoutDescriptionID[i]]}\u001b[0m\n\t" +
                    $"Decrtiption: \u001b[33mno\u001b[0m\n");
            }
        }
    }
}
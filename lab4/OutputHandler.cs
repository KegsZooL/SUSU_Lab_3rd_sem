using System;
using System.Collections.Generic;

namespace lab4
{   
    class OutputHandler
    {
        public static void Print(List<string> parametrs) 
        {
            string[] currentSplitedLine;
            string description = "";

            int[] withoutDescriptionID = new int[parametrs.Count];
            int count = 0;

            for (int i = 0; i < parametrs.Count; i++)
            {
                currentSplitedLine = parametrs[i].Split(' ');

                if (currentSplitedLine.Length == 1) 
                {
                    withoutDescriptionID[count++] = i;
                    continue;
                }
                    
                for (int q = 0; q < currentSplitedLine.Length; q++)
                {   
                    if (!currentSplitedLine[q].Contains("https")) 
                    {
                        description += $"{currentSplitedLine[q]} ";
                    }
                    else 
                    {
                        Console.WriteLine($"Ref: \x1b[36m{currentSplitedLine[q]}\x1b[0m\n\tDecrtiption: \x1b[33m{description}\u001b[0m");
                        description = "";
                    }
                }
            }

            for (int i = 0; i < count; i++)
                Console.WriteLine($"Ref: \u001b[36m{parametrs[withoutDescriptionID[i]]}\u001b[0m\n\tDecrtiption: \u001b[33mno\u001b[0m");
            
            Console.WriteLine();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{   
    class OutputHandler
    {   
        public static void Print(ref List<string> parametrs) 
        {
            //Словарь для хранения ссылок на .png\.jpg и их описаний

            Dictionary<string, string> linksAndDescriptions = new Dictionary<string, string>();

            List<string> currentSplitedLine;

            string description = "";

            //Проходимся по всем ссылкам
            for (int i = 0; i < parametrs.Count; i++)
            {
                // Разделяем текущую строку на части по пробелам
                currentSplitedLine = parametrs[i].Split(' ').ToList();

                for (int j = currentSplitedLine.Count - 1; j >= 0; j--)
                {
                    int lastIndex = currentSplitedLine.Count - 1;

                    //Добавлям к ссылке домен, если она неполная
                    string currentURI = ChangeTheURI(currentSplitedLine[lastIndex]);

                    //Пропускаяем вывод уже пройденных ссылок
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

                        // Добавляем ссылку и описание в словарь
                        linksAndDescriptions.Add(currentURI, description);
                        description = "";
                    }   
                }
            }
            // Сортируем словарь по длине описаний по убыванию
            linksAndDescriptions = linksAndDescriptions.OrderByDescending((key) => key.Value.Length).ToDictionary((key) => key.Key, key => key.Value);
            FileFormationHandler.WriteToExcel(dict: linksAndDescriptions); // Записываем данные в Excel файл
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
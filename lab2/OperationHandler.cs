using Sharprompt;
using System.Collections.Generic;
using System.Linq;
using System;

namespace lab_2
{
    public class OperationHandler
    {
        public void Start()
        {
            Dictionary<string, IntegerSet> dict = new Dictionary<string, IntegerSet>() 
            {
                {"1",  new IntegerSet(new List<int> { 1, 2, 3, 4, 5 }) },
                {"2",  new IntegerSet(new List<int> { 3, 2, 7, 4, 1 }) }
            };

            string definiteSet; 
            IEnumerable<int> value;

            var typeOfOperation = Prompt.Select("Выберите тип операции", new[] {"[1] Добавить элемент в множество.", 
                "[2] Удалить элемент в множестве.", "[3] Объединить множества.", "[4] Сравнить множества.", "[5] Разность множеств."});

            switch (typeOfOperation[1])
            {
                case ('1'):

                    definiteSet = Prompt.Select("В какое множество вы хотите добавить элемент?", dict.Keys);
                    value = Prompt.List<int>($"Введите число, которое хотите добавить в множество №{definiteSet}");

                    dict[definiteSet] = dict[definiteSet] + value.ToArray<int>()[0];

                    Console.WriteLine("\n==================Добавление элемента прошло успешно!==================\n");

                    break;

                case ('2'):

                    definiteSet = Prompt.Select("В каком множестве вы хотите удалить элемент?", dict.Keys);
                    value = Prompt.List<int>($"Введите число, которе хотите удалить в множестве №{definiteSet}");

                    int previousLength = dict[definiteSet]._HashSet.Count;

                    dict[definiteSet] = dict[definiteSet] - value.ToArray<int>()[0];

                    if (previousLength == dict[definiteSet]._HashSet.Count)
                        Console.WriteLine($"\n==================Элемент {value.ToArray<int>()[0]} не находится в множестве!==================\n");
                    else
                        Console.WriteLine($"\n==================Удаление элемента прошло успешно!==================\n");

                    break;

                case ('3'):
                    dict["1"] = dict["1"] + dict["2"];

                    Console.WriteLine($"\n==================Объединение множеств прошло успешно!==================\n");

                    break;

                case ('4'):

                    break;

                case ('5'):

                    definiteSet = Prompt.Select("Какое множество будет уменьшаемым?", dict.Keys);

                    if (definiteSet == "1")
                        dict["1"] = dict["1"] - dict["2"];
                    else
                        dict["2"] = dict["2"] - dict["1"];

                    Console.WriteLine($"\n==================Разность множеств прошла успешно!==================\n");

                    break;
            }
        }
    }
}

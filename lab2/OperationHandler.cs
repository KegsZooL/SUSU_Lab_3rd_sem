using Sharprompt; // Библиотека для организации меню в приложении (https://github.com/shibayan/Sharprompt)
using System.Collections.Generic;
using System.Linq;
using System;

namespace lab_2
{
    public class OperationHandler
    {
        public void Start()
        {
            Dictionary<string, IntegerSet> dict = new Dictionary<string, IntegerSet>() // Словарь для удобного обращения к объектам класса IntegerSet
            {
                {"1",  new IntegerSet(new List<int> { 0, 0, 0, 0, 0 }) },
                {"2",  new IntegerSet(new List<int> { 1, 0, 0, 0, 0 }) }
            };

            string definiteSet; // Вспомогательная переменная для обращения к определенному класса IntegerSet в словаре
            IEnumerable<int> value; //Вспомогательная переменная для работы удаления или добавления элемента в множество

            int programStatus = 1;

            while (programStatus == 1) // Цикл для предотвращения завершения программы после окончания определенной операции(кроме [6] Выход)
            {
                var typeOfOperation = Prompt.Select("Выберите тип операции", new[] { "[1] Добавить элемент в множество.", // Основыне операции
                                                                                     "[2] Удалить элемент в множестве.", 
                                                                                     "[3] Объединить множества.", 
                                                                                     "[4] Сравнить множества.", 
                                                                                     "[5] Разность множеств.",
                                                                                     "[6] Выход."

                                                                                    });
                switch (typeOfOperation[1]) // Проверка типа операции
                {
                    case ('1'): // Добавление элемента

                        definiteSet = Prompt.Select("В какое множество вы хотите добавить элемент?", dict.Keys);
                        value = Prompt.List<int>($"Введите число, которое хотите добавить в множество №{definiteSet}");

                        dict[definiteSet] = dict[definiteSet] + value.ToArray<int>()[0];

                        Console.WriteLine("\n==================Добавление элемента прошло успешно!==================\n");

                        break;

                    case ('2'): // Удаление элемента

                        definiteSet = Prompt.Select("В каком множестве вы хотите удалить элемент?", dict.Keys);
                        value = Prompt.List<int>($"Введите число, которе хотите удалить в множестве №{definiteSet}");

                        int previousLength = dict[definiteSet]._HashSet.Count; 

                        dict[definiteSet] = dict[definiteSet] - value.ToArray<int>()[0];

                        if (previousLength == dict[definiteSet]._HashSet.Count) //Если размер HashSet не изменился => элемент не был удалён
                            Console.WriteLine($"\n==================Элемент {value.ToArray<int>()[0]} не находится в множестве!==================\n");
                        else
                            Console.WriteLine($"\n==================Удаление элемента прошло успешно!==================\n");

                        break;

                    case ('3'): // Объединение множеств

                        dict["1"] = dict["1"] + dict["2"];

                        Console.WriteLine($"\n==================Объединение множеств прошло успешно!==================\n");

                        break;

                    case ('4'): // Сравнение множеств

                        definiteSet = Prompt.Select("Какое множество вы хотите сравнивать?", dict.Keys);
                    
                        List<string> setsID = new List<string>() { "1", "2" }; // Список, который нужен для определения с каким множеством будет сравниваться выбранное множество

                        setsID.Remove(definiteSet);

                        string operation = Prompt.Select($"Выберите операцию для сравнения с множеством №{setsID[0]}", new List<string>() {"==", "!=", ">=", "<=", ">", "<"});

                        bool isEqually; // Вспомогательная переменная для определения сравнения

                        switch (operation) // Проверка типа сравнения
                        {
                            case ("=="):

                                isEqually = dict[definiteSet] == dict[setsID[0]];
                            
                                Console.WriteLine($"Множество №{definiteSet} == множеству № {setsID[0]}? --> {isEqually}\n");

                                break;

                            case ("!="):
                                isEqually = dict[definiteSet] != dict[setsID[0]];

                                Console.WriteLine($"Множество №{definiteSet} != множеству № {setsID[0]}? --> {isEqually}\n");

                                break;
                            case (">="):

                                isEqually = dict[definiteSet] >= dict[setsID[0]];

                                Console.WriteLine($"Множество №{definiteSet} >= множеству № {setsID[0]}? --> {isEqually}\n");

                                break;

                            case ("<="):

                                isEqually = dict[definiteSet] <= dict[setsID[0]];

                                Console.WriteLine($"Множество №{definiteSet} <= множеству № {setsID[0]}? --> {isEqually}\n");

                                break;
                        
                            case (">"):

                                isEqually = dict[definiteSet] > dict[setsID[0]];

                                Console.WriteLine($"Множество №{definiteSet} > множеству № {setsID[0]}? --> {isEqually}\n");

                                break;

                            case ("<"):

                                isEqually = dict[definiteSet] < dict[setsID[0]];

                                Console.WriteLine($"Множество №{definiteSet} < множеству № {setsID[0]}? --> {isEqually}\n");

                                break;
                        }

                        break;

                    case ('5'): // Разность множеств

                        definiteSet = Prompt.Select("Какое множество будет уменьшаемым?", dict.Keys);

                        if (definiteSet == "1")
                            dict["1"] = dict["1"] - dict["2"];
                        else
                            dict["2"] = dict["2"] - dict["1"];

                        Console.WriteLine($"\n==================Разность множеств прошла успешно!==================\n");

                        break;

                    case ('6'):
                        programStatus = 0;
                        break;
                }
            }
        }
    }
}

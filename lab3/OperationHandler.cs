using System;
using Sharprompt; // Библиотека для организации меню в консольном приложении (https://github.com/shibayan/Sharprompt)
using System.Linq;

namespace lab3
{
    class OperationHandler
    {   
        public void Menu() 
        {   
            GraphicEditor graphicEditor = new GraphicEditor();

            int programStatus = 1;
            
            //Стандартное расположение файла для сериализации
            string pathToFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\MyGraphicEditor.json";
     
            while (programStatus == 1) // Цикл для предотвращения завершения программы после окончания определенной операции(кроме [5] Выход)
            {
                var typeOfOperation = Prompt.Select("Выберите тип операции", 
                    new[] {
                            "[1] Упорядочить всю последовательность графических фигур по возрастанию площади.",
                            "[2] Вывести последние 3-и фигуры без учета толщины рамки.",
                            "[3] Сериализация в JSON файл.",
                            "[4] Десериализовать из JSON файла.",
                            "[5] Выход."

                            });

                switch (typeOfOperation[1]) // Проверка 2-го символа выбранной строки для определения нужной операции
                {   
                    case ('1'): // Сортировка площадей объектов классов Figure по возрастанию

                        graphicEditor.AscendingSort(); // Вызов функции сортировки

                        string[] typesOfFigures = graphicEditor.AreaFigures.Keys.ToArray(); // Массив типов фигур

                        const int SPACE = -15; // Отступ в консоли для следующей строчки

                        // Вывод данных дочернего класса Figure
                        for (int i = 0; i < typesOfFigures.Length; i++)
                        {
                            Console.WriteLine(  $"  Тип фигуры: {typesOfFigures[i], SPACE}" +
                                                $"  Площадь фигуры: {graphicEditor.AreaFigures[typesOfFigures[i]]}\t" +
                                                $"  Толщина рамки: {GraphicEditor.Figures[typesOfFigures[i]].FrameThickness}"
                                             );
                        }

                        Console.WriteLine($"\n  Средняя площадь фигуры: {graphicEditor.GetAverageAreaFigure()}\n");

                        break; 
                    
                    case ('2'): // Вывод последних 3-x фигур
                        graphicEditor.OutputLastThreeFigures();

                        break;
                    
                    case ('3'): // Сериализация объектов
                        
                        //В качетсве параметра оbjects передаю список объектов Figure
                        FilesHandler.ToJson(pathToFile, objects: GraphicEditor.Figures.Values.ToList<Figure>());

                        break;                    
                    
                    case ('4'): // Десериализация
                        graphicEditor = FilesHandler.FromJson(pathToFile);

                        break;

                    case ('5'): // Выход
                        programStatus = 0;

                        break;
                }
            }
        }
    }
}
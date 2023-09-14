using System;
using Sharprompt;
using System.Linq;

namespace lab3
{
    class OperationHandler
    {   
        public void Menu() 
        {
            GraphicEditor graphicEditor = new GraphicEditor();

            int programStatus = 1;

            string pathToFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\MyGraphicEditor.json";

            while (programStatus == 1) 
            {
                var typeOfOperation = Prompt.Select("Выберите тип операции", 
                    new[] {
                            "[1] Упорядочить всю последовательность графических фигур по возрастанию площади.",
                            "[2] Вывести последние 3-и фигуры без учета толщины рамки.",
                            "[3] Сериализация в JSON файл.",
                            "[4] Десериализовать из JSON файла.",
                            "[5] Выход."

                            });

                switch (typeOfOperation[1]) 
                {   
                    case ('1'):

                        graphicEditor.AscendingSort();

                        string[] typesOfFigures = graphicEditor.AreaFigures.Keys.ToArray();

                        const int SPACE = -15;

                        for (int i = 0; i < typesOfFigures.Length; i++)
                        {
                            Console.WriteLine(  $"  Тип фигуры: {typesOfFigures[i], SPACE}" +
                                                $"  Площадь фигуры: {graphicEditor.AreaFigures[typesOfFigures[i]]}\t" +
                                                $"  Толщина рамки: {GraphicEditor.Figures[typesOfFigures[i]].FrameThickness}"
                                             );
                        }

                        Console.WriteLine($"\n  Средняя площадь фигуры: {graphicEditor.GetAverageAreaFigure()}\n");

                        break; 
                    
                    case ('2'):
                        graphicEditor.OutputLastThreeFigures();

                        break;
                    
                    case ('3'):

                        FilesHandler.ToJson(pathToFile, GraphicEditor.Figures.Values.ToList<Figure>());

                        break;                    
                    
                    case ('4'):
                        graphicEditor = FilesHandler.FromJson(pathToFile);

                        break;

                    case ('5'):
                        programStatus = 0;

                        break;
                }
            }
        }
    }
}

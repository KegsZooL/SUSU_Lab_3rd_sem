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

            graphicEditor.Add(new Rectangle(3, 10, 5));
            graphicEditor.Add(new Square(5, 15, 15));
            graphicEditor.Add(new Ellipse(3, 5, 7));
            graphicEditor.Add(new Circle(2, 10));

            int programStatus = 1;

            while (programStatus == 1) 
            {
                var typeOfOperation = Prompt.Select("Выберите тип операции", 
                    new[] {
                            "[1] Упорядочить всю последовательность графических фигур по возрастанию площади.",
                            "[2] Вывести последние 3-и фигуры без учета толщины рамки.",
                            "[3] Сериализация в JSON файл",
                            "[4] Выход."

                            });

                switch (typeOfOperation[1]) 
                {
                    case ('1'):

                        graphicEditor.AscendingSort();

                        string[] typesOfFigures = graphicEditor.AreaFigures.Keys.ToArray();

                        const int space = -15;

                        for (int i = 0; i < typesOfFigures.Length; i++)
                        {
                            Console.WriteLine(  $"  Тип фигуры: {typesOfFigures[i], space}" +
                                                $"  Площадь фигуры: {graphicEditor.AreaFigures[typesOfFigures[i]]}\t" +
                                                $"  Толщина рамки: {graphicEditor.Figures[typesOfFigures[i]].FrameThickness}"
                                             );
                        }

                        Console.WriteLine($"  \nСредняя площадь фигуры: {graphicEditor.GetAverageAreaFigure()}\n");

                        break; 
                    
                    case ('2'):
                        graphicEditor.OutputLastThreeFigures();

                        break;
                    
                    case ('3'):
                        graphicEditor.ToJson("C:\\Users\\KegsZooL\\Desktop\\test.txt");

                        break;

                    case ('4'):
                        programStatus = 0;

                        break;
                
                }
            }
        }
    }
}

using System;
using Sharprompt;
using System.Linq;

namespace lab3
{
    class OperationHandler
    {
        GraphicEditor graphicEditor = new GraphicEditor();

        public void Menu() 
        {
            int programStatus = 1;

            while(programStatus == 1) 
            {
                var typeOfOperation = Prompt.Select("Выберите тип операции", 
                    new[] {
                            "[1] Упорядочить всю последовательность графических фигур по возрастанию площади.",
                            "[2] Вывести последние 3-и фигуры без учета толщины рамки.",
                            "[3] Выход.",

                           });

                switch (typeOfOperation[1]) 
                {
                    case ('1'):

                        string[] typesOfFigures = graphicEditor.AreaFigures.Keys.ToArray();

                        const int space = -15;

                        Console.WriteLine($"  Тип фигуры: {typesOfFigures[0], space}" +
                                          $"  Площадь фигуры: {graphicEditor.AreaFigures[typesOfFigures[0]].ToString("0.00")}\t" +
                                          $"  Толщина рамки: {graphicEditor.Figures[typesOfFigures[0]].FrameThickness.ToString("0.00")}\t");

                        for (int i = 1; i < typesOfFigures.Length; i++)
                        {
                            Console.WriteLine($"  Тип фигуры: {typesOfFigures[i], space}" +
                                              $"  Площадь фигуры: {graphicEditor.AreaFigures[typesOfFigures[i]].ToString("0.00")}\t" +
                                              $"  Толщина рамки: {graphicEditor.Figures[typesOfFigures[i]].FrameThickness.ToString("0.00")}");
                        }

                        Console.WriteLine();
                        break; 
                    
                    case ('2'):
                        graphicEditor.OutputLastThreeFigures();

                        break;
                    
                    case ('3'):
                        programStatus = 0;

                        break;
                }
            }
        }
    }
}

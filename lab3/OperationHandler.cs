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
            var typeOfOperation = Prompt.Select("Выберите тип операции", 
                new[] {
                        "[1] Упорядочить всю последовательность графических фигур по возрастанию площади.",
                        "[2] Вывести последние 3-и фигуры без учета толщины рамки.",

                        });

            switch (typeOfOperation[1]) 
            {
                case ('1'):

                    string[] typesOfFigures = graphicEditor.AreaFigures.Keys.ToArray();

                    const int space = -15;

                    Console.WriteLine($"  Тип фигуры: {typesOfFigures[0], space}" +
                                        $"  Площадь фигуры: {graphicEditor.AreaFigures[typesOfFigures[0]]}\t" +
                                        $"  Толщина рамки: {graphicEditor.Figures[typesOfFigures[0]].FrameThickness}\t");

                    for (int i = 1; i < typesOfFigures.Length; i++)
                    {
                        Console.WriteLine($"  Тип фигуры: {typesOfFigures[i], space}" +
                                            $"  Площадь фигуры: {graphicEditor.AreaFigures[typesOfFigures[i]]}\t" +
                                            $"  Толщина рамки: {graphicEditor.Figures[typesOfFigures[i]].FrameThickness}");
                    }

                    Console.WriteLine();
                    break; 
                    
                case ('2'):
                    graphicEditor.OutputLastThreeFigures();

                    break;
                    
                
            }
        }
    }
}

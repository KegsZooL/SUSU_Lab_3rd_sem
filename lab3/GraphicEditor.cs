using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace lab3
{   
    class GraphicEditor
    {
        public Dictionary<string, Figure> Figures { get; }

        public Dictionary<string, int> AreaFigures { get; private set; }

        public GraphicEditor() 
        {          
            Figures = new Dictionary<string, Figure>()
            {
                { "Эллипс",        new Ellipse   (_frameThickness: 2, 10, 5)  },
                { "Квадрат",       new Square    (_frameThickness: 5, 15, 15) },
                { "Круг",          new Circle    (_frameThickness: 1, 2)      },
                { "Прямоугольник", new Rectangle (_frameThickness: 4, 5, 8)  },
            };
            ArrangeAscending();
        }

        private void ArrangeAscending()
        {
            var sortedAreas = Figures.OrderBy(key => key.Value.GetArea()).ThenBy(key => key.Value.GetAreaWithoutFrame()); 

            AreaFigures = sortedAreas.ToDictionary(key => key.Key, key => key.Value.GetArea());
        }

        public void OutputLastThreeFigures() 
        {
            string[] keys = AreaFigures.Keys.ToArray();

            Console.Clear();

            int countSpace = 0, centerX = Console.WindowWidth / 2, centerY = Console.WindowHeight / 2, startX, startY;

            for (int i = keys.Length - 1; i >= 1; i--)
            {
                try
                {
                    switch (keys[i])
                    {
                        case ("Эллипс"):
                            break;

                        case ("Круг"):
                            break;

                        case ("Прямоугольник"):

                            int widthRectangle = Figures[keys[i]].Length;
                            int heightRectangle = Figures[keys[i]].Length;

                            startX = centerX - widthRectangle / 2;
                            startY = centerY - heightRectangle / 2;

                            Console.SetCursorPosition(startX, startY);

                            for (int j = 0; j < widthRectangle; j++)
                            {
                                for (int q = 0; q < heightRectangle; q++)
                                {
                                    if(j == 0 | (j + 1) - widthRectangle == 0 | q == 0 | (q + 1) - heightRectangle == 0) {
                                        Console.Write("* ");
                                    }
                                    else {
                                        Console.Write("  ");
                                    }
                                }
                                Console.SetCursorPosition(startX, startY + (++countSpace));
                            }
                            break;

                        case ("Квадрат"):

                            int sideOfSquare = Figures[keys[i]].Length;

                            startX = centerX - sideOfSquare / 2;
                            startY = centerY - sideOfSquare / 2;

                            Console.SetCursorPosition(startX, startY);

                            for (int j = 0; j < sideOfSquare; j++)
                            {
                                for (int q = 0; q < sideOfSquare; q++)
                                {
                                    if (j == 0 | (j + 1) - sideOfSquare == 0 | q == 0 | (q + 1) - sideOfSquare == 0)
                                    {
                                        Console.Write("* ");
                                    }
                                    else
                                    {
                                        Console.Write("  ");
                                    }
                                }
                                Console.SetCursorPosition(startX, startY + (++countSpace));
                            }

                            break;
                    }

                    Thread.Sleep(500);
                    Console.Clear();

                    countSpace = 0;

                }
                catch(ArgumentOutOfRangeException){

                    Console.Clear();
                    Console.WriteLine($"\u001b[31mФигура: {keys[i]} выходит за пределы консоли!\u001b[0m\n");
                }
            }
        } 
    }
}

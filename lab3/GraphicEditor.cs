using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CustomException;

namespace lab3
{   
    class GraphicEditor
    {
        public Dictionary<string, Figure> Figures { get; }
        public Dictionary<string, double> AreaFigures { get; private set; }

        public GraphicEditor() 
        {          
            Figures = new Dictionary<string, Figure>()
            {
                //{ "Эллипс",        new Ellipse   (_frameThickness: 2.0, 10.0, 5.0)  },
                //{ "Квадрат",       new Square    (_frameThickness: 5.0, 10.0, 10.0) },
                //{ "Круг",          new Circle    (_frameThickness: 1.0, 6.0)        },
                //{ "Прямоугольник", new Rectangle (_frameThickness: 4.0, 10.0, 10.0) },                
                
                { "Эллипс",        new Ellipse   (_frameThickness: 2.0, 1000.0, 500.0)  },
                { "Квадрат",       new Square    (_frameThickness: 5.0, 700.0, 500.0) },
                { "Круг",          new Circle    (_frameThickness: 1.0, 6000.0)        },
                { "Прямоугольник", new Rectangle (_frameThickness: 4.0, 10000.0, 10000.0) },
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

            Size sizeConsole = new Size(Console.WindowWidth, Console.WindowHeight);
            
            int areaConsoleWindow = sizeConsole.Width * sizeConsole.Height;

            for (int i = keys.Length - 1; i >= 1; i--)
            {
                try 
                {
                    if (AreaFigures[keys[i]] >= areaConsoleWindow){
                        throw new GoingBeyondConsoleException($"\u001b[31mФигура: {keys[i]} выходит за пределы консоли!\u001b[0m\n");
                    }

                    switch (keys[i])
                    {
                        case ("Эллипс"):
                            break;

                        case ("Круг"):
                            break;

                        case ("Прямоугольник"):
                            break;

                        case ("Квадрат"):

                            break;
                    }

                }
                catch (GoingBeyondConsoleException ex){
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

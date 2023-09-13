using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace lab3
{   
    class GraphicEditor
    {
        public Dictionary<string, Figure> Figures { get; private set; }

        public Dictionary<string, int> AreaFigures { get; private set; }

        private int AverageAreaFigure { get; set; }

        public void AscendingSort()
        {
            var sortedAreas = Figures.OrderBy(key => key.Value.GetArea()).ThenBy(key => key.Value.GetAreaWithoutFrame()); 

            AreaFigures = sortedAreas.ToDictionary(key => key.Key, key => key.Value.GetArea());

            foreach (int area in AreaFigures.Values){
                AverageAreaFigure += area;
            }

            AverageAreaFigure /= AreaFigures.Count;
        }

        public int GetAverageAreaFigure() => AverageAreaFigure;

        public void OutputLastThreeFigures() 
        {
            Console.Clear();

            string[] keys = AreaFigures.Keys.ToArray();

            int consoleWidth = Console.WindowWidth, consoleHeight = Console.WindowHeight;
            int countSpace = 0, centerX = consoleWidth / 2, centerY = consoleHeight / 2, startX, startY;
            
            for (int i = keys.Length - 1; i >= 1; i--)
            {
                try
                {
                    switch (keys[i])
                    {
                        case ("Эллипс"):

                            int radiusX = Figures[keys[i]].Width;
                            int radiusY = Figures[keys[i]].Height;

                            if(radiusX >= centerX | radiusY >= centerY || radiusY * 2 >= Console.WindowHeight) 
                            {
                                Console.WriteLine($"\u001b[31mФигура: {keys[i]} выходит за пределы консоли!\u001b[0m\n");
                                break;
                            }

                            /* Проходимся по всем возможным точкам в консоли
                             * и вычисляем на сколько далеко данная точка находится от контура эллипса
                            */
                            for (int y = 0; y < consoleHeight; y++) 
                            {
                                for (int x = 0; x < consoleWidth; x++)
                                {   
                                    // Уравнение эллипса
                                    double distanceCurrentXFromCenter = Math.Pow((x - centerX) / (double)radiusX, 2);
                                    double distanceCurrentYFromCenter = Math.Pow((y - centerY) / (double)radiusY, 2);

                                    double distance = distanceCurrentXFromCenter + distanceCurrentYFromCenter; // Расстояние от текущей точки до центра эллипса

                                    //Если точка приблизительно находится на границе эллипса, то точка является контуром эллипса

                                    if(Math.Abs(1 - distance) < 0.1) {
                                        Console.Write("*");
                                    }
                                    else {
                                        Console.Write(" ");
                                    }
                                }
                                Console.WriteLine(); // Переход на следующий слой в консоли (ось Y)
                            }
                            break;

                        case ("Круг"):

                            int radius = Figures[keys[i]].Radius;

                            if (radius >= centerX || radius >= centerY)
                            {
                                Console.WriteLine($"\u001b[31mФигура: {keys[i]} выходит за пределы консоли!\u001b[0m\n");
                                break;
                            }

                            //Параметрический метод(проходимся по всем углам и преобразовываем их в радианы)
                            for (double angle = 0; angle < 360; angle += 1)
                            {
                                double radians = angle * (Math.PI / 180.0);

                                //Находим координаты точек на окружности 

                                int x = centerX + (int)Math.Round(radius * Math.Cos(radians));
                                int y = centerY + (int)Math.Round(radius * Math.Sin(radians));

                                if (x >= 0  && y >= 0)
                                {
                                    Console.SetCursorPosition(x, y);
                                    Console.Write("*");
                                }
                            }
                            Console.WriteLine();

                            break;

                        case ("Прямоугольник"):

                            int widthRectangle = Figures[keys[i]].Width;
                            int heightRectangle = Figures[keys[i]].Height;

                            startX = centerX - widthRectangle / 2;
                            startY = centerY - heightRectangle / 2;

                            for (int y = 0; y < heightRectangle; y++)
                            {
                                Console.SetCursorPosition(startX, startY + y);

                                for (int x = 0; x < widthRectangle; x++)
                                {
                                    if (y == 0 || (y + 1) - heightRectangle == 0 || x == 0 || (x + 1) - widthRectangle == 0){
                                        Console.Write("*");
                                    }
                                    else{
                                        Console.Write(" ");
                                    }
                                }
                            }

                            break;

                        case ("Квадрат"):

                            int sideOfSquare = Figures[keys[i]].Width;

                            startX = centerX - sideOfSquare / 2;
                            startY = centerY - sideOfSquare / 2;

                            Console.SetCursorPosition(startX, startY);

                            for (int j = 0; j < sideOfSquare; j++)
                            {
                                for (int q = 0; q < sideOfSquare; q++)
                                {
                                    if (j == 0 | (j + 1) - sideOfSquare == 0 | q == 0 | (q + 1) - sideOfSquare == 0){
                                        Console.Write("* ");
                                    }
                                    else{
                                        Console.Write("  ");
                                    }
                                }
                                Console.SetCursorPosition(startX, startY + (++countSpace));
                            }
                            break;
                    }
                    Thread.Sleep(1000);
                    Console.Clear();

                    countSpace = 0;
                }
                catch(ArgumentOutOfRangeException){

                    Console.Clear();
                    Console.WriteLine($"\u001b[31mФигура: {keys[i]} выходит за пределы консоли!\u001b[0m\n");
                }
            }
        }

        public void Add(Figure figure) 
        {
            if (Figures == null)
                Figures = new Dictionary<string, Figure>();

            Figures.Add(figure.Title, figure);
        }

        public void ToJson(string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(
                Figures.Values, Formatting.Indented, new JsonSerializerSettings 
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }
                ));
        }
    }
}

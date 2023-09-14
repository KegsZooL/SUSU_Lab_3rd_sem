using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace lab3
{   
    class GraphicEditor
    {
        /* Список фигур с модификатором доступа readonly
         * для предотвращения изменения списка после инициализации
        */
        public readonly List<Figure> listFigures = new List<Figure>();

        // Словарь фигур с дочерними объектами класса Figure для сортировки и удобного обращения к ним
        public static Dictionary<string, Figure> Figures { get; private set; }

        // Словарь с площадями фигур 
        public Dictionary<string, int> AreaFigures { get; private set; }

        // Среднее значение площади
        private int AverageAreaFigure { get; set; }

        public GraphicEditor() { }
        
        // Статический конструктор для инициализации единственного экземпляра класса
        static GraphicEditor() => Figures = new Dictionary<string, Figure>();
        
        public void AscendingSort()
        {
            try
            {   // Если после десериализации нужно выполнить сортировку 
                if (listFigures != null && Figures.Count == 0)
                {
                    foreach (var figure in listFigures)
                        Figures.Add(figure.Title, figure);
                }
                
                // Сортируем коллекцию сначала по площади без рамки, а затем по площади с рамкой
                var sortedAreas = Figures.OrderBy(key => key.Value.GetArea()).ThenBy(key => key.Value.GetAreaWithoutFrame());

                // Добавляем в коллекцию отсортированные площади фигур
                AreaFigures = sortedAreas.ToDictionary(key => key.Key, key => key.Value.GetArea()); 

                // Подсчёт средней площади фигуры
                if (AverageAreaFigure == 0)
                {
                    foreach (int area in AreaFigures.Values)
                    {
                        AverageAreaFigure += area;
                    }
                    AverageAreaFigure /= AreaFigures.Count;
                }

            }
            catch{
                Console.WriteLine("\n  Список фигур пуст!");
            }
        }
        
        // Вывод средней площади фигуры
        public int GetAverageAreaFigure() => AverageAreaFigure;

        // Вывод в консоль последних 3-x фигур без учёта толщины рамки
        public void OutputLastThreeFigures() 
        {
            if (Figures.Count == 0 && listFigures.Count == 0) // Выход из метода, если количестов элементов в списке или коллекции = 0
            {
                Console.WriteLine("\n  Список фигур пуст!\n");
                return;
            }
            // Если десериализации нужно выполнить вывод последних 3-x фигур
            if (listFigures != null && Figures.Count == 0)
            {
                foreach (var figure in listFigures)
                    Figures.Add(figure.Title, figure);
            }

            Console.Clear();

            string[] keys = Figures.Keys.ToArray(); // массив ключей для обращения к дочерним объектам Figure

            int consoleWidth = Console.WindowWidth, consoleHeight = Console.WindowHeight; // Размеры консоли
            int countSpace = 0, centerX = consoleWidth / 2, centerY = consoleHeight / 2, startX, startY;
            
            for (int i = keys.Length - 1; i >= 1; i--) // Обход фигур с конца до предпоследнего
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
                             * и вычисляем на сколько далеко данная точка находится от контура эллипса */

                            for (int y = 0; y < consoleHeight; y++) 
                            {
                                for (int x = 0; x < consoleWidth; x++)
                                {   
                                    // Уравнение эллипса
                                    double distanceCurrentXFromCenter = Math.Pow((x - centerX) / (double)radiusX, 2);
                                    double distanceCurrentYFromCenter = Math.Pow((y - centerY) / (double)radiusY, 2);

                                    // Расстояние от текущей точки до центра эллипса
                                    double distance = distanceCurrentXFromCenter + distanceCurrentYFromCenter;

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

                            // Вычисление начальных координат для отрисовки прямоугольника так, чтобы он был центрирован относительно центра экрана
                            startX = centerX - widthRectangle / 2;
                            startY = centerY - heightRectangle / 2;

                            for (int y = 0; y < heightRectangle; y++)
                            {
                                Console.SetCursorPosition(startX, startY + y);

                                for (int x = 0; x < widthRectangle; x++)
                                {
                                    // Определение, является ли текущая позиция на грани прямоугольника (звездочка) или внутри (пробел)
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

                            // Анологично с выводом прямоугольника
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
                    Thread.Sleep(1000); // Задержка перед выводом новой фигуры
                    Console.Clear();

                    countSpace = 0;
                }
                catch(ArgumentOutOfRangeException){ //Обработка исключения для метода Console.SetCursorPosition()

                    Console.Clear();
                    Console.WriteLine($"\u001b[31mФигура: {keys[i]} выходит за пределы консоли!\u001b[0m\n");
                }
            }
        }

        // Добавление дочернего объекта Figure в словарь
        public void Add(Figure figure) => Figures.Add(figure.Title, figure); 
    }
}
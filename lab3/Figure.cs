﻿using Newtonsoft.Json;
using System;

namespace lab3
{
    [JsonObject] // Указываем атрибут для сериализации объкта
    abstract class Figure // Базовый класс фигур
    {   
        // Свойства для определения основных характеристик фигуру
        public int FrameThickness { get; }
        public int Radius { get; }
        public int Width  { get; }
        public int Height { get; }
        public string Title { get; set; }

        public Figure(int _frameThickness, int _radius) // Конструктор для круга
        {
            FrameThickness = _frameThickness;
            Radius = _radius;
        }        
        
        public Figure(int _frameThickness, int _width, int _height) // Конструктор для остальных фигур
        {
            FrameThickness = _frameThickness;
            Width = _width;
            Height = _height;
        }

        // Создаем контракты для дочерних классов(расчёт площади с учётом толщины рамки и без учёта)
        public abstract int GetAreaWithoutFrame();
        
        public abstract int GetArea();
    }

    // Не стал выносить каждый дочерний класс в отдельный файл, что бы не пришлось качать 4-е доп. файла
    class Square : Figure // Квадрат
    {
        public Square(int frameThickness, int width, int height) : base(frameThickness, width, height) => Title = "Квадрат";
        
        public override int GetAreaWithoutFrame() => (int)(Math.Pow(Width + (2 * FrameThickness), 2) - Math.Pow(Width, 2));

        public override int GetArea() => (int)(Math.Pow(Width, 2));
    }

    class Rectangle : Figure // Прямоугольник
    {
        public Rectangle(int frameThickness, int width, int height) : base(frameThickness, width, height) => Title = "Прямоугольник";

        public override int GetAreaWithoutFrame() => (int)((Width + 2 * FrameThickness) * (Height + 2 * FrameThickness) - (Width * Height));

        public override int GetArea() => (int)(Width * Height);
    }

    class Ellipse : Figure // Эллипс
    {
        public Ellipse(int frameThickness, int width, int height) : base(frameThickness, width, height) => Title = "Эллипс";
        
        public override int GetAreaWithoutFrame() => (int)(Math.PI * (Width + FrameThickness) * (Height + FrameThickness) - Math.PI * Width * Height);

        public override int GetArea() => (int)(Math.PI * Width * Height);
    }

    class Circle : Figure //Круг
    {
        public Circle(int frameThickness, int radius) : base(frameThickness, radius) => Title = "Круг";
        
        public override int GetAreaWithoutFrame() => (int)(Math.PI * (Math.Pow(Radius + FrameThickness, 2) - Math.Pow(Radius, 2)));

        public override int GetArea() => (int)(Math.PI * Math.Pow(Radius, 2));
    }
}
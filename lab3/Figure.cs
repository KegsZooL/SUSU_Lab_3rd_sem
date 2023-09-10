using System;

namespace lab3
{
    abstract class Figure
    {   
        public double FrameThickness { get; }
        public double Radius { get; }
        public double Length { get; }
        public double Height { get; }

        public Figure(double _frameThickness, double _radius) 
        {
            FrameThickness = _frameThickness;
            Radius = _radius;
        }        
        
        public Figure(double _frameThickness, double _length, double _height) 
        {
            FrameThickness = _frameThickness;
            Length = _length;
            Height = _height;
        }        
            
        public abstract double GetAreaWithoutFrame();
        
        public abstract double GetArea();
    }

    class Circle : Figure //Круг
    {   
        public Circle(double _frameThickness, double _radius) : base(_frameThickness, _radius){ }

        public override double GetAreaWithoutFrame() => Math.Round(Math.PI * (Math.Pow(Radius + FrameThickness, 2) - 
            Math.Pow(Radius, 2)), digits: 2);
        
        public override double GetArea() => Math.Round(Math.PI * Math.Pow(Radius, 2), digits: 2);

    }

    class Square : Figure // Квадрат
    {
        public Square(double _frameThickness, double _length, double _height) : base(_frameThickness, _length, _height) { }

        public override double GetAreaWithoutFrame() => Math.Round(Math.Pow(Length + (2 * FrameThickness), 2) - 
            Math.Pow(Length, 2), digits: 2);
        
        public override double GetArea() => Math.Round(Math.Pow(Length, 2), digits: 2);
    }
    
    class Ellipse : Figure // Эллипс
    {
        public Ellipse(double _frameThickness, double _length, double _height) : base(_frameThickness, _length, _height) { }

        public override double GetAreaWithoutFrame() => Math.Round(Math.PI * (Length + FrameThickness) * (Height + FrameThickness) -
            Math.PI * Length * Height, digits: 2); 
        
        public override double GetArea() => Math.Round(Math.PI * Length * Height, digits: 2);
    }
    
    class Rectangle : Figure // Прямоугольник
    {
        public Rectangle(double _frameThickness, double _length, double _height) : base(_frameThickness, _length, _height) { }

        public override double GetAreaWithoutFrame() => Math.Round((Length + 2 * FrameThickness) * (Height + 2 * FrameThickness) -
            (Length * Height), digits: 2);        
        
        public override double GetArea() => Math.Round(Length * Height, digits: 2);
    }
}

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
            
        public abstract double GetSquare();
    }

    class Circle : Figure
    {   
        public Circle(double _frameThickness, double _radius) : base(_frameThickness, _radius){ }

        public override double GetSquare() => Math.PI * (Math.Pow(Radius + FrameThickness, 2) - Math.Pow(Radius, 2));
    }

    class Square : Figure
    {
        public Square(double _frameThickness, double _length, double _height) : base(_frameThickness, _length, _height) { }

        public override double GetSquare() => Math.Pow(Length + (2 * FrameThickness), 2) - Math.Pow(Length, 2);
    }
    
    class Ellipse : Figure
    {
        public Ellipse(double _frameThickness, double _length, double _height) : base(_frameThickness, _length, _height) { }

        public override double GetSquare() => Math.PI * (Length + FrameThickness) * (Height + FrameThickness) - Math.PI * Length * Height;
    }
    
    class Rectangle : Figure
    {
        public Rectangle(double _frameThickness, double _length, double _height) : base(_frameThickness, _length, _height) { }

        public override double GetSquare() => (Length + 2 * FrameThickness) * (Height + 2 * FrameThickness) - (Length * Height);
    }
}

using System;

namespace lab3
{
    abstract class Figure
    {   
        public int FrameThickness { get; }
        public int Radius { get; }
        public int Length { get; }
        public int Height { get; }

        public Figure(int _frameThickness, int _radius) 
        {
            FrameThickness = _frameThickness;
            Radius = _radius;
        }        
        
        public Figure(int _frameThickness, int _length, int _height) 
        {
            FrameThickness = _frameThickness;
            Length = _length;
            Height = _height;
        }        
            
        public abstract int GetAreaWithoutFrame();
        
        public abstract int GetArea();
    }

    class Circle : Figure //Круг
    {   
        public Circle(int _frameThickness, int _radius) : base(_frameThickness, _radius){ }

        public override int GetAreaWithoutFrame() => (int)(Math.PI * (Math.Pow(Radius + FrameThickness, 2) - Math.Pow(Radius, 2)));
        
        public override int GetArea() => (int)(Math.PI * Math.Pow(Radius, 2));

    }

    class Square : Figure // Квадрат
    {
        public Square(int _frameThickness, int _length, int _height) : base(_frameThickness, _length, _height) { }

        public override int GetAreaWithoutFrame() => (int)(Math.Pow(Length + (2 * FrameThickness), 2) - Math.Pow(Length, 2));
        
        public override int GetArea() => (int)(Math.Pow(Length, 2));
    }
    
    class Ellipse : Figure // Эллипс
    {
        public Ellipse(int _frameThickness, int _length, int _height) : base(_frameThickness, _length, _height) { }

        public override int GetAreaWithoutFrame() => (int)(Math.PI * (Length + FrameThickness) * (Height + FrameThickness) - Math.PI * Length * Height); 
        
        public override int GetArea() => (int)(Math.PI * Length * Height);
    }
    
    class Rectangle : Figure // Прямоугольник
    {
        public Rectangle(int _frameThickness, int _length, int _height) : base(_frameThickness, _length, _height) { }

        public override int GetAreaWithoutFrame() => (int)((Length + 2 * FrameThickness) * (Height + 2 * FrameThickness) - (Length * Height));        
        
        public override int GetArea() => (int)(Length * Height);
    }
}

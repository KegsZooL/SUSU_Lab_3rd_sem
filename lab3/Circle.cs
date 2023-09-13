using System;

namespace lab3
{
    class Circle : Figure //Круг
    {
        public Circle(int frameThickness, int radius) : base(frameThickness, radius) {
            Title = "Круг";
        }

        public override int GetAreaWithoutFrame() => (int)(Math.PI * (Math.Pow(Radius + FrameThickness, 2) - Math.Pow(Radius, 2)));

        public override int GetArea() => (int)(Math.PI * Math.Pow(Radius, 2));
    }
}

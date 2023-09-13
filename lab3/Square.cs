using System;

namespace lab3
{
    class Square : Figure // Квадрат
    {
        public Square(int frameThickness, int width, int height) : base(frameThickness, width, height) {
            Title = "Квадрат";
        }

        public override int GetAreaWithoutFrame() => (int)(Math.Pow(Width + (2 * FrameThickness), 2) - Math.Pow(Width, 2));

        public override int GetArea() => (int)(Math.Pow(Width, 2));
    }
}

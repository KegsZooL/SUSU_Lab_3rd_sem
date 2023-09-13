using System;

namespace lab3
{
    class Ellipse : Figure // Эллипс
    {
        public Ellipse(int frameThickness, int width, int height) : base(frameThickness, width, height) {
            Title = "Эллипс";
        }

        public override int GetAreaWithoutFrame() => (int)(Math.PI * (Width + FrameThickness) * (Height + FrameThickness) - Math.PI * Width * Height);

        public override int GetArea() => (int)(Math.PI * Width * Height);
    }
}

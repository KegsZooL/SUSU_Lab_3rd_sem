namespace lab3
{
    class Rectangle : Figure // Прямоугольник
    {
        public Rectangle(int frameThickness, int width, int height) : base(frameThickness, width, height) {
            Title = "Прямоугольник";
        }

        public override int GetAreaWithoutFrame() => (int)((Width + 2 * FrameThickness) * (Height + 2 * FrameThickness) - (Width * Height));

        public override int GetArea() => (int)(Width * Height);
    }
}

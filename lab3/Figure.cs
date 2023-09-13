using System;

namespace lab3
{
    [Serializable]
    abstract class Figure
    {   
        public int FrameThickness { get; }
        public int Radius { get; }
        public int Width  { get; }
        public int Height { get; }

        public string Title { get; set; }

        public Figure(int _frameThickness, int _radius) 
        {
            FrameThickness = _frameThickness;
            Radius = _radius;
        }        
        
        public Figure(int _frameThickness, int _width, int _height) 
        {
            FrameThickness = _frameThickness;
            Width = _width;
            Height = _height;
        }        
            
        public abstract int GetAreaWithoutFrame();
        
        public abstract int GetArea();
    }
}

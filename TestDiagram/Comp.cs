using System;
using System.Collections.Generic;
using System.Drawing;

namespace TestDiagram
{
    public class Comp
    {
        protected Pen pen = new Pen(Color.Black);

        // Component location, width, and height attributes
        public Point loc;
        public int width;
        public int height;

        public Comp()
        {
            loc = new Point(100, 100);
            width = 100;
            height = 100;
        }

        public virtual void Draw(Graphics gr)
        {
            // Draw a simple rectangle with black border and no fill color
            gr.DrawRectangle(pen, loc.X, loc.Y, width, height);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;

namespace TestDiagram
{
    class Rectangle : Comp
    {
        public Rectangle()
        {
            loc = new Point(250, 100);
        }

        public override void Draw(Graphics gr)
        {
            // Draw a simple rectangle with black border and no fill color
            gr.DrawRectangle(pen, loc.X, loc.Y, width, height);
        }
    }
}
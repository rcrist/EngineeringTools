using System;
using System.Collections.Generic;
using System.Drawing;

namespace TestDiagram
{
    class Rectangle : Comp
    {
        public Rectangle()
        {

        }

        public override void Draw(Graphics gr)
        {
            // Draw a simple rectangle with black border and no fill color
            gr.DrawRectangle(pen, 250, 100, 100, 100);
        }
    }
}

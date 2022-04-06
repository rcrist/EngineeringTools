using System;
using System.Collections.Generic;
using System.Drawing;

namespace TestDiagram
{
    class Circle : Comp
    {
        public Circle()
        {

        }

        public override void Draw(Graphics gr)
        {
            // Draw a simple rectangle with black border and no fill color
            gr.DrawEllipse(pen, 400, 100, 100, 100);
        }
    }
}

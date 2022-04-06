using System;
using System.Collections.Generic;
using System.Drawing;

namespace TestDiagram
{
    public class Comp
    {
        Pen pen = new Pen(Color.Black);

        public Comp()
        {

        }

        public void Draw(Graphics gr)
        {
            // Draw a simple rectangle with black border and no fill color
            gr.DrawRectangle(pen, 100, 100, 100, 100);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;

namespace TestDiagram
{
    class Triangle : Comp
    {
        // Define 3 point attributes for the triangle
        Point p1, p2, p3;

        public Triangle()
        {
            loc = new Point(550, 100);
        }

        public override void Draw(Graphics gr)
        {
            // Initialize the points to the position of each corner of the triangle
            p1 = new Point(loc.X, loc.Y + height);
            p2 = new Point(loc.X + width, loc.Y + height);
            p3 = new Point(loc.X + width / 2, loc.Y);

            // Draw a simple triangle with black border and no fill color
            gr.DrawLine(pen, p1, p2);
            gr.DrawLine(pen, p2, p3);
            gr.DrawLine(pen, p3, p1);
        }
    }
}

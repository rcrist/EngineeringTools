using System;
using System.Collections.Generic;
using System.Drawing;

namespace TestDiagram
{
    class Triangle : Comp
    {
        // Define the points for the upper left corner of the shape
        int locX = 550;
        int locY = 100;

        // Define 3 point attributes for the triangle
        Point p1, p2, p3;

        public Triangle()
        {
            // Initialize the points to the position of each corner of the triangle
            p1 = new Point(locX, locY + 100);
            p2 = new Point(locX + 100, locY + 100);
            p3 = new Point(locX + 50, locY);
        }

        public override void Draw(Graphics gr)
        {
            // Draw a simple triangle with black border and no fill color
            gr.DrawLine(pen, p1, p2);
            gr.DrawLine(pen, p2, p3);
            gr.DrawLine(pen, p3, p1);
        }
    }
}

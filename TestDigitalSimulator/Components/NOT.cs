using System;
using System.Collections.Generic;
using System.Drawing;
using TestDigitalSimulator.Components;

namespace TestDigitalSimulator.Components
{
    public class NOT : Comp
    {
        Point p0, p1, p2, p3, p4, p5, p6, p7;

        public NOT()
        {
            setLogicState();
        }

        public override void Draw(Graphics gr)
        {
            // Define the component symbol points relative to component location
            p0 = loc; // Origin = component location in screen coordinates
            p1 = new Point(p0.X, p0.Y + 20);
            p2 = new Point(p0.X + 10, p1.Y);
            p3 = new Point(p2.X, p0.Y);
            p4 = new Point(p2.X + 40, p1.Y);
            p5 = new Point(p3.X, p0.Y + 40);
            p6 = new Point(p4.X + 4, p1.Y);
            p7 = new Point(p0.X + 60, p1.Y);

            // Draw input and output leads
            gr.DrawLine(offPen, p1, p2);
            gr.DrawLine(offPen, p6, p7);

            // Draw the triangle
            gr.DrawLine(offPen, p3, p4);
            gr.DrawLine(offPen, p4, p5);
            gr.DrawLine(offPen, p5, p3);

            // Draw the circle
            gr.DrawEllipse(offPen, p4.X, p4.Y - 2, 4, 4);
        }

        public override void setLogicState()
        {
            // Assume that the input pin is Pin[0]
            Pout = !Pin[0]; // Pout = NOT Pin
        }
    }
}
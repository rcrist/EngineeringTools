// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MicrowaveTools.Components.Microstrip
{
    public class MLIN : Comp
    {
        public MLIN()
        {

        }

        public MLIN(float value, Point location, int[] nodes)
        {
            Type = "MLIN";
            Value = value;
            Loc = location;
            Nodes = nodes;
            Pout = Loc;

            // Print a debug message on the output console
            print();
        }

        // Let the MLIN draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
            Point p2 = new Point(p1.X + 10, p1.Y);
            Point p3 = new Point(p2.X, p2.Y - 10); // Location of MLIN rectangle
            Point p4 = new Point(p2.X + 40, p2.Y);
            Point p5 = new Point(p4.X + 10, p4.Y);

            gr.DrawLine(drawPen, p1, p2);
            gr.DrawLine(drawPen, p4, p5);
            gr.DrawRectangle(drawPen, p3.X, p3.Y, 40, 20);

        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " Z: " + this.Value + " " + "[" + this.Nodes[0] + "]");
        }
    }
}

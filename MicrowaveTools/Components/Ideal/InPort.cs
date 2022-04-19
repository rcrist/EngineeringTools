// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MicrowaveTools.Components.Ideal
{
    public class InPort : Comp
    {
        public InPort()
        {

        }

        public InPort(float value, Point location, int[] nodes)
        {
            Type = "Pin";
            Value = value;
            Loc = location;
            Nodes = nodes;
            Pout = Loc;

            // Print a debug message on the output console
            print();
        }

        //// Let the InPort draw itself called from the canvas paint event
        //public override void Draw(Graphics gr)
        //{
        //    Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
        //    Point p2 = new Point(p1.X - leadL, p1.Y);
        //    Point p3 = new Point(p2.X - 10, p2.Y - 10);
        //    Point p4 = new Point(p3.X - 30, p3.Y);
        //    Point p5 = new Point(p4.X, p4.Y + 20);
        //    Point p6 = new Point(p5.X + 30, p5.Y);

        //    gr.DrawLine(drawPen, p1, p2);
        //    gr.DrawLine(drawPen, p2, p3);
        //    gr.DrawLine(drawPen, p3, p4);
        //    gr.DrawLine(drawPen, p4, p5);
        //    gr.DrawLine(drawPen, p5, p6);

        //    gr.DrawLine(drawPen, p6, p2);

        //    // Draw the component text
        //    compText = "Pin";
        //    pt = new Point(Loc.X - 50, Loc.Y - 35);
        //    drawCompText(gr, pt, compText);
        //}

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " Z: " + this.Value + "ohms " + "[" + this.Nodes[0] + "]");
        }
    }
}

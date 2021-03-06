using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveTools.Components.Ideal
{
    class InPort : Comp
    {
        public InPort()
        {
            Width = 60;
            Height = 60;
        }

        public InPort(float value, Point location, int[] nodes)
        {
            Type = "Pin";
            Value = value;
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
        }

        // Let the InPort draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
            Point p2 = new Point(p1.X - leadL, p1.Y);
            Point p3 = new Point(p2.X - 10, p2.Y - 10);
            Point p4 = new Point(p3.X - 30, p3.Y);
            Point p5 = new Point(p4.X, p4.Y + 20);
            Point p6 = new Point(p5.X + 30, p5.Y);

            gr.DrawLine(drawPen, p1, p2);
            gr.DrawLine(drawPen, p2, p3);
            gr.DrawLine(drawPen, p3, p4);
            gr.DrawLine(drawPen, p4, p5);
            gr.DrawLine(drawPen, p5, p6);

            gr.DrawLine(drawPen, p6, p2);

            // Create string to draw.
            String drawString = "Pin";

            // Create point for upper-left corner of drawing.
            float x = p1.X - 80;
            float y = p1.Y - 7;

            // Draw string to screen.
            gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " Z: " + this.Value + "ohms " + "[" + this.Nodes[0] + "]");
        }
    }
}

// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MicrowaveTools.Components.Microstrip
{
    public class MTEE : Comp
    {
        public MTEE()
        {

        }

        public MTEE(float length, Point location, int[] nodes)
        {
            Type = "MTEE";
            Orientation = "Series";
            Loc = location;
            Nodes = nodes;
            Pout = Loc;

            // Print a debug message on the output console
            print();
        }

        // Let the MTEE draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            Point[] p = new Point[21];
            p[1] = Loc;            // Assume p1 is the input lead to the left
            p[2] = new Point(p[1].X + 10, p[1].Y);
            p[3] = new Point(p[2].X, p[2].Y - 10);
            p[4] = new Point(p[3].X + 10, p[3].Y);
            p[5] = new Point(p[4].X, p[4].Y - 10);
            p[6] = new Point(p[5].X + 20, p[5].Y);
            p[7] = new Point(p[6].X, p[6].Y + 10);
            p[8] = new Point(p[7].X + 10, p[7].Y);
            p[9] = new Point(p[8].X, p[8].Y + 20);
            p[14] = new Point(p[9].X - 40, p[9].Y);
            p[15] = new Point(p[5].X + 10, p[5].Y - 10);
            p[16] = new Point(p[15].X, p[15].Y + 10);
            p[17] = new Point(p[8].X + 10, p[8].Y + 10);
            p[18] = new Point(p[17].X - 10, p[17].Y);

            for (int i = 1; i < 9; i++)
                gr.DrawLine(drawPen, p[i], p[i + 1]);
            gr.DrawLine(drawPen, p[9], p[14]);
            gr.DrawLine(drawPen, p[14], p[2]);
            for (int i = 15; i < 19; i += 2)
                gr.DrawLine(drawPen, p[i], p[i + 1]);
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " Length: " + this.Value + " " + "[" + this.Nodes[0] + "]");
        }
    }
}

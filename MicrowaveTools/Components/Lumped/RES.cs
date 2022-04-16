using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MicrowaveTools.Components.Lumped
{
    public class RES : Comp
    {
        public RES()
        {

        }

        public RES(float value, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "Res";
            Value = value;
            Loc = location;
            Nodes = nodes;
            Pin = Loc;
            Pout = new Point(Loc.X + compL, Loc.Y);

            // Print debug message to the output console
            print();
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " R: " + this.Value + "\t[" + this.Nodes[0] + ", " + this.Nodes[1] + "]");
        }

        public override void Draw(Graphics gr)
        {
            // Draw the resistor body
            for (int i = 1; i < 5; i++)
            {
                gr.DrawLine(drawPen, Loc.X + leadL * (i), Loc.Y, Loc.X + leadL * (i) + 3, Loc.Y - leadL);
                gr.DrawLine(drawPen, Loc.X + leadL * (i) + 3, Loc.Y - leadL, Loc.X + leadL * (i) + 6, Loc.Y + leadL);
                gr.DrawLine(drawPen, Loc.X + leadL * (i) + 6, Loc.Y + leadL, Loc.X + leadL * (i + 1), Loc.Y);
            }

            // Draw the input and output leads
            gr.DrawLine(drawPen, Loc.X, Loc.Y, Loc.X + leadL, Loc.Y);           // Input lead
            gr.DrawLine(drawPen, Loc.X + bodyL + leadL, Loc.Y, Loc.X + compL, Loc.Y);   // Output lead

            // Draw the component text
            compText = "R = " + this.Value + "Ω";
            pt = new Point(Loc.X+5, Loc.Y-35);
            drawCompText(gr, pt, compText);
        }
    }
}

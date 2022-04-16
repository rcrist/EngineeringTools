// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;


namespace MicrowaveTools.Components.Lumped
{
    public class IND : Comp
    {
        public IND()
        {

        }

        public IND(float value, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "Ind";
            Value = value;
            Loc = location;
            Nodes = nodes;
            Pin = Loc;
            Pout = new Point(Loc.X + compL, Loc.Y);

            // Print a debug message on the output console
            print();
        }

        public override void Draw(Graphics gr)
        {
            // Draw the inductor body curves
            for (int i = 1; i < 5; i++)
            {
                float startAngle = 180;
                float sweepAngle = 180;
                Rectangle rect = new Rectangle(Loc.X + leadL * (i), Loc.Y - leadL - 5, leadL, leadL);
                gr.DrawArc(drawPen, rect, startAngle, sweepAngle);
            }

            // Draw the inductor body vertical lines
            for (int i = 1; i < 6; i++)
            {
                gr.DrawLine(drawPen, Loc.X + leadL * (i), Loc.Y, Loc.X + leadL * (i), Loc.Y - leadL);
            }

            // Draw the input and output leads
            gr.DrawLine(drawPen, Loc.X, Loc.Y, Loc.X + leadL, Loc.Y);                   // Input lead
            gr.DrawLine(drawPen, Loc.X + bodyL + leadL, Loc.Y, Loc.X + compL, Loc.Y);   // Output lead

            // Draw the component text
            compText = "L = " + this.Value + "nH";
            pt = new Point(Loc.X + 5, Loc.Y - 35);
            drawCompText(gr, pt, compText);
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " L: " + this.Value + "\t[" + this.Nodes[0] + "," + this.Nodes[1] + "]");
        }
    }
}

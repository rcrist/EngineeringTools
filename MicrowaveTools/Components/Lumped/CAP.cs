// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;


namespace MicrowaveTools.Components.Lumped
{
    public class CAP : Comp
    {
        public CAP()
        {

        }

        public CAP(float value, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "Cap";
            Value = value;
            Loc = location;
            Nodes = nodes;
            Pin = Loc;
            Pout = new Point(Loc.X + compL, Loc.Y);

            // Print a debug message on the output console
            print();
        }

        //// Let the Capacitor draw itself called from the canvas paint event
        //public override void Draw(Graphics gr)
        //{
        //    // Draw the capacitor body curves
        //    float startAngle = 90;
        //    float sweepAngle = 180;
        //    Rectangle rect = new Rectangle(Loc.X + 3 * leadL, Loc.Y - 10, leadL, leadL * 2);
        //    gr.DrawArc(drawPen, rect, startAngle, sweepAngle);

        //    // Draw the capacitor body vertical line
        //    gr.DrawLine(drawPen, Loc.X + 2 * leadL + 5, Loc.Y - leadL, Loc.X + 2 * leadL + 5, Loc.Y + leadL);

        //    // Draw the input and output leads
        //    gr.DrawLine(drawPen, Loc.X, Loc.Y, Loc.X + 2 * leadL + 5, Loc.Y);                   // Input lead
        //    gr.DrawLine(drawPen, Loc.X + 3 * leadL, Loc.Y, Loc.X + compL, Loc.Y);   // Output lead

        //    // Draw the component text
        //    compText = "C = " + this.Value + "pF";
        //    pt = new Point(Loc.X + 5, Loc.Y - 35);
        //    drawCompText(gr, pt, compText);
        //}

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " C: " + this.Value + "\t[" +
                                       this.Nodes[0] + "," + this.Nodes[1] + "]");
        }
    }
}

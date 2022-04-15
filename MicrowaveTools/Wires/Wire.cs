// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

// Microwave Tools Libraries
using MicrowaveTools.Components;

namespace MicrowaveTools.Wires
{
    public class Wire : Comp
    {
        // Create wire start and end points
        public Point Pt1 = new Point();
        public Point Pt2 = new Point();

        // Add components connected to the input and output of the wire
        public Comp Cin = new Comp();
        public Comp Cout = new Comp();

        // End caps variables
        private const int endcap_radius = 3;
        public bool endcapsVisible = false;

        public Wire()
        {

        }

        public Wire(Comp cin, Comp cout)
        {
            Cin = cin;
            Cout = cout;

            Pt1 = new Point(cin.Pout.X, cin.Pout.Y);
            Pt2 = new Point(cout.Pin.X, cin.Pout.Y);
        }

        public override void Draw(Graphics gr)
        {
            if (Pt1.X != Pt2.X || Pt1.Y != Pt2.Y)
            {
                // Draw L-shaped wire
                gr.DrawLine(drawPen, Pt1.X, Pt1.Y, Pt1.X, Pt2.Y); // Vertical line
                gr.DrawLine(drawPen, Pt1.X, Pt2.Y, Pt2.X, Pt2.Y); // Horizontal line
            }
            else
            {
                // Draw straight wire
                gr.DrawLine(drawPen, Pt1, Pt2);
            }

            // Draw the wire end caps
            drawEndCaps(gr);
        }

        private void drawEndCaps(Graphics gr)
        {
            if (this.endcapsVisible)
            {
                // Draw custom end cap for Pt1
                Rectangle rect1 = new Rectangle(
                     Pt1.X - endcap_radius, Pt1.Y - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect1);     // Rectangular end cap

                // Draw custom end cap for Pt2
                Rectangle rect2 = new Rectangle(
                     Pt2.X - endcap_radius, Pt2.Y - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect2);    // Rectangular end cap
            }
        }
    }
}

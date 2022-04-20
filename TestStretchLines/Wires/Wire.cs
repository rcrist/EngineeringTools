// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

// Microwave Tools Libraries
using TestStretchLInes.Components;

namespace TestStretchLInes.Wires
{
    public class Wire : Comp
    {
        // Create wire start and end points
        public Point Pt1 = new Point();
        public Point Pt2 = new Point();

        // Add components connected to the input and output of the wire
        public Comp Cin = new Comp();
        public Comp Cout = new Comp();

        public Wire()
        {

        }

        public Wire(Comp cin, Comp cout)
        {
            Cin = cin;
            Cout = cout;

            Cin.wires.Add(this);

            Pt1 = new Point(cin.Pout.X, cin.Pout.Y);
            Pt2 = new Point(cout.Pin.X, cin.Pout.Y);

            Loc = Pt1;

            Width = Math.Abs(Pt2.X - Pt1.X);
            Height = Math.Abs(Pt2.Y - Pt1.Y);
            if (Height == 0)
                Height = 10;

            boundBox = new Rectangle(Pt1.X, Pt1.Y, Width, Height);
        }

        public override void Draw(Graphics gr)
        {
            // Draw the wire end caps
            drawEndCaps(gr);
            checkSelect();

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
        }

        private void drawEndCaps(Graphics gr)
        {
            if (this.endcapsVisible || this.isSelected)
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

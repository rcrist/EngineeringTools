// C# class libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

// Engineering Tools class libraries
using EngineeringTools.Components;

namespace EngineeringTools.Wires
{
    class Wire : Comp
    {
        // Create wire start and end points
        public Point Pt1 = new Point();
        public Point Pt2 = new Point();

        // End caps variables
        private const int endcap_radius = 3;
        public bool endcapsVisible = false;

        public Wire()
        {

        }

        public override void Draw(Graphics gr)
        {
            if (Pt1.X != Pt2.X || Pt1.Y != Pt2.Y)
            {
                // Draw L-shaped wire
                gr.DrawLine(pen, Pt1.X, Pt1.Y, Pt1.X, Pt2.Y); // Vertical line
                gr.DrawLine(pen, Pt1.X, Pt2.Y, Pt2.X, Pt2.Y); // Horizontal line
            }
            else
            {
                // Draw straight wire
                gr.DrawLine(pen, Pt1, Pt2);
            }

            // Draw the wire end caps
            drawEndCaps(gr);
        }

        private void drawEndCaps(Graphics gr)
        {
            if (this.endcapsVisible)
            {
                // Draw custom end cap for Pt1
                gr.DrawRectangle(Pens.Red, Pt1.X - endcap_radius, Pt1.Y - endcap_radius,
                        2 * endcap_radius, 2 * endcap_radius);     // Rectangular end cap

                // Draw custom end cap for Pt2
                gr.DrawRectangle(Pens.Red, Pt2.X - endcap_radius, Pt2.Y - endcap_radius,
                        2 * endcap_radius, 2 * endcap_radius);    // Rectangular end cap
            }
        }
    }
}

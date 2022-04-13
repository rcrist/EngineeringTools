// C# class libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

// Engineering Tools class libraries
using TestDigitalSimulator.Components;

namespace TestDigitalSimulator.Wires
{
    public class Wire : Comp
    {
        // Create wire start and end points
        public Point Pt1 = new Point();
        public int inCompPout;              // Variable to store Pout of the component on the imput of wire
        public Point Pt2 = new Point();
        public int outCompPin;              // Variable to store Pin of the component on the output of wire


        // Store the input and output components connected to the wire
        public Comp inComp = new Comp();    // Variable to store the component on the input side of the wire
        public Comp outComp = new Comp();   // Variable to store the component on the output side of the wire

        public bool logicState = false;

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

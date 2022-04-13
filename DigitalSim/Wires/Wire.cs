using DigitalSim.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSim.Wires
{
    public class Wire
    {
        public Comp inComp = new Comp();    // Variable to store the component on the input side of the wire
        public int inCompPout;              // Variable to store Pout of the component on the imput of wire
        public Comp outComp = new Comp();   // Variable to store the component on the output side of the wire
        public int outCompPin;              // Variable to store Pin of the component on the output of wire
        public bool logicState = false;

        public PointF Pt1 = new PointF();
        public PointF Pt2 = new PointF();
        public Pen offPen = new Pen(Color.Black, 2);
        public Pen onPen = new Pen(Color.Green, 2);

        private const int endcap_radius = 3;
        public bool endcapsVisible = false;

        public Wire()
        {

        }

        public Wire(Pen pen)
        {
            offPen = pen;
        }

        public Wire(Comp incomp, int incomppout, Comp outcomp, int outcomppin)
        {
            inComp = incomp;
            inCompPout = incomppout; // Index to Pout pin number
            outComp = outcomp;
            outCompPin = outcomppin; // Index to Pin pin number
        }

        // Let the wire draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            if (logicState)
                // Draw the main wire straight line
                gr.DrawLine(this.onPen, Pt1, Pt2);
            else
                gr.DrawLine(this.offPen, Pt1, Pt2);

            // Draw the wire end caps
            drawEndCaps(gr);
        }

        private void drawEndCaps(Graphics gr)
        {
            if (this.endcapsVisible)
            {
                // Draw custom end cap for Pt1
                Rectangle rect1 = new Rectangle(
                     (int)Pt1.X - endcap_radius, (int)Pt1.Y - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect1);     // Rectangular end cap

                // Draw custom end cap for Pt2
                Rectangle rect2 = new Rectangle(
                     (int)Pt2.X - endcap_radius, (int)Pt2.Y - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect2);    // Rectangular end cap
            }
        }

        public void printWire()
        {
            Debug.WriteLine("Wire: " + this.ToString() +
                            " Pt1: " + this.Pt1.ToString() +
                            " Pt2: " + this.Pt2.ToString() +
                            " state: " + this.logicState);

        }
    }
}

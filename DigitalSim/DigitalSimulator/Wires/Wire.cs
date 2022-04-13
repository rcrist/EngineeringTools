using DigitalSimulator.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSimulator.Wires
{
    public class Wire
    {
        public DigitalComponent[] Nodes = new DigitalComponent[2];
        public PointF Pt1 = new PointF();
        public PointF Pt2 = new PointF();
        public Pen offPen = new Pen(Color.Black, 2);
        public Pen onPen = new Pen(Color.Green, 2);

        private const int endcap_radius = 3;
        public bool endcapsVisible = false;

        // Properties used by algorithms.
        public bool Visited = false;

        public bool logicState = false;

        public Wire()
        {

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
    }
}

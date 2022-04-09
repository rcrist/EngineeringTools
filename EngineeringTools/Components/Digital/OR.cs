using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace EngineeringTools.Components.Digital
{
    class OR : DigComp
    {
        public bool logicState = false;
        const int leadLength = 10;
        const int gateSize = 60;

        public OR()
        {
            setLogicState();
        }

        public override void setLogicState()
        {
            Pout = Pin[0] || Pin[1];  // Pout = Pin1 OR Pin2
            printGate();
        }

        // Let the OrGate draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Draw input curve
            float startAngle = -70F;
            float sweepAngle = 135.0F;
            Rectangle rect = new Rectangle((int)loc.X - 25, (int)loc.Y + leadLength, 40, 40);
            gr.DrawArc(offPen, rect, startAngle, sweepAngle);

            // Draw bottom curve
            float bottomstartAngle = 0F;
            float bottomsweepAngle = 90.0F;
            Rectangle bottomrect = new Rectangle((int)loc.X - 37, (int)loc.Y + leadLength, 80, 38);
            gr.DrawArc(offPen, bottomrect, bottomstartAngle, bottomsweepAngle);

            // Draw top curve
            float topstartAngle = 270F;
            float topsweepAngle = 90.0F;
            Rectangle toprect = new Rectangle((int)loc.X - 37, (int)loc.Y + leadLength, 80, 38);
            gr.DrawArc(offPen, toprect, topstartAngle, topsweepAngle);

            // Draw in input and output lines based on the logic state
            if (logicState)
                drawShape(gr, onPen);
            else
                drawShape(gr, offPen);
        }

        private void drawShape(Graphics gr, Pen pen)
        {
            // Draw input lines
            gr.DrawLine(pen, new Point((int)loc.X, (int)loc.Y + 20), new Point((int)loc.X + 10, (int)loc.Y + 20));
            gr.DrawLine(pen, new Point((int)loc.X, (int)loc.Y + 40), new Point((int)loc.X + 10, (int)loc.Y + 40));

            // Draw output line
            gr.DrawLine(pen, new Point((int)loc.X + 45, (int)loc.Y + 30), new Point((int)loc.X + 60, (int)loc.Y + 30));
        }

        public void printGate()
        {
            Debug.WriteLine("Gate: " + this.ToString() +
                            " Pin1: " + this.Pin[0] +
                            " Pin2: " + this.Pin[1] +
                            " Pout: " + this.Pout);

        }
    }
}

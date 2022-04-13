using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSim.Components
{
    class OR : Comp
    {
        public OR()
        {
            Pin = new bool[2];
            Pout = new bool[1];
            InitGate();
        }

        public OR(Pen pen)
        {
            Pin = new bool[2];
            Pout = new bool[1];
            offPen = pen;
            InitGate();
        }

        private void InitGate()
        {
            Pin[0] = false;
            Pin[1] = false;
            SetOutput();
        }

        public override void SetOutput()
        {
            Pout[0] = Pin[0] || Pin[1];  // Pout = Pin1 OR Pin2
            printGate();
        }

        // Let the OrGate draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            // Draw input curve
            float startAngle = -70F;
            float sweepAngle = 135.0F;
            Rectangle rect = new Rectangle((int)Location.X - 25, (int)Location.Y + leadLength, 40, 40);
            gr.DrawArc(offPen, rect, startAngle, sweepAngle);

            // Draw bottom curve
            float bottomstartAngle = 0F;
            float bottomsweepAngle = 90.0F;
            Rectangle bottomrect = new Rectangle((int)Location.X - 37, (int)Location.Y + leadLength, 80, 38);
            gr.DrawArc(offPen, bottomrect, bottomstartAngle, bottomsweepAngle);

            // Draw top curve
            float topstartAngle = 270F;
            float topsweepAngle = 90.0F;
            Rectangle toprect = new Rectangle((int)Location.X - 37, (int)Location.Y + leadLength, 80, 38);
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
            gr.DrawLine(pen, new Point((int)Location.X, (int)Location.Y + 20), new Point((int)Location.X + 10, (int)Location.Y + 20));
            gr.DrawLine(pen, new Point((int)Location.X, (int)Location.Y + 40), new Point((int)Location.X + 10, (int)Location.Y + 40));

            // Draw output line
            gr.DrawLine(pen, new Point((int)Location.X + 45, (int)Location.Y + 30), new Point((int)Location.X + 60, (int)Location.Y + 30));
        }

        public void printGate()
        {
            Debug.WriteLine("Gate: " + this.ToString() +
                            " Pin1: " + this.Pin[0] +
                            " Pin2: " + this.Pin[1] +
                            " Pout: " + this.Pout[0]);

        }
    }
}

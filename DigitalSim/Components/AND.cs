using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace DigitalSim.Components
{
    class AND : Comp
    {
        public AND()
        {
            InitGate();
        }

        public AND(Pen pen)
        {
            offPen = pen;
            InitGate();
        }

        private void InitGate()
        {
            Pin = new bool[2];
            Pout = new bool[1];
            Pin[0] = false;
            Pin[1] = false;
            SetOutput();
        }

        public override void SetOutput()
        {
            Pout[0] = Pin[0] && Pin[1];  // Pout = Pin1 AND Pin2
            printGate();
        }

        // Let the AndGate draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            // Draw in input and output lines based on the logic state
            if (logicState)
                drawShape(gr, onPen);
            else
                drawShape(gr, offPen);

            // Draw the AND gate
            //gr.DrawRectangle(this.Pen, Pt.X + leadLength, Pt.Y + leadLength, gateSize-2*leadLength, gateSize - 2 * leadLength);
            gr.DrawLine(offPen, (int)Location.X + leadLength, (int)Location.Y + leadLength, (int)Location.X + 3 * leadLength, (int)Location.Y + leadLength);
            gr.DrawLine(offPen, (int)Location.X + leadLength, (int)Location.Y + gateSize - leadLength, (int)Location.X + 3 * leadLength, (int)Location.Y + gateSize - leadLength);
            gr.DrawLine(offPen, (int)Location.X + leadLength, (int)Location.Y + leadLength, (int)Location.X + leadLength, (int)Location.Y + gateSize - leadLength);

            float startAngle = -90F;
            float sweepAngle = 180.0F;
            Rectangle rect = new Rectangle((int)Location.X + leadLength, (int)Location.Y + leadLength, 40, 40);
            gr.DrawArc(offPen, rect, startAngle, sweepAngle);
        }

        private void drawShape(Graphics gr, Pen pen)
        {
            // Draw input lines
            gr.DrawLine(pen, new Point((int)Location.X, (int)Location.Y + 2 * leadLength), new Point((int)Location.X + leadLength, (int)Location.Y + 2 * leadLength));
            gr.DrawLine(pen, new Point((int)Location.X, (int)Location.Y + gateSize - 2 * leadLength), new Point((int)Location.X + leadLength, (int)Location.Y + gateSize - 2 * leadLength));

            // Draw output line
            gr.DrawLine(pen, new Point((int)Location.X + gateSize - leadLength, (int)Location.Y + gateSize / 2), new Point((int)Location.X + gateSize, (int)Location.Y + gateSize / 2));
        }

        public void printGate()
        {
            Debug.WriteLine("Gate: " + this.ToString() +
                            " Name: " + this.Name + 
                            " Pin1: " + this.Pin[0] +
                            " Pin2: " + this.Pin[1] +
                            " Pout: " + this.Pout[0]);

        }
    }
}

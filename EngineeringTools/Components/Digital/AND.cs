using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringTools.Components.Digital
{
    class AND : DigComp
    {
        public bool logicState = false;
        const int leadLength = 10;
        const int gateSize = 60;

        public AND()
        {
            setLogicState();
        }

        public override void setLogicState()
        {
            Pout = Pin[0] && Pin[1];  // Pout = Pin1 AND Pin2
            printGate();
        }

        // Let the AndGate draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Draw in input and output lines based on the logic state
            if (logicState)
                drawShape(gr, onPen);
            else
                drawShape(gr, offPen);

            // Draw the AND gate
            //gr.DrawRectangle(this.Pen, Pt.X + leadLength, Pt.Y + leadLength, gateSize-2*leadLength, gateSize - 2 * leadLength);
            gr.DrawLine(offPen, (int)loc.X + leadLength, (int)loc.Y + leadLength, (int)loc.X + 3 * leadLength, (int)loc.Y + leadLength);
            gr.DrawLine(offPen, (int)loc.X + leadLength, (int)loc.Y + gateSize - leadLength, (int)loc.X + 3 * leadLength, (int)loc.Y + gateSize - leadLength);
            gr.DrawLine(offPen, (int)loc.X + leadLength, (int)loc.Y + leadLength, (int)loc.X + leadLength, (int)loc.Y + gateSize - leadLength);

            float startAngle = -90F;
            float sweepAngle = 180.0F;
            Rectangle rect = new Rectangle((int)loc.X + leadLength, (int)loc.Y + leadLength, 40, 40);
            gr.DrawArc(offPen, rect, startAngle, sweepAngle);
        }

        private void drawShape(Graphics gr, Pen pen)
        {
            // Draw input lines
            gr.DrawLine(pen, new Point((int)loc.X, (int)loc.Y + 2 * leadLength), new Point((int)loc.X + leadLength, (int)loc.Y + 2 * leadLength));
            gr.DrawLine(pen, new Point((int)loc.X, (int)loc.Y + gateSize - 2 * leadLength), new Point((int)loc.X + leadLength, (int)loc.Y + gateSize - 2 * leadLength));

            // Draw output line
            gr.DrawLine(pen, new Point((int)loc.X + gateSize - leadLength, (int)loc.Y + gateSize / 2), new Point((int)loc.X + gateSize, (int)loc.Y + gateSize / 2));
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

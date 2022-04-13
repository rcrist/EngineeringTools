using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSim.Components
{
    class NOT : Comp
    {
        public NOT()
        {
            Pin = new bool[1];
            Pout = new bool[1];
            InitGate();
        }

        public NOT(Pen pen)
        {
            Pin = new bool[1];
            Pout = new bool[1];
            offPen = pen;
            InitGate();
        }

        private void InitGate()
        {
            Pin[0] = false;
            SetOutput();
        }

        public override void SetOutput()
        {
            Pout[0] = !Pin[0];            // Pout = NOT Pin
        }

        // Let the OrGate draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            // Draw the triangle
            gr.DrawLine(offPen, new Point((int)Location.X + 10, (int)Location.Y + 10), new Point((int)Location.X + 45, (int)Location.Y + 30));
            gr.DrawLine(offPen, new Point((int)Location.X + 10, (int)Location.Y + 50), new Point((int)Location.X + 45, (int)Location.Y + 30));
            gr.DrawLine(offPen, new Point((int)Location.X + 10, (int)Location.Y + 10), new Point((int)Location.X + 10, (int)Location.Y + 50));

            Rectangle rect = new Rectangle((int)Location.X + 45, (int)Location.Y + 27, 5, 5);
            gr.DrawEllipse(offPen, rect);

            // Draw in input and output lines based on the logic state
            if (logicState)
                drawShape(gr, onPen);
            else
                drawShape(gr, offPen);
        }

        public void drawShape(Graphics gr, Pen pen)
        {
            // Draw input lines
            gr.DrawLine(pen, new Point((int)Location.X, (int)Location.Y + 20), new Point((int)Location.X + 10, (int)Location.Y + 20));
            gr.DrawLine(pen, new Point((int)Location.X, (int)Location.Y + 40), new Point((int)Location.X + 10, (int)Location.Y + 40));

            // Draw output line
            gr.DrawLine(pen, new Point((int)Location.X + 50, (int)Location.Y + 30), new Point((int)Location.X + 60, (int)Location.Y + 30));
        }
    }
}

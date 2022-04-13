using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDigitalSimulator.Components;

namespace TestDigitalSimulator.Components
{
    public class LED : Comp
    {
        // LED specific variables
        SolidBrush onfillColor = new SolidBrush(Color.Yellow);
        SolidBrush offfillColor = new SolidBrush(Color.DarkGray);
        Color offColor = Color.Black;

        private const int endcap_radius = 3;
        public bool hotSpotsVisible = false;

        public LED()
        {

        }

        public override void setLogicState()
        {
            printGate();
        }

        // Let the LED draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Draw outer circle
            Rectangle rect = new Rectangle((int)loc.X + 10, (int)loc.Y + 10, 20, 20);
            gr.FillEllipse(new SolidBrush(offColor), rect);

            // Draw inner circle = LED based on the LED state
            if (Pin[0])
            {
                gr.DrawLine(this.onPen, new Point((int)loc.X, (int)loc.Y + 20), new Point((int)loc.X + 10, (int)loc.Y + 20));
                rect = new Rectangle((int)loc.X + 12, (int)loc.Y + 12, 16, 16);
                gr.FillEllipse(this.onfillColor, rect);
            }
            else
            {
                gr.DrawLine(this.offPen, new Point((int)loc.X, (int)loc.Y + 20), new Point((int)loc.X + 10, (int)loc.Y + 20));
                rect = new Rectangle((int)loc.X + 12, (int)loc.Y + 12, 16, 16);
                gr.FillEllipse(this.offfillColor, rect);
            }
            // Draw the output pin hot spot
            drawHotSpots(gr);
        }

        private void drawHotSpots(Graphics gr)
        {
            if (this.hotSpotsVisible)
            {
                // Draw hot spot for output pin
                Rectangle rect1 = new Rectangle(
                     (int)loc.X - endcap_radius, (int)loc.Y + 20 - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect1);     // Rectangular hot spot
            }
        }

        public void printGate()
        {
            Debug.WriteLine("Gate: " + this.ToString() +
                            " Pin1: " + this.Pin[0]);

        }
    }
}

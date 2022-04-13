using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSim.Components
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
            Pin = new bool[1];
            Pin[0] = false;
        }

        public LED(Pen pen, Color color)
        {
            Pin = new bool[1];
            Pin[0] = false;
            offPen = pen;
            offColor = color;
        }

        public override void SetOutput()
        {
            printGate();
        }

        // Let the LED draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            // Draw outer circle
            Rectangle rect = new Rectangle((int)Location.X + 10, (int)Location.Y + 10, 20, 20);
            gr.FillEllipse(new SolidBrush(offColor), rect);

            // Draw inner circle = LED based on the LED state
            if (Pin[0])
            {
                gr.DrawLine(this.onPen, new Point((int)Location.X, (int)Location.Y + 20), new Point((int)Location.X + 10, (int)Location.Y + 20));
                rect = new Rectangle((int)Location.X + 12, (int)Location.Y + 12, 16, 16);
                gr.FillEllipse(this.onfillColor, rect);
            }
            else
            {
                gr.DrawLine(this.offPen, new Point((int)Location.X, (int)Location.Y + 20), new Point((int)Location.X + 10, (int)Location.Y + 20));
                rect = new Rectangle((int)Location.X + 12, (int)Location.Y + 12, 16, 16);
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
                        (int)Location.X - endcap_radius, (int)Location.Y + 20 - endcap_radius,
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

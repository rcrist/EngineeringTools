using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSim.Components
{
    public class Switch : Comp
    {
        public bool switchState = false;
        private const int endcap_radius = 3;
        public bool hotSpotsVisible = false;

        public Switch()
        {
            Pout = new bool[1];
            Pout[0] = false;
        }

        public Switch(Pen pen)
        {
            Pout = new bool[1];
            Pout[0] = false;
            offPen = pen;
        }

        // Let the switch draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            // Draw outer Rectangle
            Rectangle rect = new Rectangle((int)Location.X + 20, (int)Location.Y + 10, 30, 20);
            gr.DrawRectangle(offPen, rect);

            // Draw switch based on switch on/off state
            if (switchState)
            {
                gr.DrawLine(onPen, new Point((int)Location.X + 50, (int)Location.Y + 20), new Point((int)Location.X + 60, (int)Location.Y + 20));
                rect = new Rectangle((int)Location.X + 25, (int)Location.Y + 15, 10, 10);
                gr.DrawRectangle(offPen, rect);
                rect = new Rectangle((int)Location.X + 35, (int)Location.Y + 15, 10, 10);
                gr.FillRectangle(new SolidBrush(Color.Black), rect);
            }
            else
            {
                gr.DrawLine(offPen, new Point((int)Location.X + 50, (int)Location.Y + 20), new Point((int)Location.X + 60, (int)Location.Y + 20));
                rect = new Rectangle((int)Location.X + 35, (int)Location.Y + 15, 10, 10);
                gr.DrawRectangle(offPen, rect);
                rect = new Rectangle((int)Location.X + 25, (int)Location.Y + 15, 10, 10);
                gr.FillRectangle(new SolidBrush(Color.Black), rect);
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
                     (int)Location.X + 60 - endcap_radius, (int)Location.Y + 20 - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect1);     // Rectangular hot spot
            }
        }

        public void ToggleSwitchState()
        {
            switchState = !switchState;
            Pout[0] = !Pout[0];
        }
    }
}

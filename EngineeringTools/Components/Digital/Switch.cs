using System;
using System.Collections.Generic;
using System.Drawing;

namespace EngineeringTools.Components.Digital
{
    public class Switch : DigComp
    {
        public bool switchState = false;
        private const int endcap_radius = 3;
        public bool hotSpotsVisible = false;

        public Switch()
        {
            Pout = false;
        }

        // Let the switch draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Draw outer Rectangle
            Rectangle rect = new Rectangle((int)loc.X + 20, (int)loc.Y + 10, 30, 20);
            gr.DrawRectangle(offPen, rect);

            // Draw switch based on switch on/off state
            if (switchState)
            {
                gr.DrawLine(onPen, new Point((int)loc.X + 50, (int)loc.Y + 20), new Point((int)loc.X + 60, (int)loc.Y + 20));
                rect = new Rectangle((int)loc.X + 25, (int)loc.Y + 15, 10, 10);
                gr.DrawRectangle(offPen, rect);
                rect = new Rectangle((int)loc.X + 35, (int)loc.Y + 15, 10, 10);
                gr.FillRectangle(new SolidBrush(Color.Black), rect);
            }
            else
            {
                gr.DrawLine(offPen, new Point((int)loc.X + 50, (int)loc.Y + 20), new Point((int)loc.X + 60, (int)loc.Y + 20));
                rect = new Rectangle((int)loc.X + 35, (int)loc.Y + 15, 10, 10);
                gr.DrawRectangle(offPen, rect);
                rect = new Rectangle((int)loc.X + 25, (int)loc.Y + 15, 10, 10);
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
                     (int)loc.X + 60 - endcap_radius, (int)loc.Y + 20 - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect1);     // Rectangular hot spot
            }
        }

        public void ToggleSwitchState()
        {
            switchState = !switchState;
            Pout = !Pout;
        }
    }
}

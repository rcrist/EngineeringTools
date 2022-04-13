using DigitalSimulator.Components;
using System.Diagnostics;
using System.Drawing;

namespace DigitalSimulator.Components
{
    class LED : DigitalComponent
    {
        // LED specific variables
        SolidBrush onfillColor = new SolidBrush(Color.Yellow);
        SolidBrush offfillColor = new SolidBrush(Color.DarkGray);

        // constructors
        public LED()
        {
            Debug.WriteLine("LED Created");
        }

        public LED(Point pt)
        {
            Debug.WriteLine("LED Created");
            Location = pt;
        }

        // Let the LED draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            // Draw outer circle
            Rectangle rect = new Rectangle((int)Location.X + 10, (int)Location.Y + 10, 20, 20);
            gr.FillEllipse(new SolidBrush(Color.Black), rect);

            // Draw inner circle = LED based on the LED state
            if (logicState)
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
        }
    }
}

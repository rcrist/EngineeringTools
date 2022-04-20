// C# Libraries
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MicrowaveTools.Components
{
    public class LED : Comp
    {
        // LED specific variables
        SolidBrush onfillColor = new SolidBrush(Color.Green);
        SolidBrush offfillColor = new SolidBrush(Color.Maroon);
        Color offColor = Color.Black;
        public Pen offPen = new Pen(Color.DimGray, 2);
        public Pen onPen = new Pen(Color.Green, 2);

        private const int endcap_radius = 3;
        public bool hotSpotsVisible = false;
        public bool logicState = false;

        // Component text variables
        // Create font and brush.
        public Font drawFont = new Font("Arial", 10);
        public SolidBrush drawBrush = new SolidBrush(Color.White);
        public StringFormat drawFormat = new StringFormat();

        public LED()
        {

        }

        public LED(Point location)
        {
            Loc = location;
            Pin = Loc;
        }

        // Let the LED draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Draw outer circle
            Rectangle rect = new Rectangle((int)Loc.X - 6, (int)Loc.Y - 6, 12, 12);
            gr.FillEllipse(new SolidBrush(Color.DarkGray), rect);

            // Draw inner circle = LED based on the LED state
            if (logicState)
            {
                //gr.DrawLine(this.onPen, new Point((int)Loc.X, (int)Loc.Y + 20), new Point((int)Loc.X + 10, (int)Loc.Y + 20));
                rect = new Rectangle((int)Loc.X - 4, (int)Loc.Y - 4, 8, 8);
                gr.FillEllipse(this.onfillColor, rect);
            }
            else
            {
                //gr.DrawLine(this.offPen, new Point((int)Loc.X, (int)Loc.Y + 20), new Point((int)Loc.X + 10, (int)Loc.Y + 20));
                rect = new Rectangle((int)Loc.X - 4, (int)Loc.Y - 4, 8, 8);
                gr.FillEllipse(this.offfillColor, rect);
            }

            // Add label
            drawCompText(gr, new Point(Loc.X - 75, Loc.Y - 8), "Wire Mode");
        }

        public virtual void drawCompText(Graphics gr, Point p1, String drawString)
        {
            // Convert Point ints to floats
            float x = p1.X;
            float y = p1.Y;

            // Draw string to screen.
            gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }
    }
}
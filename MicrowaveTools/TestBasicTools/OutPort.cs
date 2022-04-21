using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBasicTools
{
    public class OutPort : Comp
    {
        Pen drawPen = new Pen(Color.White);

        public OutPort(float value, Point location, int[] nodes)
        {
            Type = "Pin";
            Value = value;
            Location = location;
            Nodes = nodes;
            print();
        }

        // Let the InPort draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Draw the input lead
            gr.DrawLine(drawPen, Location.X, Location.Y + halfCompSize, Location.X + leadLength, Location.Y + halfCompSize);

            // Draw the InPort body
            gr.DrawLine(drawPen, Location.X + leadLength, Location.Y + halfCompSize, Location.X + leadLength * 2, Location.Y + halfCompSize - leadLength);
            gr.DrawLine(drawPen, Location.X + leadLength, Location.Y + halfCompSize, Location.X + leadLength * 2, Location.Y + halfCompSize + leadLength);
            gr.DrawLine(drawPen, Location.X + compSize - leadLength * 2, Location.Y + halfCompSize - leadLength, Location.X + leadLength * 2, Location.Y + halfCompSize - leadLength);
            gr.DrawLine(drawPen, Location.X + compSize - leadLength * 2, Location.Y + halfCompSize - leadLength, Location.X + compSize - leadLength * 2, Location.Y + halfCompSize + leadLength);
            gr.DrawLine(drawPen, Location.X + compSize - leadLength * 2, Location.Y + halfCompSize + leadLength, Location.X + leadLength * 2, Location.Y + halfCompSize + leadLength);

            // Create string to draw.
            String drawString = "Pout";

            // Create font and brush.
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            // Create point for upper-left corner of drawing.
            float x = Location.X + compSize - leadLength;
            float y = Location.Y + halfCompSize - 7;

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            //drawFormat.FormatFlags = StringFormatFlags.DirectionHorizontal;

            // Draw string to screen.
            gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " Z: " + this.Value + "ohms " + "[" + this.Nodes[0] + "]");
        }
    }
}

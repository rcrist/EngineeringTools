using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace TestBasicTools
{
    public class Capacitor : Comp
    {
        Pen drawPen = new Pen(Color.White);

        public Capacitor()
        {

        }

        public Capacitor(float value, Point location, int[] nodes)
        {
            Type = "Cap";
            Value = value;
            Location = location;
            Nodes = nodes;
            print();
        }

        // Analysis initializer
        public override void initComp(float f)
        {
            Matrix<Complex32> Ycap = Matrix<Complex32>.Build.Dense(2, 2);
            Ycap[0, 0] = 1;
            Ycap[0, 1] = -1;
            Ycap[1, 0] = -1;
            Ycap[1, 1] = 1;

            Complex32 denom = new Complex32(0, (float)(-1 / (2 * Constants.Pi * f * this.Value)));
            Ycap = Ycap / denom; // Won't work with a double, must be a float
            Y = Ycap;
            N = this.Nodes;
        }

        // Let the Capacitor draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Draw the input and output leads
            gr.DrawLine(drawPen, Location.X, Location.Y + halfCompSize, Location.X + leadLength, Location.Y + halfCompSize);
            gr.DrawLine(drawPen, Location.X + compSize - leadLength, Location.Y + halfCompSize, Location.X + compSize, Location.Y + halfCompSize);

            // Draw the capacitor body curves
            float startAngle = 90;
            float sweepAngle = 180;
            Rectangle rect = new Rectangle(Location.X + leadLength * 3, Location.Y + leadLength * 2, leadLength, leadLength * 2);
            gr.DrawArc(drawPen, rect, startAngle, sweepAngle);

            // Draw the capacitor body vertical line
            gr.DrawLine(drawPen, Location.X + 25, Location.Y + halfCompSize - leadLength, Location.X + 25, Location.Y + halfCompSize + leadLength);

            // Draw the capacitor lead extenders
            gr.DrawLine(drawPen, Location.X + leadLength, Location.Y + halfCompSize, Location.X + 25, Location.Y + halfCompSize);
            gr.DrawLine(drawPen, Location.X + 30, Location.Y + halfCompSize, Location.X + compSize, Location.Y + halfCompSize);

            // Create string to draw.
            String drawString = "C = " + this.Value + "pF";

            // Create font and brush.
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            // Create point for upper-left corner of drawing.
            float x = Location.X + 5;
            float y = Location.Y - 5;

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            //drawFormat.FormatFlags = StringFormatFlags.DirectionHorizontal;

            // Draw string to screen.
            gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " C: " + this.Value + "\t[" + 
                                       this.Nodes[0] + "," + this.Nodes[1] + "]");
        }
    }
}
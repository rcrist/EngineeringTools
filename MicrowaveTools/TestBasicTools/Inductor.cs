using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace TestBasicTools
{
    class Inductor : Comp
    {
        Pen drawPen = new Pen(Color.White);

        public Inductor()
        {

        }

        public Inductor(float value, Point location, int[] nodes)
        {
            Type = "Ind";
            Value = value;
            Location = location;
            Nodes = nodes;
            print();
        }

        // Analysis initializer
        public override void initComp(float f)
        {
            Matrix<Complex32> Yind = Matrix<Complex32>.Build.Dense(2, 2);
            Yind[0, 0] = 1;
            Yind[0, 1] = -1;
            Yind[1, 0] = -1;
            Yind[1, 1] = 1;

            Complex32 denom = new Complex32(0, (float)(2 * Constants.Pi * f * this.Value));
            if (denom != 0)
                Yind = Yind / denom; // Won't work with a double, must be a float
            else
                Debug.WriteLine("ERROR: Divide by 0 in initComp(): " + "f: " + f + " Value: " + this.Value);
            Y = Yind;
            N = this.Nodes;
        }


        // Let the Inductor draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
                // Draw the input and output leads
                gr.DrawLine(drawPen, Location.X, Location.Y + halfCompSize, Location.X + leadLength, Location.Y + halfCompSize);
                gr.DrawLine(drawPen, Location.X + compSize - leadLength, Location.Y + halfCompSize, Location.X + compSize, Location.Y + halfCompSize);

                // Draw the inductor body curves
                for (int i = 1; i < 5; i++)
                {

                    float startAngle = 180;
                    float sweepAngle = 180;
                    Rectangle rect = new Rectangle(Location.X + leadLength * i, Location.Y + leadLength + leadLength / 2, leadLength, leadLength);
                    gr.DrawArc(drawPen, rect, startAngle, sweepAngle);
                }

                // Draw the inductor body vertical lines
                for (int i = 1; i < 6; i++)
                {
                    gr.DrawLine(drawPen, Location.X + leadLength * (i), Location.Y + halfCompSize, Location.X + leadLength * (i), Location.Y + halfCompSize - leadLength);
                }

                // Create string to draw.
                String drawString = "L = " + this.Value + "nH";

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
            Debug.WriteLine("Type: " + this.Type + " L: " + this.Value + "\t[" + this.Nodes[0] + "," + this.Nodes[1] + "]");
        }
    }
}

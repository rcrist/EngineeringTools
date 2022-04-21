using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;


namespace TestBasicTools
{
    class Resistor : Comp
    {
        Pen drawPen = new Pen(Color.White);

        public Resistor()
        {

        }

        public Resistor(float value, Point location, int[] nodes)
        {
            Type = "Res";
            Value = value;
            Location = location;
            Nodes = nodes;
            print();
        }

        // Analysis initializer
        public override void initComp(float f)
        {
            Matrix<Complex32> Yres = Matrix<Complex32>.Build.Dense(2, 2);
            Yres[0, 0] = 1;
            Yres[0, 1] = -1;
            Yres[1, 0] = -1;
            Yres[1, 1] = 1;

            Yres = Yres / this.Value; // Won't work with a double, must be a float
            Y = Yres;
            N = this.Nodes;
        }

        // Let the Resistor draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Draw the input and output leads
            gr.DrawLine(drawPen, Location.X, Location.Y + halfCompSize, Location.X + leadLength, Location.Y + halfCompSize);
            gr.DrawLine(drawPen, Location.X + compSize - leadLength, Location.Y + halfCompSize, Location.X + compSize, Location.Y + halfCompSize);

            // Draw the resitor body
            for (int i = 1; i < 5; i++)
            {
                gr.DrawLine(drawPen, Location.X + leadLength * (i), Location.Y + halfCompSize, Location.X + leadLength * (i) + 3, Location.Y + halfCompSize - leadLength);
                gr.DrawLine(drawPen, Location.X + leadLength * (i) + 3, Location.Y + halfCompSize - leadLength, Location.X + leadLength * (i) + 6, Location.Y + halfCompSize + leadLength);
                gr.DrawLine(drawPen, Location.X + leadLength * (i) + 6, Location.Y + halfCompSize + leadLength, Location.X + leadLength * (i + 1), Location.Y + halfCompSize);
            }

            // Create string to draw.
            String drawString = "R = " + this.Value + "Ω";

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
            Debug.WriteLine("Type: " + this.Type + " R: " + this.Value + "\t[" + this.Nodes[0] + ", " + this.Nodes[1] + "]");
        }
    }
}
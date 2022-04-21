// Ideal Lossless Transmission Line
// References:
// (1) "QUCS Technical Papers", S. Jahn, et al, 2007, pg. 99-100.
// (2) "Transmission Line Design Handbook", B. Wadell, Artech House, 1991, pg. 28-42.
// (3) "Computer-Aided Design of Microwave Circuits", K.C. Gupta, et al, Artech House, 1981, pg. 39.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Ideal
{
    class TLIN : Comp
    {
        private double l, z;
        private const double C0 = 299792458;   // Speed of light (m/s)

        public TLIN(double Z, double L, Point location, int[] nodes)
        {
            z = Z;
            l = L;
            Type = "TLIN";
            Value = (float)Z;
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
        }

        // Analysis initializer
        public override void initComp(float f)
        {
            double z0 = 50.0;

            Matrix<Complex32> Yres = Matrix<Complex32>.Build.Dense(2, 2);
            Yres[0, 0] = 1;
            Yres[0, 1] = -1;
            Yres[1, 0] = -1;
            Yres[1, 1] = 1;

            Yres = Yres / this.Value; // Won't work with a double, must be a float
            Y = Yres;
            N = this.Nodes;

            double r = (z - z0) / (z + z0);
            double b = 2 * Math.PI * f / C0;
            double p = Math.Exp(-l * b); // for lossless transmission line (a=0)
            double s11 = r * (1.0 - p * p) / (1.0 - p * p * r * r);
            double s21 = p * (1.0 - r * r) / (1.0 - p * p * r * r);
        }

        // Let the TLIN draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + leadL, p1.Y);
                Point p3 = new Point(p2.X + compL, p2.Y);
                Point p4 = new Point(p3.X + leadL, p3.Y);
                Point p5 = new Point(p2.X, p2.Y - leadL);
                Point p6 = new Point(p1.X, p1.Y + 20);
                Point p7 = new Point(p4.X, p4.Y + 20);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p3, p4);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawRectangle(drawPen, p5.X, p5.Y, compL, 20);

                // Draw the grounds
                drawSeriesGnd(gr, p6);
                drawSeriesGnd(gr, p7);

                // Create string to draw.
                String drawString = "TLIN";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 25;
                float y = p1.Y - 30;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if (Orientation == "Shunt")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X, p1.Y + leadL);
                Point p3 = new Point(p2.X, p2.Y + compL);
                Point p4 = new Point(p3.X, p3.Y + leadL);
                Point p5 = new Point(p2.X - 10, p2.Y);
                Point p6 = new Point(p1.X + 10, p1.Y);
                Point p7 = new Point(p4.X + 10, p4.Y);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p3, p4);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawRectangle(drawPen, p5.X, p5.Y, 20, 60);

                // Draw the grounds
                drawShuntGnd(gr, p6);
                drawShuntGnd(gr, p7);

                // Create string to draw.
                String drawString = "TLIN";

                // Define the label location
                float x = p1.X - 45;
                float y = p1.Y + 35;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " Z: " + this.z + "ohms " + "[" + this.Nodes[0] + "]");
        }
    }
}
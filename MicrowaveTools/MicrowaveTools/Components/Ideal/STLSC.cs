using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Ideal
{
    class STLSC : Comp
    {
        private double l, z;
        private const double C0 = 299792458;   // Speed of light (m/s)

        public STLSC(double Z, double L, Point location, int[] nodes)
        {
            z = Z;
            l = L;
            Type = "STLSC";
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

        // Let the STLOC draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;             // Assume p1 is the at the Location point
                Point p2 = new Point(p1.X + 20, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 10);
                Point p4 = new Point(p3.X - 5, p3.Y - 30);
                Point p5 = new Point(p2.X + 20, p2.Y - 50);
                Point p8 = new Point(p2.X + 20, p2.Y);
                Point p9 = new Point(p8.X + 20, p8.Y);
                Point p6 = new Point(p1.X, p1.Y + 20);
                Point p7 = new Point(p9.X, p9.Y + 20);
                Point p10 = new Point(p2.X, p2.Y - 40);
                Point p11 = new Point(p10.X, p10.Y - 10);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p2, p3);
                gr.DrawLine(drawPen, p10, p11);
                gr.DrawLine(drawPen, p11, p5);
                gr.DrawLine(drawPen, p5, p8);
                gr.DrawLine(drawPen, p8, p9);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawRectangle(drawPen, p4.X, p4.Y, 10, 30);

                // Draw the grounds
                drawSeriesGnd(gr, p6);
                drawSeriesGnd(gr, p7);

                // Create string to draw.
                String drawString = "STLSC";

                // Create point for upper-left corner of drawing.
                float x = p1.X - 35;
                float y = p1.Y - 30;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if (Orientation == "Shunt")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + 30, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 5);
                Point p4 = new Point(p2.X + 30, p2.Y);
                Point p5 = new Point(p4.X + 10, p4.Y);
                Point p6 = new Point(p1.X, p1.Y + 20);
                Point p7 = new Point(p6.X + 70, p6.Y);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawLine(drawPen, p5, p7);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 30, 10);

                // Draw the grounds
                drawSeriesGnd(gr, p6);

                // Create string to draw.
                String drawString = "STLSC";

                // Define the label location
                float x = p1.X + 10;
                float y = p1.Y - 25;

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


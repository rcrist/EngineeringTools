using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;


namespace MicrowaveTools.Components.Microstrip
{
    class MCORNERR : Comp
    {
        double w, h, er, C, L;

        public MCORNERR(float value, Point location, int[] nodes)
        {
            Substrate subst = new Substrate();
            w = value;
            h = subst.h;
            er = subst.er;

            Type = "MCORNERR";
            Value = value;
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
            //Matrix<Complex32> Yres = Matrix<Complex32>.Build.Dense(2, 2);
            //Yres[0, 0] = 1;
            //Yres[0, 1] = -1;
            //Yres[1, 0] = -1;
            //Yres[1, 1] = 1;

            //Yres = Yres / this.Value; // Won't work with a double, must be a float
            //Y = Yres;
            //N = this.Nodes;

            double Wh = w / h;

            if (Wh < 0.2 || Wh > 6.0)
                Debug.WriteLine("LOG_ERROR: " + "WARNING: Model for microstrip corner defined for "
                    + "0.2 <= W/h <= 6.0 (W/h = " + Wh);

            if (er < 2.36 || er > 10.4)
                Debug.WriteLine("LOG_ERROR: " + "WARNING: Model for microstrip corner defined for "
                    + "2.36 <= er <= 10.4 (er = " + er);

            // capacitance in pF
            C = w * ((10.35 * er + 2.5) * Wh + (2.6 * er + 5.64));
            // inductivity in nH
            L = 220.0 * h * (1.0 - 1.35 * Math.Exp(-0.18 * Math.Pow(Wh, 1.39)));
        }

        Matrix<Complex32> calcMatrixZ(double frequency)
        {
            // check frequency validity
            if (frequency * h > 12e6)
            {
                Debug.WriteLine("LOG_ERROR" + "WARNING: Model for microstrip corner defined for "
                        + "freq*h <= 12MHz (is " + frequency * h);
            }

            // create Z-parameter matrix
            Matrix<Complex32> z = Matrix<Complex32>.Build.Dense(2, 2);
            Complex32 z21 = new Complex32(0, (float)(-1 / (2 * Constants.Pi * frequency * C)));
            Complex32 z11 = new Complex32(0, (float)(2 * Constants.Pi * frequency * L)) + z21;
            z[0, 0] = z11;
            z[0, 1] = z21;
            z[1, 0] = z21;
            z[1, 1] = z11;
            return z;
        }

        // Let the MCORNERL draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + 10, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 10); 
                Point p4 = new Point(p3.X + 40, p3.Y);
                Point p5 = new Point(p4.X, p4.Y + 40);
                Point p6 = new Point(p5.X - 20, p5.Y);
                Point p7 = new Point(p6.X, p6.Y - 20);
                Point p8 = new Point(p7.X - 20, p7.Y);
                Point p9 = new Point(p5.X - 10, p5.Y);
                Point p10 = new Point(p9.X, p9.Y + 10);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p3, p8);
                gr.DrawLine(drawPen, p3, p4);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawLine(drawPen, p5, p6);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawLine(drawPen, p7, p8);
                gr.DrawLine(drawPen, p9, p10);

                // Create string to draw.
                String drawString = "MCORN";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 5;
                float y = p1.Y - 30;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if(Orientation == "Shunt")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + 10, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 10);
                Point p4 = new Point(p3.X + 20, p3.Y);
                Point p5 = new Point(p4.X, p4.Y - 20);
                Point p6 = new Point(p5.X + 20, p5.Y);
                Point p7 = new Point(p6.X, p6.Y + 40);
                Point p8 = new Point(p7.X - 40, p7.Y);
                Point p9 = new Point(p5.X + 10, p5.Y);
                Point p10 = new Point(p9.X, p9.Y - 10);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p3, p8);
                gr.DrawLine(drawPen, p3, p4);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawLine(drawPen, p5, p6);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawLine(drawPen, p7, p8);
                gr.DrawLine(drawPen, p9, p10);

                // Create string to draw.
                String drawString = "MCORN";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 55;
                float y = p1.Y - 20;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }
    }
}

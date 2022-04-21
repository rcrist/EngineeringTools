// C# class libraries
using System;
using System.Diagnostics;
using System.Drawing;

// MathNet.Numerics math libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;
using MicrowaveTools.Components.Microstrip;

namespace MicrowaveTools.Components.CPW
{
    class CPWGAP : Comp
    {
        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);
        public double W;
        public double s;
        public double g;
        public double len;
        public int backMetal = 1;
        public int approx = 0;

        public double er;
        public double h;
        public double t;
        public double tand;
        public double rho;
        public double D;

        double Z0 = 50;
        double z0 = 50;
        double C0 = 3e8; // Speed of light m/s
        double MU0 = 4e-7 * Math.PI;
        double E0 = 8.854187817e-12;

        public CPWGAP()
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;
        }

        public CPWGAP(double w, double S, double l, double G, int BackMetal, int Approx, Point location, int[] nodes)
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;

            Type = "CPWGAP";
            W = w;
            s = S;
            len = l;
            g = G;
            backMetal = BackMetal;
            approx = Approx;
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
        }

        void calcSP(double frequency)
        {
            S = ytos(calcMatrixY(frequency));
        }

        Matrix<Complex32> calcMatrixY(double frequency)
        {
            // calculate series capacitance
            er = (er + 1) / 2;
            double p = g / 4 / W;
            double C = 2 * E0 * er * W / Math.PI *
              (p - Math.Sqrt(1 + p * p) + Math.Log((1 + Math.Sqrt(1 + p * p)) / p));

            // build Y-parameter matrix
            Complex32 y11 = new Complex32(0.0f, (float)(2.0 * Math.PI * frequency * C));
            Matrix<Complex32> y = Matrix<Complex32>.Build.Dense(2,2);
            y[0, 0] = +y11;
            y[0, 1] = -y11;
            y[1, 0] = -y11;
            y[1, 1] = +y11;
            return y;
        }

        public Matrix<Complex32> ytos(Matrix<Complex32> Y)
        {
            var M = Matrix<Complex32>.Build;
            var V = Vector<Complex32>.Build;
            var S = Matrix<Complex32>.Build.Dense(2, 2);

            Matrix<Complex32> I = Matrix<Complex32>.Build.Dense(2, 2);
            I = DiagonalMatrix.CreateIdentity(2);
            Matrix<Complex32> Yo = Matrix<Complex32>.Build.Dense(2, 2);
            float Zo = 50.0f;
            Yo = I * 1.0f / Zo;

            Matrix<Complex32> Stemp1 = M.Dense(2, 2);
            Stemp1 = M.DenseOfMatrix(Yo + Y);
            Stemp1 = Stemp1.Inverse();

            Matrix<Complex32> Stemp2 = M.Dense(2, 2);
            Stemp2 = M.DenseOfMatrix((Yo - Y));

            S = M.DenseOfMatrix(Stemp1 * Stemp2);
            return S;
        }

        // Let the MGAP draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + 10, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 10); // Location of rectangle #1
                Point p4 = new Point(p2.X + 40, p2.Y);
                Point p5 = new Point(p4.X + 10, p4.Y);
                Point p6 = new Point(p2.X + 25, p2.Y - 10); // Location of rectangle #2

                Point p13 = new Point(p3.X, p3.Y - 10);
                Point p14 = new Point(p13.X + 40, p13.Y);
                Point p15 = new Point(p5.X-10, p5.Y + 20);
                Point p16 = new Point(p15.X - 40, p15.Y);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 15, 20);
                gr.DrawRectangle(drawPen, p6.X, p6.Y, 15, 20);

                gr.DrawLine(drawPen, p13, p14);
                gr.DrawLine(drawPen, p15, p16);

                // Create string to draw.
                String drawString = "CPWGAP";

                // Create point for upper-left corner of drawing.
                float x = p1.X;
                float y = p1.Y - 40;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if (Orientation == "Shunt")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X, p1.Y + 10);
                Point p3 = new Point(p2.X - 10, p2.Y); // Location of rectangle #1
                Point p4 = new Point(p2.X, p2.Y + 40);
                Point p5 = new Point(p4.X, p4.Y + 10);
                Point p6 = new Point(p2.X - 10, p2.Y + 25); // Location of rectangle #2

                Point p13 = new Point(p3.X - 10, p3.Y);
                Point p14 = new Point(p13.X, p13.Y + 40);
                Point p15 = new Point(p5.X + 20, p5.Y-10);
                Point p16 = new Point(p15.X, p15.Y - 40);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 20, 15);
                gr.DrawRectangle(drawPen, p6.X, p6.Y, 20, 15);

                gr.DrawLine(drawPen, p13, p14);
                gr.DrawLine(drawPen, p15, p16);

                // Create string to draw.
                String drawString = "CPWGAP";

                // Create point for upper-left corner of drawing.
                float x = p1.X - 85;
                float y = p1.Y + 25;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }

    }
}

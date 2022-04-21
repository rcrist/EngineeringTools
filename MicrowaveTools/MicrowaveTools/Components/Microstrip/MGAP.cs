// C# class libraries
using System;
using System.Diagnostics;
using System.Drawing;

// MathNet.Numerics math libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;
using MicrowaveTools.Circuits;

namespace MicrowaveTools.Components.Microstrip
{
    class MGAP : Comp
    {
        Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);
        double W1, W2, s;
        double h, er, t;

        public MGAP(float w1, float w2, float S, Point location, int[] nodes)
        {
            Substrate subst = new Substrate();
            W1 = w1;
            W2 = w2;
            s = S;
            h = subst.h;
            er = subst.er;
            t = subst.t;

            Type = "MGAP";
            Orientation = "Series";
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
            double Q1, Q2, Q3, Q4, Q5;
            bool flip = false;
            if (W2 < W1)
            {  // equations are valid for 1 <= W2/W1 <= 3
                Q1 = W1;
                W1 = W2;
                W2 = Q1;
                flip = true;
            }

            // calculate parallel open end capacitances
            MOPEN mopen = new MOPEN();
            double C1 = mopen.calcCend(frequency, W1, h, t, er);
            double C2 = mopen.calcCend(frequency, W2, h, t, er);

            W2 /= W1;
            W1 /= h;
            s /= h;

            // local variables
            Q5 = 1.23 / (1.0 + 0.12 * Math.Pow(W2 - 1.0, 0.9));
            Q1 = 0.04598 * (0.03 + Math.Pow(W1, Q5)) * (0.272 + 0.07 * er);
            Q2 = 0.107 * (W1 + 9.0) * Math.Pow(s, 3.23) +
              2.09 * Math.Pow(s, 1.05) * (1.5 + 0.3 * W1) / (1.0 + 0.6 * W1);
            Q3 = Math.Exp(-0.5978 * Math.Pow(W2, +1.35)) - 0.55;
            Q4 = Math.Exp(-0.5978 * Math.Pow(W2, -1.35)) - 0.55;

            double Cs = 5e-10 * h * Math.Exp(-1.86 * s) * Q1 *
              (1.0 + 4.19 * (1.0 - Math.Exp(-0.785 * Math.Sqrt(1.0 / W1) * W2)));
            C1 *= (Q2 + Q3) / (Q2 + 1.0);
            C2 *= (Q2 + Q4) / (Q2 + 1.0);

            if (flip)
            { // if necessary flip ports back
                Q1 = C1;
                C1 = C2;
                C2 = Q1;
            }

            // build Y-parameter matrix
            Complex32 y21 = new Complex32(0.0f, (float)(-2.0 * Math.PI * frequency * Cs));
            Complex32 y11 = new Complex32(0.0f, (float)(2.0 * Math.PI * frequency * (C1 + Cs)));
            Complex32 y22 = new Complex32(0.0f, (float)(2.0 * Math.PI * frequency * (C2 + Cs)));
            Matrix<Complex32> y = Matrix<Complex32>.Build.Dense(2,2);
            y[0, 0] = y11;
            y[0, 1] = y21;
            y[1, 0] = y21;
            y[1, 1] = y22;
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

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 15, 20);
                gr.DrawRectangle(drawPen, p6.X, p6.Y, 15, 20);

                // Create string to draw.
                String drawString = "MGAP";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 8;
                float y = p1.Y - 30;

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

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 20, 15);
                gr.DrawRectangle(drawPen, p6.X, p6.Y, 20, 15);

                // Create string to draw.
                String drawString = "MGAP";

                // Create point for upper-left corner of drawing.
                float x = p1.X - 55;
                float y = p1.Y + 25;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }
    }
}

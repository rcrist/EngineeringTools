// C# class libraries
using System;
using System.Diagnostics;
using System.Drawing;

// MathNet.Numerics math libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Microstrip
{
    class MSTEP : Comp
    {
        public double W1;
        public double W2;
        public double L = 10e-3f;    // Length
        public Substrate subst;     // Substrate
        public double Temp = 26.85f; // Temperature
        public double z0 = 50;       // Reference impedance
        public double Z0 = 50;
        public double U0 = 4e-7 * Math.PI;
        public double C0 = 3e8;   // Speed of light

        public double er;
        public double h;
        public double t;
        public double tand;
        public double rho;
        public double D;
        public double ac, ad;
        public double ZlEff, ErEff, WEff, ZlEffFreq, ErEffFreq;
        public double alpha, beta, zl, ereff;
        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);

        public MSTEP()
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;
        }

        public MSTEP(float w1, float w2, Point location, int[] nodes)
        {
            W1 = w1;
            W2 = w2;
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;

            Type = "MSTEP";
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
        }

        void calcSP(double frequency)
        {
            S = ztos(calcMatrixZ(frequency));
        }

        Matrix<Complex32> calcMatrixZ(double frequency)
        {
            // compute parallel capacitance
            double t1 = Math.Log10(er);
            double t2 = W1 / W2;
            double Cs = Math.Sqrt(W1 * W2) * (t2 * (10.1 * t1 + 2.33) - 12.6 * t1 - 3.17);

            // compute series inductance
            t1 = Math.Log10(t2);
            t2 = t2 - 1;
            double Ls = h * (t2 * (40.5 + 0.2 * t2) - 75 * t1);

            double ZlEff = 0, ErEff = 0, WEff = 0, ZlEffFreq = 0, ErEffFreq = 0;
            MLIN mlin = new MLIN();
            mlin.calcQuasiStatic(W1, h, t, er, ref ZlEff, ref ErEff, ref WEff);
            mlin.calcDispersion(W1, h, er, ZlEff, ErEff, frequency, ref ZlEffFreq, ref ErEffFreq);
            double L1 = ZlEffFreq * Math.Sqrt(ErEffFreq) / C0;

            mlin.calcQuasiStatic(W2, h, t, er, ref ZlEff, ref ErEff, ref WEff);
            mlin.calcDispersion(W2, h, er, ZlEff, ErEff, frequency, ref ZlEffFreq, ref ErEffFreq);
            double L2 = ZlEffFreq * Math.Sqrt(ErEffFreq) / C0;

            Ls /= (L1 + L2);
            L1 *= Ls;
            L2 *= Ls;

            // build Z-parameter matrix
            Complex32 z21 = new Complex32(0.0f, (float)(-0.5e12 / (Math.PI * frequency * Cs)));
            Complex32 z11 = new Complex32(0.0f, (float)(2e-9 * Math.PI * frequency * L1)) + z21;
            Complex32 z22 = new Complex32(0.0f, (float)(2e-9 * Math.PI * frequency * L2)) + z21;
            Matrix<Complex32> z = Matrix<Complex32>.Build.Dense(2, 2);
            z[0, 0] = z11;
            z[0, 1] = z21;
            z[1, 0] = z21;
            z[1, 1] = z22;
            return z;
        }

        private Matrix<Complex32> ztos(Matrix<Complex32> Z)
        {
            Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);
            float Z0 = 50;

            Complex32 denom = new Complex32();
            denom = (Z[0, 0] + Z0) * (Z[1, 1] - Z0) - Z[0, 1] * Z[1, 0];

            S[0, 0] = ((Z[1, 1] + Z0) * (Z[0, 0] - Z0) - Z[0, 1] * Z[1, 0]) / denom;
            S[0, 1] = (float)(2 * Math.Sqrt(Z0) * Math.Sqrt(Z0)) * Z[1, 0] / denom;
            S[1, 0] = (float)(2 * Math.Sqrt(Z0) * Math.Sqrt(Z0)) * Z[0, 1] / denom;
            S[1, 1] = ((Z[0, 0] + Z0) * (Z[1, 1] - Z0) - Z[0, 1] * Z[1, 0]) / denom;

            return S;
        }

        // Let the MSTEP draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;
                Point p2 = new Point(p1.X + 10, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 10);
                Point p4 = new Point(p3.X + 20, p3.Y);
                Point p5 = new Point(p4.X, p4.Y - 10);
                Point p6 = new Point(p5.X + 20, p5.Y);
                Point p7 = new Point(p6.X, p6.Y + 40);
                Point p8 = new Point(p7.X - 20, p7.Y);
                Point p9 = new Point(p8.X, p8.Y - 10);
                Point p10 = new Point(p9.X - 20, p9.Y);
                Point p11 = new Point(p6.X, p6.Y + 20);
                Point p12 = new Point(p11.X + 10, p11.Y);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p2, p3);
                gr.DrawLine(drawPen, p3, p4);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawLine(drawPen, p5, p6);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawLine(drawPen, p7, p8);
                gr.DrawLine(drawPen, p8, p9);
                gr.DrawLine(drawPen, p10, p2);
                gr.DrawLine(drawPen, p9, p10);
                gr.DrawLine(drawPen, p11, p12);

                // Create string to draw.
                String drawString = "MSTEP";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 13;
                float y = p1.Y - 40;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if (Orientation == "Shunt")
            {
                {
                    Point p1 = Loc;
                    Point p2 = new Point(p1.X, p1.Y + 10);
                    Point p3 = new Point(p2.X - 10, p2.Y);
                    Point p4 = new Point(p3.X, p3.Y + 20);
                    Point p5 = new Point(p4.X - 10, p4.Y);
                    Point p6 = new Point(p5.X, p5.Y + 20);
                    Point p7 = new Point(p6.X + 40, p6.Y);
                    Point p8 = new Point(p7.X, p7.Y - 20);
                    Point p9 = new Point(p8.X - 10, p8.Y);
                    Point p10 = new Point(p9.X, p9.Y - 20);
                    Point p11 = new Point(p6.X + 20, p6.Y);
                    Point p12 = new Point(p11.X, p11.Y + 10);

                    gr.DrawLine(drawPen, p1, p2);
                    gr.DrawLine(drawPen, p2, p3);
                    gr.DrawLine(drawPen, p3, p4);
                    gr.DrawLine(drawPen, p4, p5);
                    gr.DrawLine(drawPen, p5, p6);
                    gr.DrawLine(drawPen, p6, p7);
                    gr.DrawLine(drawPen, p7, p8);
                    gr.DrawLine(drawPen, p8, p9);
                    gr.DrawLine(drawPen, p10, p2);
                    gr.DrawLine(drawPen, p9, p10);
                    gr.DrawLine(drawPen, p11, p12);

                    // Create string to draw.
                    String drawString = "MSTEP";

                    // Create point for upper-left corner of drawing.
                    float x = p1.X - 70;
                    float y = p1.Y + 25;

                    // Draw string to screen.
                    gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                }
            }

        }
    }
}

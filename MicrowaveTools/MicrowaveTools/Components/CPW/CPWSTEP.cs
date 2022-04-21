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
    class CPWSTEP : Comp
    {
        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);
        public double W1;
        public double W2;
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

        public CPWSTEP()
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;
        }

        public CPWSTEP(double w1, double w2, double S, double l, double G, int BackMetal, int Approx, Point location, int[] nodes)
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;

            Type = "CPWSHORT";
            W1 = w1;
            W2 = w2;
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

        // Returns the coplanar step capacitances per unit length.
        void calcCends(double frequency,
                     ref double C1, ref double C2)
        {
            double s1 = (s - W1) / 2;
            double s2 = (s - W2) / 2;

            double ZlEff = 0, ErEff = 0, ZlEffFreq = 0, ErEffFreq = 0;
            CPWLIN clin = new CPWLIN();
            clin.analyseQuasiStatic(W1, s1, h, t, er, backMetal, ref ZlEff, ref ErEff);
            clin.analyseDispersion(W1, s1, h, er, ZlEff, ErEff, frequency,
                             ref ZlEffFreq, ref ErEffFreq);
            C1 = ErEffFreq / C0 / ZlEffFreq;
            clin.analyseQuasiStatic(W2, s2, h, t, er, backMetal, ref ZlEff, ref ErEff);
            clin.analyseDispersion(W2, s2, h, er, ZlEff, ErEff, frequency,
                             ref ZlEffFreq, ref ErEffFreq);
            C2 = ErEffFreq / C0 / ZlEffFreq;
        }

        void initSP()
        {
            checkProperties();
        }

        void calcSP(double frequency)
        {
            Complex32 z = 2.0f / calcY(frequency) / (float)z0;
            Complex32 s11 = -1.0f / (z + 1.0f);
            Complex32 s21 = +z / (z + 1.0f);
            S[0, 0] = s11;
            S[1, 1] = s11;
            S[0, 1] = s21;
            S[1, 0] = s21;
        }

        void checkProperties()
        {
            if (W1 == W2)
            {
                Debug.WriteLine("LOG_ERROR: " + "ERROR: Strip widths of step discontinuity do not " +
                      "differ\n");
            }
            if (W1 >= s || W2 >= s)
            {
                Debug.WriteLine("LOG_ERROR: " + "ERROR: Strip widths of step discontinuity do not " +
                      "than groundplane gap\n");
            }
            if (er < 2 || er > 14)
            {
                Debug.WriteLine("LOG_ERROR: " + "WARNING: Model for coplanar step valid for " +
                      "2 < er < 14 (er = %g)\n", er);
            }
        }

        Complex32 calcY(double frequency)
        {
            double s1 = (s - W1) / 2;
            double s2 = (s - W2) / 2;
            double a, c, c1 = 0, c2 = 0, x1, x2;
            double o = 2 * Math.PI * frequency;
            calcCends(frequency, ref c1, ref c2);
            x1 = c1 * s1;
            x2 = c2 * s2;
            a = s1 > s2 ? s2 / s1 : s1 / s2;
            c = 1.0 / Math.PI * ((a * a + 1) / a * Math.Log((1 + a) / (1 - a)) -
                  2 * Math.Log(4 * a / (1 - a * a)));
            c = c * (x1 + x2) / 2;
            return new Complex32(0, (float)(c * o));
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

                Point p13 = new Point(p3.X, p3.Y - 20);
                Point p14 = new Point(p13.X + 40, p13.Y);
                Point p15 = new Point(p10.X, p10.Y + 20);
                Point p16 = new Point(p15.X + 40, p15.Y);

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

                gr.DrawLine(drawPen, p13, p14);
                gr.DrawLine(drawPen, p15, p16);

                // Create string to draw.
                String drawString = "CPWSTEP";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 3;
                float y = p1.Y - 50;

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

                    Point p13 = new Point(p3.X - 20, p3.Y);
                    Point p14 = new Point(p13.X, p13.Y + 40);
                    Point p15 = new Point(p10.X + 20, p10.Y);
                    Point p16 = new Point(p15.X, p15.Y + 40);

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

                    gr.DrawLine(drawPen, p13, p14);
                    gr.DrawLine(drawPen, p15, p16);

                    // Create string to draw.
                    String drawString = "CPWSTEP";

                    // Create point for upper-left corner of drawing.
                    float x = p1.X - 100;
                    float y = p1.Y + 25;

                    // Draw string to screen.
                    gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                }
            }
        }
    }
}

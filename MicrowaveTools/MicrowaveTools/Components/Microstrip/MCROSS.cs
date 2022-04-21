using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Microstrip
{
    class MCROSS : Comp
    {
        double W1, W2, W3, W4;
        double h, er, t;

        public MCROSS(float w1, float w2, float w3, float w4, Point location, int[] nodes)
        {
            Substrate subst = new Substrate();
            W1 = w1;
            W2 = w2;
            W3 = w3;
            W4 = w4;
            h = subst.h;
            er = subst.er;
            t = subst.t;


            Type = "MCROSS";
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
            Y = calcMatrixY(f);
            N = this.Nodes;
        }

        private Matrix<Complex32> calcMatrixY(float f)
        {
            double W1h, W2h;
            double C1, C2, C3, C4, L1, L2, L3, L4, L5;

            W1h = (W1 + W3) / 2 / h;
            W2h = (W2 + W4) / 2 / h;

            // apply asymmetric modifications of original model
            C1 = calcCap(W1, h, (W2 + W4) / 2);
            C2 = calcCap(W2, h, (W1 + W3) / 2);
            C3 = calcCap(W3, h, (W4 + W2) / 2);
            C4 = calcCap(W4, h, (W3 + W1) / 2);

            L1 = calcInd(W1, h, (W2 + W4) / 2);
            L2 = calcInd(W2, h, (W1 + W3) / 2);
            L3 = calcInd(W3, h, (W4 + W2) / 2);
            L4 = calcInd(W4, h, (W3 + W1) / 2);

            L5 = 1e-9 * h * (5 * W2h * Math.Cos(Math.PI / 2 * (1.5 - W1h)) -
                 (1 + 7 / W1h) / W2h - 337.5);

            // center inductance correction
            L5 = L5 * 0.8;

            // capacitance corrections
            C1 = C1 * capCorrection(W1, f);
            C2 = C2 * capCorrection(W2, f);
            C3 = C3 * capCorrection(W3, f);
            C4 = C4 * capCorrection(W4, f);

            // compute admittance matrix
            double o = 2 * Math.PI * f;
            Complex32 yc1 = new Complex32(0, (float)(o * C1));
            Complex32 yc2 = new Complex32(0, (float)(o * C2));
            Complex32 yc3 = new Complex32(0, (float)(o * C3));
            Complex32 yc4 = new Complex32(0, (float)(o * C4));
            Complex32 yl1 = 1.0f / new Complex32(0, (float)(o * L1));
            Complex32 yl2 = 1.0f / new Complex32(0, (float)(o * L2));
            Complex32 yl3 = 1.0f / new Complex32(0, (float)(o * L3));
            Complex32 yl4 = 1.0f / new Complex32(0, (float)(o * L4));
            Complex32 yl5 = 1.0f / new Complex32(0, (float)(o * L5));

            Matrix<Complex32> Ycross = Matrix<Complex32>.Build.Dense(6, 6);
            Ycross[0, 0] = yl1 + yc1;
            Ycross[1, 1] = yl2 + yc2;
            Ycross[2, 2] = yl3 + yc3;
            Ycross[3, 3] = yl4 + yc4;
            Ycross[0, 4] = -yl1; Ycross[4, 0] = -yl1;
            Ycross[2, 4] = -yl3; Ycross[4, 2] = -yl3;
            Ycross[1, 5] = -yl2; Ycross[5, 1] = -yl2;
            Ycross[3, 5] = -yl4; Ycross[5, 3] = -yl4;
            Ycross[4, 5] = -yl5; Ycross[5, 4] = -yl5;
            Ycross[4, 4] = yl1 + yl3 + yl5;
            Ycross[5, 5] = yl2 + yl4 + yl5;
            return Ycross;
        }

        private double capCorrection(double W, double f)
        {
            double Zl1 = 0, Er1 = 0, Zl2 = 0, Er2 = 0;
            double ZlEff = 0, ErEff = 0, WEff = 0;

            MLIN mlin = new MLIN();
            // calcQuasiStatic(double W, double h, double t, double er, double ZlEff, double ErEff, double WEff)
            mlin.calcQuasiStatic(W, h, t, 9.9, ref ZlEff, ref ErEff, ref WEff);
            // calcDispersion(double W, double h, double er, double ZlEff, double ErEff, double f, double ZleffFreq, double ErEffFreq)
            mlin.calcDispersion(W, h, 9.9, ZlEff, ErEff, f, ref Zl1, ref Er1);
            mlin.calcQuasiStatic(W, h, t, er, ref ZlEff, ref ErEff, ref WEff);
            mlin.calcDispersion(W, h, er, ZlEff, ErEff, f, ref Zl2, ref Er2);

            return Zl1 / Zl2 * Math.Sqrt(Er2 / Er1);
        }

        private double calcCap(double W1, double h, double W2)
        {
            double W1h = W1 / h;
            double W2h = W2 / h;

            double X = Math.Log10(W1h) * (86.6 * W2h - 30.9 * Math.Sqrt(W2h) + 367) +
                            Math.Pow(W2h, 3) + 74 * W2h + 130;
            return 1e-12 * W1 * (0.25 * X * Math.Pow(W1h, -1.0 / 3.0) - 60 +
                            1 / W2h / 2 - 0.375 * W1h * (1 - W2h));
        }

        private double calcInd(double W1, double h, double W2)
        {
            double W1h = W1 / h;
            double W2h = W2 / h;
            double Y = 165.6 * W2h + 31.2 * Math.Sqrt(W2h) - 11.8 * Math.Pow(W2h, 2);
            return 1e-9 * h * (Y * W1h - 32 * W2h + 3) * Math.Pow(W1h, -1.5);
        }

        // Let the MLIN draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            Point[] p = new Point[21];
            p[1] = Loc;            // Assume p1 is the input lead to the left
            p[2] = new Point(p[1].X + 10, p[1].Y);
            p[3] = new Point(p[2].X, p[2].Y - 10);
            p[4] = new Point(p[3].X + 10, p[3].Y);
            p[5] = new Point(p[4].X, p[4].Y-10);
            p[6] = new Point(p[5].X+20, p[5].Y);
            p[7] = new Point(p[6].X, p[6].Y+10);
            p[8] = new Point(p[7].X + 10, p[7].Y);
            p[9] = new Point(p[8].X, p[8].Y+20);
            p[10] = new Point(p[9].X-10, p[9].Y);
            p[11] = new Point(p[10].X, p[10].Y + 10);
            p[12] = new Point(p[11].X-20, p[11].Y);
            p[13] = new Point(p[12].X, p[12].Y-10);
            p[14] = new Point(p[13].X-10, p[13].Y);
            p[15] = new Point(p[5].X + 10, p[5].Y - 10);
            p[16] = new Point(p[15].X, p[15].Y + 10);
            p[17] = new Point(p[8].X + 10, p[8].Y + 10);
            p[18] = new Point(p[17].X-10, p[17].Y);
            p[19] = new Point(p[11].X-10, p[11].Y+10);
            p[20] = new Point(p[19].X, p[19].Y-10);

            for (int i = 1; i < 14; i++)
                gr.DrawLine(drawPen, p[i], p[i + 1]);
            gr.DrawLine(drawPen, p[14], p[2]);
            for (int i = 15; i < 21; i+=2)
                gr.DrawLine(drawPen, p[i], p[i + 1]);

            // Create string to draw.
            String drawString = "MLIN";

            // Create point for upper-left corner of drawing.
            float x = p[1].X - 20;
            float y = p[1].Y - 30;

            // Draw string to screen.
            gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }
    }
}

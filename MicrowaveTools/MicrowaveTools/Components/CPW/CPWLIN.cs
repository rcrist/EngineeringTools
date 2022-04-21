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
    class CPWLIN : Comp
    {
        public double Zl = 0;
        public double Er = 0;
        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);
        public double W;
        public double s;
        public double len;
        public int backMetal = 1;
        public int approx = 0;

        public double er;
        public double h;
        public double t;
        public double tand;
        public double rho;
        public double D;

        private double EPSI = 1.1920928955078125e-07;

        double sr_er;
        double sr_er0;
        double zl_factor;
        double ac_factor;
        double ad_factor;
        double bt_factor;
        double fte, G;
        double Z0 = 50;
        double z0 = 50;
        double C0 = 3e8; // Speed of light m/s
        double MU0 = 4e-7 * Math.PI;

        public CPWLIN()
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;
        }

        public CPWLIN(double w, double S, double l, int BackMetal, int Approx, Point location, int[] nodes)
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;

            Type = "MLIN";
            W = w;
            s = S;
            len = l;
            backMetal = BackMetal;
            approx = Approx;
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
        }

        /* The function computes the complete elliptic integral of first kind
           K() and the second kind E() using the arithmetic-geometric mean
           algorithm (AGM) by Abramowitz and Stegun. */
        void ellipke(double arg, ref double k, ref double e)
        {
            int iMax = 16;
            if (arg == 1.0)
            {
                k = double.PositiveInfinity; // infinite
                e = 0;
            }
            else if (arg == double.NegativeInfinity && arg < 0)
            {
                k = 0;
                e = double.PositiveInfinity; // infinite
            }
            else
            {
                double a, b, c, f, s, fk = 1, fe = 1, t, da = arg;
                int i;
                if (arg < 0)
                {
                    fk = 1 / Math.Sqrt(1 - arg);
                    fe = Math.Sqrt(1 - arg);
                    da = -arg / (1 - arg);
                }
                a = 1;
                b = Math.Sqrt(1 - da);
                c = Math.Sqrt(da);
                f = 0.5;
                s = f * c * c;
                for (i = 0; i < iMax; i++)
                {
                    t = (a + b) / 2;
                    c = (a - b) / 2;
                    b = Math.Sqrt(a * b);
                    a = t;
                    f *= 2;
                    s += f * c * c;
                    if (c / a < EPSI) break;
                }
                if (i >= iMax)
                {
                    k = 0; e = 0;
                }
                else
                {
                    k = Math.PI/2 / a;
                    e = Math.PI/2 * (1 - s) / a;
                    if (arg < 0)
                    {
                        k *= fk;
                        e *= fe;
                    }
                }
            }
        }

        /* We need to know only K(k), and if possible KISS. */
        double ellipk(double k)
        {
            double r=0, lost=0;
            ellipke(k, ref r, ref lost);
            return r;
        }

        /* More or less accurate approximation of K(k)/K'(k).  Suggested by
           publications dealing with coplanar components. */
        double ellipa(double k)
        {
            double r, kp;
            if (k < 1/Math.Sqrt(2))
            {
                kp = Math.Sqrt(1 - k * k);
                r = Math.PI / Math.Log(2 * (1 + Math.Sqrt(kp)) / (1 - Math.Sqrt(kp)));
            }
            else
            {
                r = Math.Log(2 * (1 + Math.Sqrt(k)) / (1 - Math.Sqrt(k))) / Math.PI;
            }
            return r;
        }

        void initSP()
        {
            // pre-compute propagation factors
            initPropagation();
        }

        public void initPropagation()
        {
            // other local variables (quasi-static constants)
            double k1, kk1, kpk1, k2, k3, q1, q2, q3 = 0, qz, er0 = 0;

            // compute the necessary quasi-static approx. (K1, K3, er(0) and Z(0))
            k1 = W / (W + s + s);
            kk1 = ellipk(k1);
            kpk1 = ellipk(Math.Sqrt(1 - k1 * k1));
            if (approx == 1)
            {
                q1 = ellipa(k1);
            }
            else
            {
                q1 = kk1 / kpk1;
            }

            // backside is metal
            if (backMetal == 1)
            {
                k3 = Trig.Tanh((Math.PI / 4) * (W / h)) / Trig.Tanh((Math.PI / 4) * (W + s + s) / h);
                if (approx == 1)
                {
                    q3 = ellipa(k3);
                }
                else
                {
                    q3 = ellipk(k3) / ellipk(Math.Sqrt(1 - k3 * k3));
                }
                qz = 1 / (q1 + q3);
                er0 = 1 + q3 * qz * (er - 1);
                zl_factor = Z0 / 2 * qz;
            }
            // backside is air
            else
            {
                k2 = Trig.Sinh((Math.PI / 4) * (W / h)) / Trig.Sinh((Math.PI / 4) * (W + s + s) / h);
                if (approx == 1)
                {
                    q2 = ellipa(k2);
                }
                else
                {
                    q2 = ellipk(k2) / ellipk(Math.Sqrt(1 - k2 * k2));
                }
                er0 = 1 + (er - 1) / 2 * q2 / q1;
                zl_factor = Z0 / 4 / q1;
            }

            // adds effect of strip thickness
            if (t > 0)
            {
                double d, se, We, ke, qe;
                d = (t * 1.25 / Math.PI) * (1 + Math.Log(4 * Math.PI * W / t));
                se = s - d;
                We = W + d;

                // modifies k1 accordingly (k1 = ke)
                ke = We / (We + se + se); // ke = k1 + (1 - k1 * k1) * d / 2 / s;
                if (approx == 1)
                {
                    qe = ellipa(ke);
                }
                else
                {
                    qe = ellipk(ke) / ellipk(Math.Sqrt(1 - ke * ke));
                }
                // backside is metal
                if (backMetal == 1)
                {
                    qz = 1 / (qe + q3);
                    er0 = 1 + q3 * qz * (er - 1);
                    zl_factor = Z0 / 2 * qz;
                }
                // backside is air
                else
                {
                    zl_factor = Z0 / 4 / qe;
                }

                // modifies er0 as well
                er0 = er0 - (0.7 * (er0 - 1) * t / s) / (q1 + (0.7 * t / s));
            }

            // pre-compute square roots
            sr_er = Math.Sqrt(er);
            sr_er0 = Math.Sqrt(er0);

            // cut-off frequency of the TE0 mode
            fte = (C0 / 4) / (h * Math.Sqrt(er - 1));

            // dispersion factor G
            double p = Math.Log(W / h);
            double u = 0.54 - (0.64 - 0.015 * p) * p;
            double v = 0.43 - (0.86 - 0.54 * p) * p;
            G = Math.Exp(u * Math.Log(W / s) + v);

            // loss constant factors (computed only once for efficency sake)
            double ac = 0;
            if (t > 0)
            {
                // equations by GHIONE
                double n = (1 - k1) * 8 * Math.PI / (t * (1 + k1));
                double a = W / 2;
                double b = a + s;
                ac = (Math.PI + Math.Log(n * a)) / a + (Math.PI + Math.Log(n * b)) / b;
            }
            ac_factor = ac / (4 * Z0 * kk1 * kpk1 * (1 - k1 * k1));
            ac_factor *= Math.Sqrt(Math.PI * MU0 * rho); // Rs factor
            ad_factor = (er / (er - 1)) * tand * Math.PI / C0;

            bt_factor = 2 * Math.PI / C0;
        }

        void calcAB(double f, ref double zl, ref double al, ref double bt)
        {
            double sr_er_f = sr_er0;
            double ac = ac_factor;
            double ad = ad_factor;

            // by initializing as much as possible outside this function, the
            // overhead is minimal

            // add the dispersive effects to er0
            sr_er_f += (sr_er - sr_er0) / (1 + G * Math.Pow(f / fte, -1.8));

            // computes impedance
            zl /= sr_er_f;

            // for now, the loss are limited to strip losses (no radiation
            // losses yet) losses in neper/length
            ad *= f * (sr_er_f - 1 / sr_er_f);
            ac *= Math.Sqrt(f) * sr_er0;

            al = ac + ad;
            bt *= sr_er_f * f;

            Er = Math.Pow(sr_er_f,2);
            Zl = zl;
        }

        void calcSP(double frequency)
        {

            double zl = zl_factor;
            double beta = bt_factor;
            double alpha=0;

            calcAB(frequency, ref zl, ref alpha, ref beta);

            // calculate and set S-parameters
            double z = zl / z0;
            double y = 1 / z;
            Complex32 g = new Complex32((float)alpha, (float)beta);
            Complex32 n = 2.0f * Complex32.Cosh(g * (float)len) + (float)(z + y) * Complex32.Sinh(g * (float)len);
            Complex32 s11 = (float)(z - y) * Complex32.Sinh(g * (float)len) / n;
            Complex32 s21 = 2.0f / n;

            S[0,0] = s11; S[1,1] = s11;
            S[0,1] = s21; S[1,0] = s21;
        }

        /* The function calculates the quasi-static impedance of a coplanar
           waveguide line and the value of the effective dielectric constant
           for the given coplanar line and substrate properties. */
        public void analyseQuasiStatic(double W, double s, double h,
                          double t, double er, int backMetal,
                          ref double ZlEff, ref double ErEff)
        {

            // local variables (quasi-static constants)
            double k1, k2, k3, q1, q2, q3 = 0, qz;

            ErEff = er;
            ZlEff = 0;

            // compute the necessary quasi-static approx. (K1, K3, er(0) and Z(0))
            k1 = W / (W + s + s);
            q1 = ellipk(k1) / ellipk(Math.Sqrt(1 - k1 * k1));

            // backside is metal
            if (backMetal == 1)
            {
                k3 = Trig.Tanh((Math.PI / 4) * (W / h)) / Trig.Tanh((Math.PI / 4) * (W + s + s) / h);
                q3 = ellipk(k3) / ellipk(Math.Sqrt(1 - k3 * k3));
                qz = 1 / (q1 + q3);
                ErEff = 1 + q3 * qz * (er - 1);
                ZlEff = Z0 / 2 * qz;
            }
            // backside is air
            else
            {
                k2 = Trig.Sinh((Math.PI / 4) * (W / h)) / Trig.Sinh((Math.PI / 4) * (W + s + s) / h);
                q2 = ellipk(k2) / ellipk(Math.Sqrt(1 - k2 * k2));
                ErEff = 1 + (er - 1) / 2 * q2 / q1;
                ZlEff = Z0 / 4 / q1;
            }

            // adds effect of strip thickness
            if (t > 0)
            {
                double d, se, We, ke, qe;
                d = (t * 1.25 / Math.PI) * (1 + Math.Log(4 * Math.PI * W / t));
                se = s - d;
                We = W + d;

                // modifies k1 accordingly (k1 = ke)
                ke = We / (We + se + se); // ke = k1 + (1 - k1 * k1) * d / 2 / s;
                qe = ellipk(ke) / ellipk(Math.Sqrt(1 - ke * ke));

                // backside is metal
                if (backMetal ==  1)
                {
                    qz = 1 / (qe + q3);
                    ErEff = 1 + q3 * qz * (er - 1);
                    ZlEff = Z0 / 2 * qz;
                }
                // backside is air
                else
                {
                    ZlEff = Z0 / 4 / qe;
                }

                // modifies ErEff as well
                ErEff = ErEff - (0.7 * (ErEff - 1) * t / s) / (q1 + (0.7 * t / s));
            }
            ErEff = Math.Sqrt(ErEff);
            ZlEff /= ErEff;
        }

        /* This function calculates the frequency dependent value of the
           effective dielectric constant and the coplanar line impedance for
           the given frequency. */
        public void analyseDispersion(double W, double s, double h,
                         double er, double ZlEff,
                         double ErEff, double frequency,
                         ref double ZlEffFreq,
                         ref double ErEffFreq)
        {
            // local variables
            double fte, G;

            ErEffFreq = ErEff;
            ZlEffFreq = ZlEff * ErEff;

            // cut-off frequency of the TE0 mode
            fte = (C0 / 4) / (h * Math.Sqrt(er - 1));

            // dispersion factor G
            double p = Math.Log(W / h);
            double u = 0.54 - (0.64 - 0.015 * p) * p;
            double v = 0.43 - (0.86 - 0.54 * p) * p;
            G = Math.Exp(u * Math.Log(W / s) + v);

            // add the dispersive effects to er0
            ErEffFreq += (Math.Sqrt(er) - ErEff) / (1 + G * Math.Pow(frequency / fte, -1.8));

            // computes impedance
            ZlEffFreq /= ErEffFreq;
        }

        // Let the CPWLIN draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + 10, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 10); // Location of rectangle
                Point p4 = new Point(p2.X + 40, p2.Y);
                Point p5 = new Point(p4.X + 10, p4.Y);

                Point p6 = new Point(p2.X, p2.Y - 20);
                Point p7 = new Point(p6.X + 40, p6.Y);

                Point p8 = new Point(p2.X, p2.Y + 20);
                Point p9 = new Point(p8.X + 40, p8.Y);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 40, 20);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawLine(drawPen, p8, p9);

                // Create string to draw.
                String drawString = "CPWLIN";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 5;
                float y = p1.Y - 40;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if (Orientation == "Shunt")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X, p1.Y + 10);
                Point p3 = new Point(p2.X - 10, p2.Y); // Location of rectangle
                Point p4 = new Point(p2.X, p2.Y + 40);
                Point p5 = new Point(p4.X, p4.Y + 10);

                Point p6 = new Point(p2.X - 20, p2.Y);
                Point p7 = new Point(p6.X, p6.Y + 40);

                Point p8 = new Point(p2.X + 20, p2.Y);
                Point p9 = new Point(p8.X, p8.Y + 40);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 20, 40);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawLine(drawPen, p8, p9);

                // Create string to draw.
                String drawString = "CPWLIN";

                // Create point for upper-left corner of drawing.
                float x = p1.X - 80;
                float y = p1.Y + 25;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }
    }
}

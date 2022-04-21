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
    class MCPLIN : Comp
    {
        public float W;
        public float s;
        public float l;    // Length
        public Substrate subst;     // Substrate
        public float Temp = 26.85f; // Temperature
        public float z0 = 50;       // Reference impedance
        public float Z0 = 50;
        public float U0 = 4e-7f * (float)Math.PI;
        public float C0 = 3e8f;   // Speed of light
        public float E0 = 8.854e-12f; // f/m

        public float er;
        public float h;
        public float t;
        public float tand;
        public float rho;
        public float D;
        public float r;
        public float ac, ad;
        public float ZlEff, ErEff, WEff, ZlEffFreq, ErEffFreq;
        public float alpha, beta, zl, ereff;
        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(4, 4);
        private float ae, be, ze, ao, bo, zo, ee, eo;
        public float ZlEven, ErEven, ZlOdd, ErOdd;

        public MCPLIN()
        {
            Substrate subst = new Substrate();
            er = (float)subst.er;
            h = (float)subst.h;
            t = (float)subst.t;
            tand = (float)subst.tand;
            rho = (float)subst.rho;
            D = (float)subst.d;
            r = D / 2f;
        }

        public MCPLIN(float w, float S, float L, Point location, int[] nodes)
        {
            W = w;
            s = S;
            l = L;
            Substrate subst = new Substrate();
            er = (float)subst.er;
            h = (float)subst.h;
            t = (float)subst.t;
            tand = (float)subst.tand;
            rho = (float)subst.rho;
            D = (float)subst.d;

            Type = "MCPLIN";
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
        }

        void calcPropagation(float frequency)
        {
            // quasi-static analysis
            float Zle=0, ErEffe=0, Zlo=0, ErEffo=0;

            analysQuasiStatic(W, h, s, t, er, ref Zle, ref Zlo, ref ErEffe, ref ErEffo);

            // analyse dispersion of Zl and Er
            float ZleFreq=0, ErEffeFreq=0, ZloFreq=0, ErEffoFreq=0;
            analyseDispersion(W, h, s, er, Zle, Zlo, ErEffe, ErEffo, frequency, ref ZleFreq, ref ZloFreq, 
                ref ErEffeFreq, ref ErEffoFreq);

            // analyse losses of line
            double ace=0, aco=0, ade=0, ado=0;
            MLIN mlin = new MLIN();
            mlin.calcLoss(W, t, er, rho, D, tand, Zle, Zlo, ErEffe, frequency, ref ace, ref ade);
            mlin.calcLoss(W, t, er, rho, D, tand, Zlo, Zle, ErEffo, frequency, ref aco, ref ado);

            // compute propagation constants for even and odd mode
            float k0 = 2f * (float)Math.PI * frequency / C0;
            ae = (float)(ace + ade);
            ao = (float)(aco + ado);
            be = (float)Math.Sqrt(ErEffeFreq) * k0;
            bo = (float)Math.Sqrt(ErEffoFreq) * k0;
            ze = ZleFreq;
            zo = ZloFreq;
            ee = ErEffeFreq;
            eo = ErEffoFreq;
        }

        void saveCharacteristics()
        {
            ZlEven = ze;
            ErEven = ee;
            ZlOdd = zo;
            ErOdd = eo;
        }

        void calcSP(float frequency)
        {
            // compute propagation constants for even and odd mode
            calcPropagation(frequency);
            Complex32 ge = new Complex32(ae, be);
            Complex32 go = new Complex32(ao, bo);

            // compute abbreviations
            Complex32 Ee, Eo, De, Do, Xe, Xo, Ye, Yo;
            Ee = (float)(Math.Pow(ze,2) + Math.Pow(z0,2)) * Complex32.Sinh(ge * l);
            Eo = (float)(Math.Pow(zo,2) + Math.Pow(z0,2)) * Complex32.Sinh(go * l);
            De = 2 * ze * z0 * Complex32.Cosh(ge * l) + Ee;
            Do = 2 * zo * z0 * Complex32.Cosh(go * l) + Eo;
            Xe = (float)(Math.Pow(ze,2) - Math.Pow(z0,2)) * Complex32.Sinh(ge * l) / 2.0f / De;
            Xo = (float)(Math.Pow(zo,2) - Math.Pow(z0,2)) * Complex32.Sinh(go * l) / 2.0f / Do;
            Ye = ze * z0 / De;
            Yo = zo * z0 / Do;

            // reflexion coefficients
            S[0,0] = Xe + Xo; S[1,1] = Xe + Xo;
            S[2,2] = Xe + Xo; S[3,3] = Xe + Xo;
            // through paths
            S[0,1] = Ye + Yo; S[1,0] = Ye + Yo;
            S[2,3] = Ye + Yo; S[3,2] = Ye + Yo;
            // coupled paths
            S[0,3] = Xe - Xo; S[3,0] = Xe - Xo;
            S[1,2] = Xe - Xo; S[2,1] = Xe - Xo;
            // isolated paths
            S[0,2] = Ye - Yo; S[2,0] = Ye - Yo;
            S[1,3] = Ye - Yo; S[3,1] = Ye - Yo;
        }

        /* The function calculates the quasi-static dielectric constants and
           characteristic impedances for the even and odd mode based upon the
           given line and substrate properties for parallel coupled microstrip
           lines. */
        void analysQuasiStatic(float W, float h, float s,
                           float t, float er,
                           ref float Zle,
                           ref float Zlo, ref float ErEffe,
                           ref float ErEffo)
        {
            // initialize default return values
            ErEffe = ErEffo = er;
            Zlo = 42.2f; Zle = 55.7f;

            // normalized width and gap
            float u = W / h;
            float g = s / h;

            // HAMMERSTAD and JENSEN
            float Zl1, Fe, Fo, fo, Mu, Alpha, Beta;
            float Pe, Po, r, fo1, q, p, n, Psi, Phi, m, Theta;
            double a = 0, b = 0, ErEff = 0;

            // modifying equations for even mode
            m = (float)(0.2175 + Math.Pow(4.113 + Math.Pow(20.36 / g, 6), -0.251) +
                Math.Log(Math.Pow(g, 10) / (1 + Math.Pow(g / 13.8, 10))) / 323);
            Alpha = (float)(0.5 * Math.Exp(-g));
            Psi = (float)(1 + g / 1.45 + Math.Pow(g, 2.09) / 3.95);
            Phi = (float)(0.8645 * Math.Pow(u, 0.172));
            Pe = (float)(Phi / (Psi * (Alpha * Math.Pow(u, m) + (1 - Alpha) * Math.Pow(u, -m))));
            // TODO: is this ... Psi * (Alpha ... or ... Psi / (Alpha ... ?

            // modifying equations for odd mode
            n = (float)((1 / 17.7 + Math.Exp(-6.424 - 0.76 * Math.Log(g) - Math.Pow(g / 0.23, 5))) *
                Math.Log((10 + 68.3 * Math.Pow(g,2)) / (1 + 32.5 * Math.Pow(g, 3.093))));
            Beta = (float)(0.2306 + Math.Log(Math.Pow(g, 10) / (1 + Math.Pow(g / 3.73, 10))) / 301.8 +
                Math.Log(1 + 0.646 * Math.Pow(g, 1.175)) / 5.3);
            Theta = (float)(1.729 + 1.175 * Math.Log(1 + 0.627 / (g + 0.327 * Math.Pow(g, 2.17))));
            Po = (float)(Pe - Theta / Psi * Math.Exp(Beta * Math.Pow(u, -n) * Math.Log(u)));

            // further modifying equations
            r = (float)(1 + 0.15 * (1 - Math.Exp(1 - Math.Pow(er - 1,2) / 8.2) / (1 + Math.Pow(g, -6))));
            fo1 = (float)(1 - Math.Exp(-0.179 * Math.Pow(g, 0.15) -
                    0.328 * Math.Pow(g, r) / Math.Log(E0 + Math.Pow(g / 7, 2.8))));
            q = (float)(Math.Exp(-1.366 - g));
            p = (float)(Math.Exp(-0.745 * Math.Pow(g, 0.295)) / Trig.Cosh(Math.Pow(g, 0.68)));
            fo = (float)(fo1 * Math.Exp(p * Math.Log(u) + q * Math.Sin(Math.PI * Math.Log10(u))));

            Mu = (float)(g * Math.Exp(-g) + u * (20 + Math.Pow(g,2)) / (10 + Math.Pow(g,2)));

            MLIN mlin = new MLIN();
            mlin.calcAB(Mu, er, ref a, ref b);
            Fe = (float)(Math.Pow(1 + 10 / Mu, -a * b));
            mlin.calcAB(u, er, ref a, ref b);
            Fo = (float)(fo * Math.Pow(1 + 10 / u, -a * b));

            // finally compute effective dielectric constants and impedances
            ErEffe = (er + 1) / 2 + (er - 1) / 2 * Fe;
            ErEffo = (er + 1) / 2 + (er - 1) / 2 * Fo;

            ErEff = mlin.calcEr(u, er, a, b);  // single microstrip

            // first variant
            Zl1 = (float)(Z0 / (u + 1.98 * Math.Pow(u, 0.172)));
            Zl1 /= (float)(Math.Sqrt(ErEff));

            // second variant
            Zl1 = (float)(mlin.calcZl(u));
            Zl1 /= (float)(Math.Sqrt(ErEff));

            Zle = Zl1 / (1 - Zl1 * Pe / Z0);
            Zlo = Zl1 / (1 - Zl1 * Po / Z0);
            
        }

        /* The function computes the dispersion effects on the dielectric
           constants and characteristic impedances for the even and odd mode
           of parallel coupled microstrip lines. */
        void analyseDispersion(float W, float h, float s,
                           float er, float Zle,
                           float Zlo, float ErEffe,
                           float ErEffo, float frequency,
                           ref float ZleFreq,
                           ref float ZloFreq,
                           ref float ErEffeFreq,
                           ref float ErEffoFreq)
        {

            // initialize default return values
            ZleFreq = Zle;
            ErEffeFreq = ErEffe;
            ZloFreq = Zlo;
            ErEffoFreq = ErEffo;

            // normalized width and gap
            float u = W / h;
            float g = s / h;

 
            // KIRSCHNING and JANSEN
            double p1, p2, p3, p4, p5, p6, p7, Fe;
            double fn = frequency * h * 1e-6;

            // even relative dielectric constant dispersion
            p1 = 0.27488 * (0.6315 + 0.525 / Math.Pow(1 + 0.0157 * fn, 20)) * u -
                0.065683 * Math.Exp(-8.7513 * u);
            p2 = 0.33622 * (1 - Math.Exp(-0.03442 * er));
            p3 = 0.0363 * Math.Exp(-4.6 * u) * (1 - Math.Exp(-Math.Pow(fn / 38.7, 4.97)));
            p4 = 1 + 2.751 * (1 - Math.Exp(-Math.Pow(er / 15.916, 8)));
            p5 = 0.334 * Math.Exp(-3.3 * Math.Pow(er / 15,3)) + 0.746;
            p6 = p5 * Math.Exp(-Math.Pow(fn / 18, 0.368));
            p7 = 1 + 4.069 * p6 * Math.Pow(g, 0.479) *
                Math.Exp(-1.347 * Math.Pow(g, 0.595) - 0.17 * Math.Pow(g, 2.5));
            Fe = p1 * p2 * Math.Pow((p3 * p4 + 0.1844 * p7) * fn, 1.5763);
            ErEffeFreq = (float)(er - (er - ErEffe) / (1 + Fe));

            // odd relative dielectric constant dispersion
            double p8, p9, p10, p11, p12, p13, p14, p15, Fo;
            p8 = 0.7168 * (1 + 1.076 / (1 + 0.0576 * (er - 1)));
            p9 = p8 - 0.7913 * (1 - Math.Exp(-Math.Pow(fn / 20, 1.424))) *
                Math.Atan(2.481 * Math.Pow(er / 8, 0.946));
            p10 = 0.242 * Math.Pow(er - 1, 0.55);
            p11 = 0.6366 * (Math.Exp(-0.3401 * fn) - 1) *
                Math.Atan(1.263 * Math.Pow(u / 3, 1.629));
            p12 = p9 + (1 - p9) / (1 + 1.183 * Math.Pow(u, 1.376));
            p13 = 1.695 * p10 / (0.414 + 1.605 * p10);
            p14 = 0.8928 + 0.1072 * (1 - Math.Exp(-0.42 * Math.Pow(fn / 20, 3.215)));
            p15 = Math.Abs(1 - 0.8928 * (1 + p11) *
                Math.Exp(-p13 * Math.Pow(g, 1.092)) * p12 / p14);
            Fo = p1 * p2 * Math.Pow((p3 * p4 + 0.1844) * fn * p15, 1.5763);
            ErEffoFreq = (float)(er - (er - ErEffo) / (1 + Fo));

            // dispersion of even characteristic impedance
            double t, q11, q12, q13, q14, q15, q16, q17, q18, q19, q20, q21;
            q11 = 0.893 * (1 - 0.3 / (1 + 0.7 * (er - 1)));
            t = Math.Pow(fn / 20, 4.91);
            q12 = 2.121 * t / (1 + q11 * t) * Math.Exp(-2.87 * g) * Math.Pow(g, 0.902);
            q13 = 1 + 0.038 * Math.Pow(er / 8, 5.1);
            t = Math.Pow(er / 15,4);
            q14 = 1 + 1.203 * t / (1 + t);
            q15 = 1.887 * Math.Exp(-1.5 * Math.Pow(g, 0.84)) * Math.Pow(g, q14) /
                (1 + 0.41 * Math.Pow(fn / 15, 3) *
                Math.Pow(u, 2 / q13) / (0.125 + Math.Pow(u, 1.626 / q13)));
            q16 = q15 * (1 + 9 / (1 + 0.403 * Math.Pow(er - 1,2)));
            q17 = 0.394 * (1 - Math.Exp(-1.47 * Math.Pow(u / 7, 0.672))) *
                (1 - Math.Exp(-4.25 * Math.Pow(fn / 20, 1.87)));
            q18 = 0.61 * (1 - Math.Exp(-2.31 * Math.Pow(u / 8, 1.593))) /
                (1 + 6.544 * Math.Pow(g, 4.17));
            q19 = 0.21 * Math.Pow(g,4) / (1 + 0.18 * Math.Pow(g, 4.9)) / (1 + 0.1 * Math.Pow(u,2)) /
                (1 + Math.Pow(fn / 24, 3));
            q20 = q19 * (0.09 + 1 / (1 + 0.1 * Math.Pow(er - 1, 2.7)));
            t = Math.Pow(u, 2.5);
            q21 = Math.Abs(1 - 42.54 * Math.Pow(g, 0.133) * Math.Exp(-0.812 * g) * t /
                (1 + 0.033 * t));

            double re, qe, pe, de, Ce, q0=0, ZlFreq=0, ErEffFreq=0;
            MLIN mlin = new MLIN();
            mlin.Kirschning_er(u, fn, er, ErEffe, ref ErEffFreq);
            mlin.Kirschning_zl(u, fn, er, ErEffe, ErEffFreq, Zle, ref q0, ref ZlFreq);
            re = Math.Pow(fn / 28.843, 12);
            qe = 0.016 + Math.Pow(0.0514 * er * q21, 4.524);
            pe = 4.766 * Math.Exp(-3.228 * Math.Pow(u, 0.641));
            t = Math.Pow(er - 1, 6);
            de = 5.086 * qe * re / (0.3838 + 0.386 * qe) *
                Math.Exp(-22.2 * Math.Pow(u, 1.92)) / (1 + 1.2992 * re) * t / (1 + 10 * t);
            Ce = 1 + 1.275 * (1 - Math.Exp(-0.004625 * pe * Math.Pow(er, 1.674) *
                Math.Pow(fn / 18.365, 2.745))) - q12 + q16 - q17 + q18 + q20;
            ZleFreq = (float)(Zle * Math.Pow((0.9408 * Math.Pow(ErEffFreq, Ce) - 0.9603) /
                        ((0.9408 - de) * Math.Pow(ErEffe, Ce) - 0.9603), q0));

            // dispersion of odd characteristic impedance
            double q22, q23, q24, q25, q26, q27, q28, q29;
            mlin.Kirschning_er(u, fn, er, ErEffo, ref ErEffFreq);
            mlin.Kirschning_zl(u, fn, er, ErEffo, ErEffFreq, Zlo, ref q0, ref ZlFreq);
            q29 = 15.16 / (1 + 0.196 * Math.Pow(er - 1,2));
            t = Math.Pow(er - 1,2);
            q25 = 0.3 * Math.Pow(fn,2) / (10 + Math.Pow(fn,2)) * (1 + 2.333 * t / (5 + t));
            t = Math.Pow((er - 1) / 13, 12);
            q26 = 30 - 22.2 * t / (1 + 3 * t) - q29;
            t = Math.Pow(er - 1, 1.5);
            q27 = 0.4 * Math.Pow(g, 0.84) * (1 + 2.5 * t / (5 + t));
            t = Math.Pow(er - 1, 3);
            q28 = 0.149 * t / (94.5 + 0.038 * t);
            q22 = 0.925 * Math.Pow(fn / q26, 1.536) / (1 + 0.3 * Math.Pow(fn / 30, 1.536));
            q23 = 1 + 0.005 * fn * q27 / (1 + 0.812 * Math.Pow(fn / 15, 1.9)) /
                (1 + 0.025 * Math.Pow(u,2));
            t = Math.Pow(u, 0.894);
            q24 = 2.506 * q28 * t / (3.575 + t) *
                Math.Pow((1 + 1.3 * u) * fn / 99.25, 4.29);
            ZloFreq = (float)(ZlFreq + (Zlo * Math.Pow(ErEffoFreq / ErEffo, q22) - ZlFreq * q23) /
                (1 + q24 + Math.Pow(0.46 * g, 2.2) * q25));
        }

        // Let the MCPLIN draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + 10, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 5);      // Location of rectangle #1
                Point p4 = new Point(p2.X + 40, p2.Y-20);
                Point p5 = new Point(p4.X + 10, p4.Y);
                Point p6 = new Point(p2.X, p2.Y-25);      // Location of rectangle #2

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 40, 10);
                gr.DrawRectangle(drawPen, p6.X, p6.Y, 40, 10);

                // Create string to draw.
                String drawString = "MCPLIN";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 5;
                float y = p1.Y - 45;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if (Orientation == "Shunt")
            {
                Point p1 = Loc;             
                Point p2 = new Point(p1.X, p1.Y + 10);
                Point p3 = new Point(p2.X - 5, p2.Y);      // Location of rectangle #1
                Point p4 = new Point(p2.X + 20, p2.Y + 40);
                Point p5 = new Point(p4.X, p4.Y + 10);
                Point p6 = new Point(p2.X + 15, p2.Y);      // Location of rectangle #2

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 10, 40);
                gr.DrawRectangle(drawPen, p6.X, p6.Y, 10, 40);

                // Create string to draw.
                String drawString = "MCPLIN";

                // Create point for upper-left corner of drawing.
                float x = p1.X - 60;
                float y = p1.Y + 25;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }

    }
}

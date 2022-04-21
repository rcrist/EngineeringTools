// C# class libraries
using System;
using System.Diagnostics;
using System.Drawing;

// MathNet.Numerics math libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

// Microwave tools libraries
using static MicrowaveTools.Helper.Globals;
using static MicrowaveTools.Helper.Helper;

namespace MicrowaveTools.Components.Microstrip
{
    class Microstrip
    {
        public double W;     // Width
        public double L;    // Length

        public double er;
        public double h;
        public double t;
        public double tand;
        public double rho;
        public double D;
        public double ac, ad, ac_db, ad_db;
        public double ZlEff, ErEff, WEff, ZlEffFreq, ErEffFreq;
        public double alpha, beta, zl, ereff;
        public double eLen;
        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);
        public double Z0_h_1;               /* homogeneous stripline impedance */

        public double sigma;             /* Conductivity of the metal */
        public double mur;               /* mag. permeability */
        public double skindepth;         /* Skin depth */

        public double fu;
        public double a = 0, b = 0, du1, du, u, ur, u1, zr, z1;
        public double z, e, Zo;

        public Microstrip(Substrate subst, double w, double l, double Sigma)
        {
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;

            W = w;
            L = l;
            sigma = Sigma;
        }

        public void calcPropagation(double frequency)
        {
            // quasi-static effective dielectric constant of substrate + line and
            // the impedance of the microstrip line
            calcQuasiStatic(W, h, t, er, ref ZlEff, ref ErEff, ref WEff);
            Console.WriteLine("Quasi-Static Calculations");
            Console.WriteLine("EfEff1 = " + ErEff);
            Console.WriteLine("Z1Eff1 = " + ZlEff);

            // analyse dispersion of Zl and Er (use WEff here?)
            calcDispersion(W, h, er, ZlEff, ErEff, frequency, ref ZlEffFreq, ref ErEffFreq);
            Console.WriteLine("\nDispersion Calculations");
            Console.WriteLine("EfEff1 = " + ErEffFreq);
            Console.WriteLine("Z1Eff1 = " + ZlEffFreq);

            // analyse losses of line
            calcLoss(W, t, er, rho, D, tand, ZlEffFreq, ZlEffFreq, ErEffFreq, frequency, ref ac, ref ad);

            // calculate propagation constants and reference impedance
            zl = ZlEffFreq;
            ereff = ErEffFreq;
            alpha = ac + ad;
            beta = Math.Sqrt(ErEffFreq) * 2 * Math.PI * frequency / C0;

            skindepth = skin_depth(frequency);
            eLen = elecLength(frequency);
        }

        public void calcSP(double frequency)
        {
            double l = L;

            // calculate propagation constants
            calcPropagation(frequency);

            // calculate S-parameters
            double z = zl / z0;
            double y = 1 / z;
            Complex32 g = new Complex32((float)alpha, (float)beta);
            Complex32 test = new Complex32(1.0f, 2.0f);
            Complex32 n = 2.0f * Complex32.Cosh(g * (float)l) + (float)(z + y) * Complex32.Sinh(g * (float)l);
            Complex32 s11 = (float)(z - y) * Complex32.Sinh(g * (float)l) / n;
            Complex32 s21 = 2.0f / n;
            S[0, 0] = s11; S[1, 1] = s11;
            S[1, 0] = s21; S[0, 1] = s21;
        }

        public void calcQuasiStatic(double W, double h, double t, double er, ref double ZlEff, ref double ErEff, ref double WEff)
        {
            e = er;
            z = z0;
            WEff = W;

            u = W / h; // normalized width
            t = t / h; // normalized thickness

            // compute strip thickness effect
            if (t != 0)
            {
                du1 = t / Math.PI * Math.Log(1 + 4 * Math.E / (t * Math.Pow(Helper.Helper.Coth(Math.Sqrt(6.517 * u)), 2.0)));
            }
            else du1 = 0;
            du = du1 * (1 + Helper.Helper.Sech(Math.Sqrt(er - 1))) / 2;
            u1 = u + du1;
            ur = u + du;
            WEff = ur * h;

            // compute impedances for homogeneous medium
            zr = calcZl(ur);
            Zo = zr / Math.Sqrt(e);
            z1 = calcZl(u1);

            // compute effective dielectric constant
            calcAB(ur, er, ref a, ref b);
            e = calcEr(ur, er, a, b);

            // compute final characteristic impedance and dielectric constant
            // including strip thickness effects
            z = zr / Math.Sqrt(e);
            e = e * Math.Pow((z1 / zr), 2);

            ZlEff = z;
            ErEff = e;
        }

        public void calcDispersion(double W, double h, double er, double ZlEff, double ErEff, double f, ref double ZleffFreq, ref double ErEffFreq)
        {
            double e, z;
            double g;


            g = Math.Pow(Math.PI, 2) / 12 * (er - 1) / ErEff * Math.Sqrt(2 * Math.PI * ZlEff / Z0);
            f = 2 * MU0 * h * f / ZlEff;
            e = er - (er - ErEff) / (1 + g * Math.Pow(f, 2));
            z = ZlEff * Math.Sqrt(ErEff / e) * (e - 1) / (ErEff - 1);

            ZlEffFreq = z;
            ErEffFreq = e;
        }

        public void calcAB(double u, double er, ref double a, ref double b)
        {
            a = 1 + Math.Log(Math.Pow(u, 4) + Math.Pow(u / 52, 2)) / (Math.Pow(u, 4) + 0.432) / 49 +
                Math.Log(1 + Math.Pow(u / 18.1, 3)) / 18.7;
            b = 0.564 * Math.Pow((er - 0.9) / (er + 3), 0.053);
        }

        public double calcEr(double u, double er, double a, double b)
        {
            double e;
            e = (er + 1) / 2 + (er - 1) / 2 * Math.Pow(1 + 10 / u, -a * b);
            return e;
        }

        public double calcZl(double u)
        {
            fu = 6 + (2 * Math.PI - 6) * Math.Exp(-Math.Pow(30.666 / u, 0.7528));
            double zl = ZF0 / (2 * Math.PI) * Math.Log(fu / u + Math.Sqrt(1 + Math.Pow(2 / u, 2)));
            return zl;
        }

        /* This function computes the dispersion of the effective dielectric
           constant of a single microstrip line.  It is defined in a separate
           function because it is used within the coupled microstrip lines as
           well. */
        public void Kirschning_er(double u, double fn, double er, double ErEff, ref double ErEffFreq)
        {
            double p, p1, p2, p3, p4;
            p1 = 0.27488 + (0.6315 + 0.525 / Math.Pow(1 + 0.0157 * fn, 20)) * u -
              0.065683 * Math.Exp(-8.7513 * u);
            p2 = 0.33622 * (1 - Math.Exp(-0.03442 * er));
            p3 = 0.0363 * Math.Exp(-4.6 * u) * (1 - Math.Exp(-Math.Pow(fn / 38.7, 4.97)));
            p4 = 1 + 2.751 * (1 - Math.Exp(-Math.Pow(er / 15.916, 8)));
            p = p1 * p2 * Math.Pow((0.1844 + p3 * p4) * fn, 1.5763);
            ErEffFreq = er - (er - ErEff) / (1 + p);
        }

        /* Computes dispersion effects of characteristic impedance of a single
           microstrip line according to Kirschning and Jansen.  Also used in
           coupled microstrip lines calculations. */
        public void Kirschning_zl(double u, double fn, double er,
                        double ErEff, double ErEffFreq,
                        double ZlEff, ref double r17,
                        ref double ZlEffFreq)
        {
            double r1, r2, r3, r4, r5, r6, r7, r8, r9, r10;
            double r11, r12, r13, r14, r15, r16;
            r1 = 0.03891 * Math.Pow(er, 1.4);
            r2 = 0.267 * Math.Pow(u, 7);
            r3 = 4.766 * Math.Exp(-3.228 * Math.Pow(u, 0.641));
            r4 = 0.016 + Math.Pow(0.0514 * er, 4.524);
            r5 = Math.Pow(fn / 28.843, 12);
            r6 = 22.20 * Math.Pow(u, 1.92);
            r7 = 1.206 - 0.3144 * Math.Exp(-r1) * (1 - Math.Exp(-r2));
            r8 = 1 + 1.275 * (1 - Math.Exp(-0.004625 * r3 *
                           Math.Pow(er, 1.674) * Math.Pow(fn / 18.365, 2.745)));
            r9 = 5.086 * r4 * r5 / (0.3838 + 0.386 * r4) *
              Math.Exp(-r6) / (1 + 1.2992 * r5) *
              Math.Pow(er - 1, 6) / (1 + 10 * Math.Pow(er - 1, 6));
            r10 = 0.00044 * Math.Pow(er, 2.136) + 0.0184;
            r11 = Math.Pow(fn / 19.47, 6) / (1 + 0.0962 * Math.Pow(fn / 19.47, 6));
            r12 = 1 / (1 + 0.00245 * Math.Pow(u, 2));
            r13 = 0.9408 * Math.Pow(ErEffFreq, r8) - 0.9603;
            r14 = (0.9408 - r9) * Math.Pow(ErEff, r8) - 0.9603;
            r15 = 0.707 * r10 * Math.Pow(fn / 12.3, 1.097);
            r16 = 1 + 0.0503 * Math.Pow(er, 2) * r11 * (1 - Math.Exp(-Math.Pow(u / 15, 6)));
            r17 = r7 * (1 - 1.1241 * r12 / r16 *
                    Math.Exp(-0.026 * Math.Pow(fn, 1.15656) - r15));
            ZlEffFreq = ZlEff * Math.Pow(r13 / r14, r17);
        }

        public void calcLoss(double W, double t, double er, double rho, double D, double tand,
            double ZlEff1, double ZlEff2, double Ereff, double f, ref double ac, ref double ad)
        {
            ac = ad = 0;

            double Rs, ds, l0, Kr, Ki;

            // conductor losses
            if (t != 0.0)
            {
                Rs = Math.Sqrt(rho * Math.PI * (f * 1e9) * MU0); // skin resistance
                ds = rho / Rs;                            // skin depth

                // valid for t > 3 * ds
                if (t < 3 * ds)
                {
                    Debug.WriteLine("LOG_ERROR " +
                          "WARNING: conductor loss calculation invalid for line " +
                          "height t (%g) < 3 * skin depth (%g)\n", t, 3 * ds);
                }
                // current distribution factor
                Ki = Math.Exp(-1.2 * Math.Pow(ZlEff1 / Z0, 0.7));
                // D is RMS surface roughness
                Kr = 1 + Math.PI / 2 * Math.Atan(1.4 * Math.Pow(D / ds, 2));
                //Kr = 1 + 2 / Math.PI * Math.Atan(1.4 * Math.Pow(D / ds, 2));
                ac = Rs / (ZlEff1 * W) * Kr * Ki;
                double ac_db_len = 20 * Math.Log10(Math.Exp(1.0)) * ac;
                ac_db = ac_db_len * L;
            }

            // dielectric losses
            l0 = C0 / (f * 1e9);

            ad = Math.PI * er / (er - 1) * (ErEff - 1) / Math.Sqrt(ErEff) * tand / l0;
            double ad_db_len = 20 * Math.Log10(Math.Exp(1.0)) * ad;
            ad_db = ad_db_len * L;
        }

        public double skin_depth(double f)
        {
            double Rs, ds;

            Rs = Math.Sqrt(Math.PI * f * 1e9 * MU0 * this.rho); // skin resistance
            ds = this.rho / Rs;                            // skin depth
            return ds;
        }

        public double elecLength(double f)
        {
            double eLength = 0;
            double Lm = this.L * 0.0000254;
            double v = C0 / Math.Sqrt(this.ErEffFreq);
            eLength = 360.0 * Lm / (v / (f * 1e9));

            return eLength;
        }
    }
}

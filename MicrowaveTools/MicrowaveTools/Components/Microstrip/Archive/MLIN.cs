// Microstrip Line
// Reference:
// I.J. Bahl and D.K. Trivedi, "A Designer's Guide to Microstrip Line" MicroWaves, May 1977, pp.174­-182.
// E. Hammerstad and O. Jensen, "Accurate Models for Microstrip Computer-Aided Design",
//                              1980 IEEE MTT-S International Microwave symposium Digest, pp. 407-409.

using System;

namespace MicrowaveTools.Components.Microstrip
{
    public class MLIN
    {
        public double W = 1e-3f;     // Width
        public double L = 10e-3f;    // Length
        public Substrate subst;     // Substrate
        public double Temp = 26.85f; // Temperature
        public double z0 = 50;       // Reference impedance

        public double er;
        public double h;
        public double t;
        public double tand;
        public double rho;
        public double D;
        public double ac, ad;
        public double ZlEff, ErEff, WEff, ZlEffFreq, ErEffFreq;

        public MLIN()
        {
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.D;
        }

        public void calcQuasiStatic()
        {
            double z, e;
            e = er;
            z = z0;
            WEff = W;

            double a, b, du1, du, u, ur, u1, zr, z1;

            u = W / h; // normalized width
            t = t / h; // normalized thickness

            // compute strip thickness effect
            if (t != 0)
            {
                du1 = t / Math.PI * Math.Log(1 + 4 * Math.E / t / Math.Pow((Helper.Helper.Coth(Math.Sqrt(6.517 * u)),2.0)));
            }
            else du1 = 0;
            du = du1 * (1 + Helper.Helper.Sech(Math.Sqrt(er - 1))) / 2;
            u1 = u + du1;
            ur = u + du;
            WEff = ur * h;

            // compute impedances for homogeneous medium
            calcZl(ur, zr);
            calcZl(u1, z1);

            // compute effective dielectric constant
            calcAB(ur, er, a, b);
            calcEr(ur, er, a, b, e);

            // compute final characteristic impedance and dielectric constant
            // including strip thickness effects
            z = zr / Math.Sqrt(e);
            e = e * Math.Pow((z1 / zr),2);

            ZlEff = z;
            ErEff = e;
        }

        public void calcDispersion()
        {
            nr_double_t f, g;
            g = sqr(M_PI) / 12 * (er - 1) / ErEff * sqrt(2 * M_PI * ZlEff / Z0);
            f = 2 * MU0 * h * frequency / ZlEff;
            e = er - (er - ErEff) / (1 + g * sqr(f));
            z = ZlEff * sqrt(ErEff / e) * (e - 1) / (ErEff - 1);

            ZlEffFreq = z;
            ErEffFreq = e;
        }

        public void calcAB(double u, double er, double a, double b)
        {
            a = 1 + log((quadr(u) + sqr(u / 52)) / (quadr(u) + 0.432)) / 49 +
                log(1 + cubic(u / 18.1)) / 18.7;
            b = 0.564 * pow((er - 0.9) / (er + 3), 0.053);
        }

        public void calcEr(double u, double er, double a, double b, double e)
        {
            e = (er + 1) / 2 + (er - 1) / 2 * pow(1 + 10 / u, -a * b);
        }

        public void calcZl(double u, double zl)
        {
            nr_double_t fu = 6 + (2 * M_PI - 6) * exp(-pow(30.666 / u, 0.7528));
            zl = Z0 / 2 / M_PI * log(fu / u + sqrt(1 + sqr(2 / u)));
        }

        public void calcLoss()
        {
            ac = ad = 0;

            // conductor losses
            if (t != 0.0)
            {
                Rs = sqrt(M_PI * frequency * MU0 * rho); // skin resistance
                ds = rho / Rs;                            // skin depth
                                                          // valid for t > 3 * ds
                if (t < 3 * ds)
                {
                    logprint(LOG_ERROR,
                          "WARNING: conductor loss calculation invalid for line "
                
                          "height t (%g) < 3 * skin depth (%g)\n", t, 3 * ds);
                }
                // current distribution factor
                Ki = exp(-1.2 * pow((ZlEff1 + ZlEff2) / 2 / Z0, 0.7));
                // D is RMS surface roughness
                Kr = 1 + M_2_PI * atan(1.4 * sqr(D / ds));
                ac = Rs / (ZlEff1 * W) * Ki * Kr;
            }

            // dielectric losses
            l0 = C0 / frequency;
            ad = M_PI * er / (er - 1) * (ErEff - 1) / sqrt(ErEff) * tand / l0;
        }
    }
    }
}

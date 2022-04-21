// Microstrip Substrate
// Reference:
// I.J. Bahl and D.K. Trivedi, "A Designer's Guide to Microstrip Line" MicroWaves, May 1977, pp.174­-182.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveTools.Components.Microstrip
{
    public class Substrate
    {
        public double er;           // Relative dielectric constant - Range (1, 100)
        public double h;            // Substrate height
        public double t;            // Substrate metal thickness
        public double tand;         // Dielectric loss tangent
        public double rho;          // Metal resistivity
        public double d;            // Surface roughness

        public Substrate()
        {
            er = 9.9;
            h = 25;          // mils
            t = 1.5;         // mils
            tand = 0.0002;
            rho = 1.72e-8 / 0.0000254;   // ohms/mil
            d = 0.055;       // mil
        }

        public Substrate(double Er, double H, double T, double Tand, double Rho, double D)
        {
            er = Er;
            h = H;
            t = T;
            tand = Tand;
            rho = Rho;
            d = D;
        }
    }
}

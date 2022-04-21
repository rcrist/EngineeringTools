using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MicrowaveTools.Helper.Globals;

namespace MicrowaveTools.Components.Microstrip
{
    class TransLine
    {
        protected double f;                 /* Frequency of operation */
        protected double sigma;             /* Conductivity of the metal */
        protected double mur;               /* mag. permeability */
        protected double skindepth;         /* Skin depth */

        public double skin_depth()
        {
            double depth;
            depth = 1.0 / (Math.Sqrt(Math.PI * f * mur * MU0 * sigma));
            return depth;
        }
    }
}

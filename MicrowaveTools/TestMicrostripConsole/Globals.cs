using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMicrostripConsole
{
    public static class Globals
    {
        public const double M_E = 2.7182818284590452353602874713526625;   /* e */
        public const double MU0 = 4*Math.PI*1e-7*0.0000254;              /* magnetic constant         */
        //public const double C0 = 299792458.0;                   /* speed of light in vacuum  */
        public const double C0 = 299792458.0/0.0000254;                   /* mils/sec speed of light in vacuum  */
        public const double ZF0 = 376.73031346958504364963;     /* wave resistance in vacuum */
        public const double Z0 = 50.0;                          /* system impedance, assume 50 ohms */
        public const double z0 = 50.0;
    }
}

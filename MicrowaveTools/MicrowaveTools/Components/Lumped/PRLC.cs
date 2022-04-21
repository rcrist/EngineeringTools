using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Lumped
{
    class PRLC : Comp
    {
        float nH = 1e-9f;
        float pF = 1e-12f;
        float Res = 75.0f;
        float Ind = 5.0f;
        float Cap = 1.0f;

        public PRLC()
        {

        }

        public PRLC(float res, float ind, float cap, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "PRLC";
            Res = res;
            Ind = ind;
            Cap = cap;
            Loc = location;
            Nodes = nodes;
            print();
        }

        // Analysis initializer
        public override void initComp(float f)
        {
            Matrix<Complex32> Yi = Matrix<Complex32>.Build.Dense(2, 2);
            Yi[0, 0] = 1;
            Yi[0, 1] = -1;
            Yi[1, 0] = -1;
            Yi[1, 1] = 1;

            Complex32 Z = (1.0f /(1.0f/Res + 1.0f/((float)(2.0f * Constants.Pi * f * Ind * nH)) +
                1.0f/((float)(-1.0f / (2.0f * Constants.Pi * f * Cap * pF)))));

            Complex32 denom = 1.0f / Z;
            Yi = Yi / denom; // Won't work with a double, must be a float
            Y = Yi;
            N = this.Nodes;
        }
        // Let the SRC draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Create the component labels
            String drawString1 = "R = " + Res + "Ω";
            String drawString2 = "L = " + Ind + "nH";
            String drawString3 = "C = " + Cap + "pF";
 
            if (Orientation == "Series")
            {
                Width = 80;
                Height = 110;
                drawSeriesPRLC(gr, "PRLC", Loc, drawString1, drawString2, drawString3);
            }
            else if (Orientation == "Shunt")
            {
                Width = 110;
                Height = 80;
                drawShuntPRLC(gr, "PRLC", Loc, drawString1, drawString2, drawString3);
            }
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + Type + " R: " + Res + " L: " + Ind + " C: " + Cap + "\t[" + Nodes[0] + ", " + Nodes[1] + "]");
        }
    }
}

using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;


namespace MicrowaveTools.Components.Lumped
{
    class PRL : Comp
    {
        float nH = 1e-9f;
        float pF = 1e-12f;
        float Res = 75.0f;
        float Ind = 5.0f;
        float Cap = 1.0f;

        public PRL()
        {

        }

        public PRL(float res, float ind, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "PRC";
            Res = res;
            Ind = ind;
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

            // parallel Z = 1/(1/Z1 + 1/Z2)
            Complex32 Z = 1.0f / (1.0f / Res + 1.0f / (float)(2.0f * Constants.Pi * f * Ind * nH));

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

            if (Orientation == "Series")
            {
                Width = 80;
                Height = 80;
                drawPxxSeriesLump2(gr, "PRL", Loc, drawString2, drawString1);
            }
            else if (Orientation == "Shunt")
            {
                Width = 80;
                Height = 80;
                drawPxxShuntLump2(gr, "PRL", Loc, drawString1, drawString2);
            }
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + Type + " R: " + Res + " L: " + Ind + "\t[" + Nodes[0] + ", " + Nodes[1] + "]");
        }
    }
}
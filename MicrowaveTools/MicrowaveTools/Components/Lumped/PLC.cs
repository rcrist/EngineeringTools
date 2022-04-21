using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Lumped
{
    class PLC : Comp
    {
        float nH = 1e-9f;
        float pF = 1e-12f;
        float Res = 75.0f;
        float Ind = 5.0f;
        float Cap = 1.0f;

        public PLC()
        {

        }

        public PLC(float ind, float cap, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "PLC";
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

            // parallel Z = 1/(1/Z1 + 1/Z2)
            Complex32 Z = 1.0f / (1.0f / (float)(2.0f * Constants.Pi * f * Ind * nH) 
                        + 1.0f / (float)(-1.0f / (2.0f * Constants.Pi * f * Cap * pF)));

            Complex32 denom = 1.0f / Z;
            Yi = Yi / denom; // Won't work with a double, must be a float
            Y = Yi;
            N = this.Nodes;
        }
        // Let the PLC draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Create the component labels
            String drawString1 = "L = " + Ind + "nH";
            String drawString2 = "C = " + Cap + "pF";

            if (Orientation == "Series")
            {
                Width = 80;
                Height = 80;
                drawPxxSeriesLump2(gr, "PLC", Loc, drawString1, drawString2);
            }
            else if (Orientation == "Shunt")
            {
                Width = 80;
                Height = 80;
                drawPxxShuntLump2(gr, "PLC", Loc, drawString2, drawString1);
            }
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + Type + " L: " + Ind + " C: " + Cap + "\t[" + Nodes[0] + ", " + Nodes[1] + "]");
        }
    }
}
using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Lumped
{
    class SRLC : Comp
    {
        // 3 component bounding box dimensions
        public int compSizeL = 130;
        public int compSizeW = 60;
        public int halfCompSizeL = 65;
        public int halfCompSizeW = 30;

        Pen drawPen = new Pen(Color.White);
        float nH = 1e-9f;
        float pF = 1e-12f;
        float Res = 75.0f;
        float Ind = 5.0f;
        float Cap = 1.0f;

        public SRLC()
        {

        }

        public SRLC(float res, float ind, float cap, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "SRLC";
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

            Complex32 Z = Res + (float)(2.0f * Constants.Pi * f * Ind * nH) +
                (float)(-1.0f / (2.0f * Constants.Pi * f * Cap * pF));

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
                Width = 130;
                Height = 60;
                drawSeriesLump3(gr, "SRLC", Loc, drawString1, drawString2, drawString3);
            }
            else if (Orientation == "Shunt")
            {
                Width = 60;
                Height = 130;
                drawShuntLump3(gr, "PRLC", Loc, drawString1, drawString2, drawString3);
            }
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + Type + " R: " + Res + " L: " + Ind + " C: " + Cap + "\t[" + Nodes[0] + ", " + Nodes[1] + "]");
        }
    }
}
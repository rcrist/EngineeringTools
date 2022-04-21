using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Lumped
{
    class SLC : Comp
    {
        // 2 component bounding box dimensions
        public int compSizeL = 110;
        public int compSizeW = 60;
        public int halfCompSizeL = 55;
        public int halfCompSizeW = 30;

        Pen drawPen = new Pen(Color.White);
        float nH = 1e-9f;
        float pF = 1e-12f;
        float Res = 75.0f;
        float Ind = 5.0f;
        float Cap = 1.0f;

        public SLC()
        {

        }

        public SLC(float ind, float cap, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "SLC";
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

            Complex32 Z = new Complex32(0f, (float)(2.0f * Constants.Pi * f * Ind * nH) + 
                (float)(-1.0f / (2.0f * Constants.Pi * f * Cap * pF)));

            Complex32 denom = 1.0f / Z;
            Yi = Yi / denom; // Won't work with a double, must be a float
            Y = Yi;
            N = this.Nodes;
        }
        // Let the SRC draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Create the component labels
            String drawString1 = "L = " + Ind + "nH";
            String drawString2 = "C = " + Cap + "pF";

            if (Orientation == "Series")
            {
                Width = 110;
                Height = 60;
                drawSxxSeriesLump2(gr, "SLC", Loc, drawString1, drawString2);
            }
            else if (Orientation == "Shunt")
            {
                Width = 60;
                Height = 110;
                drawSxxShuntLump2(gr, "SLC", Loc, drawString1, drawString2);
            }
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + Type + " L: " + Ind + " C: " + Cap + "\t[" + Nodes[0] + ", " + Nodes[1] + "]");
        }
    }
}

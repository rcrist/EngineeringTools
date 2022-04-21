using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Lumped
{
    class Inductor : Comp
    {
        public Inductor()
        {

        }

        public Inductor(float value, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "Ind";
            Value = value;
            Loc = location;
            Nodes = nodes;
            print();
            Pin = Loc;
            Pout = new Point(Loc.X + compL, Loc.Y);
        }

        // Analysis initializer
        public override void initComp(float f)
        {
            Matrix<Complex32> Yind = Matrix<Complex32>.Build.Dense(2, 2);
            Yind[0, 0] = 1;
            Yind[0, 1] = -1;
            Yind[1, 0] = -1;
            Yind[1, 1] = 1;

            Complex32 denom = new Complex32(0, (float)(2 * Constants.Pi * f * this.Value*nH));
            if (denom != 0)
                Yind = Yind / denom; // Won't work with a double, must be a float
            else
                Debug.WriteLine("ERROR: Divide by 0 in initComp(): " + "f: " + f + " Value: " + this.Value);
            Y = Yind;
            N = this.Nodes;
        }

        // Let the Inductor draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Create the component label
            String drawString = "L = " + this.Value + "nH";

            if(Orientation == "Series")
                drawSeriesLump1(gr, "Ind", Loc, drawString);
            else if(Orientation == "Shunt")
                drawShuntLump1(gr, "Ind", Loc, drawString);
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " L: " + this.Value + "\t[" + this.Nodes[0] + "," + this.Nodes[1] + "]");
        }
    }
}

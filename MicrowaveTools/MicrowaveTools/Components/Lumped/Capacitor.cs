using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Lumped
{
    class Capacitor : Comp
    {
        public Capacitor()
        {

        }

        public Capacitor(float value, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "Cap";
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
            Matrix<Complex32> Ycap = Matrix<Complex32>.Build.Dense(2, 2);
            Ycap[0, 0] = 1;
            Ycap[0, 1] = -1;
            Ycap[1, 0] = -1;
            Ycap[1, 1] = 1;

            Complex32 denom = new Complex32(0, (float)(-1 / (2 * Constants.Pi * f * this.Value*pF)));
            Ycap = Ycap / denom; // Won't work with a double, must be a float
            Y = Ycap;
            N = this.Nodes;
        }

        // Let the Capacitor draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Create the component label
            String drawString = "C = " + this.Value + "pF";
            
            if(Orientation == "Series")
                drawSeriesLump1(gr, "Cap", Loc, drawString);
            else if(Orientation == "Shunt")
                drawShuntLump1(gr, "Cap", Loc, drawString);
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " C: " + this.Value + "\t[" +
                                       this.Nodes[0] + "," + this.Nodes[1] + "]");
        }
    }
}

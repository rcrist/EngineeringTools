using System;
using System.Diagnostics;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Lumped
{
    class Resistor : Comp
    {
        public Resistor()
        {

        }

        public Resistor(float value, Point location, int[] nodes)
        {
            Orientation = "Series";
            Type = "Res";
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
            Matrix<Complex32> Yres = Matrix<Complex32>.Build.Dense(2, 2);
            Yres[0, 0] = 1;
            Yres[0, 1] = -1;
            Yres[1, 0] = -1;
            Yres[1, 1] = 1;

            Yres = Yres / this.Value; // Won't work with a double, must be a float
            Y = Yres;
            N = this.Nodes;
        }

        // Let the Resistor draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Create the component label
            String drawString = "R = " + this.Value + "Ω";

            if(Orientation == "Series")
                drawSeriesLump1(gr, "Res", Loc, drawString);
            else if(Orientation == "Shunt")
                drawShuntLump1(gr, "Res", Loc, drawString);
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + " R: " + this.Value + "\t[" + this.Nodes[0] + ", " + this.Nodes[1] + "]");
        }
    }
}

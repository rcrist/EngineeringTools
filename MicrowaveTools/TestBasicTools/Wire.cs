using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBasicTools
{
    public class Wire : Comp
    {
        Pen drawPen = new Pen(Color.White);

        // Create wire start and end points
        public Point Pt1 = new Point();
        public Point Pt2 = new Point();

        // Add components connected to the input and output of the wire
        public Comp Cin = new Comp();
        public Comp Cout = new Comp();

        public Wire()
        {

        }

        public Wire(Comp cin, Comp cout)
        {
            Cin = cin;
            Cout = cout;

            Pt1 = new Point(cin.Location.X + compSize, cin.Location.Y + halfCompSize);
            Pt2 = new Point(cout.Location.X, cout.Location.Y + halfCompSize);
            cin.wires.Add(this); // Add wire to input component wire list
        }

        public override void Draw(Graphics gr)
        {
            // Draw straight wire
            gr.DrawLine(drawPen, Pt1, Pt2);

        }
    }
}

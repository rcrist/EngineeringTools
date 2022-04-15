using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveTools.Components.Ideal
{
    public class Ground : Comp
    {
        public Ground()
        {
            initComp();
            Orientation = "Series";
        }

        public Ground(String orientation)
        {
            initComp();
            Orientation = orientation;
        }

        private void initComp()
        {
            Type = "Gnd";
            Name = "Gnd";
            Loc = new Point(200, 300);
        }

        // Let the Ground draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Define the points
            Point p1 = Loc;
            Point p2 = new Point(p1.X, p1.Y + leadL);
            Point p3 = new Point(p2.X - 10, p2.Y);
            Point p4 = new Point(p2.X + 10, p2.Y);
            Point p5 = new Point(p3.X + 5, p3.Y + 5);
            Point p6 = new Point(p5.X + 10, p5.Y);
            Point p7 = new Point(p5.X + 3, p5.Y + 5);
            Point p8 = new Point(p7.X + 4, p7.Y);

            // Draw the input lead
            gr.DrawLine(drawPen, p1, p2);
            gr.DrawLine(drawPen, p3, p4);
            gr.DrawLine(drawPen, p5, p6);
            gr.DrawLine(drawPen, p7, p8);
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type);
        }
    }
}

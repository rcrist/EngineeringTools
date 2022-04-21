using System;
using System.Collections.Generic;
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
            Loc = new Point(200, 600);
        }

        // Let the Ground draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
                drawSeriesGnd(gr, Loc);
            else if (Orientation == "Shunt")
                drawShuntGnd(gr, Loc);
        }
    }
}

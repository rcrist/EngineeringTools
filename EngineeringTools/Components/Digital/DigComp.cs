using System;
using System.Collections.Generic;
using System.Drawing;

namespace EngineeringTools.Components.Digital
{
    public class DigComp : Comp
    {
        // Define the colors for logic states on (1) and off (0)
        protected Pen onPen = new Pen(Color.Green);  // Color for logic 1
        protected Pen offPen = new Pen(Color.White); // Color for logic 0

        // Logical model input and output pins
        public bool[] Pin = { false, false };
        public bool Pout = false;

        public DigComp()
        {
            loc = new Point(200, 300);
            width = 60;
            height = 40;
        }

        public virtual void setLogicState()
        {
            // Do nothing
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;

using TestDigitalSimulator.Wires;

namespace TestDigitalSimulator.Components
{
    public class Comp
    {
        public List<Wire> wires = new List<Wire>(); // Create a LIST of output wires

        protected Pen pen = new Pen(Color.Black);
        // Define the colors for logic states on (1) and off (0)
        protected Pen onPen = new Pen(Color.Green);  // Color for logic 1
        protected Pen offPen = new Pen(Color.Black); // Color for logic 0

        // Logical model input and output pins
        public bool[] Pin = { false, false };
        public bool Pout = false;
        public int[] Nin = null;
        public int[] Nout = null;

        // Component location, width, and height attributes
        public Point loc;
        public int width;
        public int height;

        // Traverse variables
        public bool Visited = false;

        // Print variables
        public string Type = null;
        public string Name = null;
        public double Value = 0.0;

        public Comp()
        {
            loc = new Point(100, 100);
            width = 60;
            height = 40;
        }

        public virtual void Draw(Graphics gr)
        {
            // Draw a simple rectangle with black border and no fill color
            gr.DrawRectangle(pen, loc.X, loc.Y, width, height);
        }

        public virtual void setLogicState()
        {
            // Do nothing
        }
    }
}

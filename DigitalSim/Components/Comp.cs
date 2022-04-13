// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

// Digital Simulator Libraries
using DigitalSim.Wires;

namespace DigitalSim.Components
{
    public class Comp
    {
        public List<Wire> wires = new List<Wire>(); // Create a LIST of output wires
        public bool[] Pin = null;
        public bool[] Pout = null;

        // Public variables
        public string Name;
        public string Text;
        public int Index;
        public PointF Location;

        // protected variables
        protected const int leadLength = 10;
        protected const int gateSize = 60;

        // Properties used by algorithms.
        public bool Visited = false;

        // public variables
        // public Point Pt = new Point(); // Changed to Location
        public Pen offPen = new Pen(Color.Black, 2);
        public Pen onPen = new Pen(Color.Green, 2);
        public int Width = 60;
        public int Height = 60;
        public bool logicState = false;
        public bool stateChanged = false;

        public virtual void SetOutput()
        {
            // Do nothing
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using DigitalSimulator.Circuits;
using DigitalSimulator.Components;
using DigitalSimulator.Wires;

namespace DigitalSimulator.Components
{
    public class DigitalComponent
    {
        // Public variables
        public string Name;
        public string Text;
        public int Index;
        public PointF Location;
        public List<Wire> wires = new List<Wire>();

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

        public DigitalComponent()
        {

        }
    }
}

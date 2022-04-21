using System;
using System.Collections.Generic;
using TestWire.Components;

namespace TestWire.Schematics
{
    public class Schematic
    {
        public List<Wire> wires = new List<Wire>();
        public List<Node> nodes = new List<Node>();

        public Schematic()
        {

        }
    }
}
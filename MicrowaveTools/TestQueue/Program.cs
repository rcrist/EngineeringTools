using System;
using System.Collections.Generic;
using System.Drawing;

namespace TestQueue
{
    public class Circuit
    {
        public List<Comp> comps = new List<Comp>();
        public List<Comp> netlist = new List<Comp>();

        public Circuit()
        {
            // Create components
            InPort inport = new InPort();
            Resistor R1 = new Resistor();
            Inductor L1 = new Inductor();
            Capacitor C1 = new Capacitor();
            OutPort outport = new OutPort();

            // Create wires
            Wire w1 = new Wire(inport, 1, R1, 1);
            Wire w2 = new Wire(R1, 1, L1, 1);
            Wire w3 = new Wire(L1, 1, C1, 1);
            Wire w4 = new Wire(C1, 1, outport, 1);

            // Add wires to components
            inport.wires.Add(w1);
            R1.wires.Add(w2);
            L1.wires.Add(w3);
            C1.wires.Add(w4);

            // Add components to circuit
            this.comps.Add(inport);
            this.comps.Add(R1);
            this.comps.Add(L1);
            this.comps.Add(C1);
            this.comps.Add(outport);

            // Traverse the circuit to create a netlist
            netlist = TraverseCircuit(inport);

            // Print the netlist
            foreach(Comp comp in netlist)
            {
                if (comp.Type == "InPort")
                    Console.WriteLine(comp.Type + " " + comp.Name + " [" + comp.Nout[0] + "]");
                else if (comp.Type == "OutPort")
                    Console.WriteLine(comp.Type + " " + comp.Name + " [" + comp.Nin[0] + "]");
                else
                    Console.WriteLine(comp.Type + " " + comp.Name + " " + comp.Value + " [" + comp.Nin[0] + "," + comp.Nout[0] + "]");
            }
            Console.WriteLine("Hit any key to continue...");
            Console.ReadLine();
        }

        public List<Comp> TraverseCircuit(Comp startNode)
        {
            int nodeIndex = 0;
            // Add the start node to the queue.
            Queue<Comp> queue = new Queue<Comp>();
            queue.Enqueue(startNode);

            // Visit the start node.
            List<Comp> netlist = new List<Comp>();
            netlist.Add(startNode);
            startNode.Visited = true;

            // Process the queue until it's empty.
            while (queue.Count > 0)
            {
                // Get the next node from the queue.
                Comp node = queue.Dequeue();

                // Process the node's links.
                foreach (Wire link in node.wires)
                {
                    Comp fromNode = link.inComp;
                    Comp toNode = link.outComp;

                    // Create virtual nodes
                    if (fromNode.Type != "Node" && toNode.Type != "Node")
                    {
                        nodeIndex++;

                        fromNode.Nout[link.inCompPout - 1] = nodeIndex;
                        toNode.Nin[link.outCompPin - 1] = nodeIndex;
                    }

                    // Only use the link if the destination
                    // node hasn't been visited.
                    if (!toNode.Visited)
                    {
                        // Mark the node as visited.
                        toNode.Visited = true;

                        // Add the node to the netlist.
                        netlist.Add(toNode);

                        // Add the link to the traversal.
                        link.Visited = true;

                        // Add the node onto the queue.
                        queue.Enqueue(toNode);
                    }
                }
            }
            return netlist;
        }
    }

    public class Comp
    {
        public List<Wire> wires = new List<Wire>(); // Create a LIST of output wires
        public int[] Pin = null;
        public int[] Pout = null;
        public int[] Nin = null;
        public int[] Nout = null;
        public Point Location;
        public bool Visited = false;

        // Print variables
        public string Type = null;
        public string Name = null;
        public double Value = 0.0;
    }

    public class Resistor : Comp
    {
        public Resistor()
        {
            Pin = new int[1]; // Add one intput port
            Pout = new int[1]; // Add one output port
            Nin = new int[1]; // Add one input node
            Nout = new int[1]; // Add one output node
            Nin[0] = 0;
            Nout[0] = 0;
            Type = "Res";
            Name = "R1";
            Value = 75.0;
        }
    }

    public class Inductor : Comp
    {
        public Inductor()
        {
            Pin = new int[1]; // Add one intput port
            Pout = new int[1]; // Add one output port
            Nin = new int[1]; // Add one input node
            Nout = new int[1]; // Add one output node
            Nin[0] = 0;
            Nout[0] = 0;
            Type = "Ind";
            Name = "L1";
            Value = 5.0;
        }
    }

    public class Capacitor : Comp
    {
        public Capacitor()
        {
            Pin = new int[1]; // Add one intput port
            Pout = new int[1]; // Add one output port
            Nin = new int[1]; // Add one input node
            Nout = new int[1]; // Add one output node
            Nin[0] = 0;
            Nout[0] = 0;
            Type = "Cap";
            Name = "C1";
            Value = 1.0;
        }
    }

    public class InPort : Comp
    {
        public InPort()
        {
            Pout = new int[1]; // Add one output port
            Nout = new int[1];
            Nout[0] = 0;
            Type = "InPort";
            Name = "In1";
        }
    }

    public class OutPort : Comp
    {
        public OutPort()
        {
            Pin = new int[1]; // Add one input port
            Nin = new int[1];
            Nin[0] = 0;
            Type = "OutPort";
            Name = "Out1";
        }
    }

    public class Wire
    {
        public Comp inComp = new Comp();    // Variable to store the component on the input side of the wire
        public int inCompPout;              // Variable to store Pout of the component on the imput of wire
        public Comp outComp = new Comp();   // Variable to store the component on the output side of the wire
        public int outCompPin;              // Variable to store Pin of the component on the output of wire
        public bool Visited = false;

        public PointF Pt1 = new PointF();
        public PointF Pt2 = new PointF();

        public Wire(Comp incomp, int incomppout, Comp outcomp, int outcomppin)
        {
            inComp = incomp;
            inCompPout = incomppout; // Index to Pout pin number
            outComp = outcomp;
            outCompPin = outcomppin; // Index to Pin pin number
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Circuit RLCckt = new Circuit();
        }
    }
}

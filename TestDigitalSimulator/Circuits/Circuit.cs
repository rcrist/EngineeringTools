using System;
using System.Collections.Generic;

using TestDigitalSimulator.Components;
using TestDigitalSimulator.Wires;

namespace TestDigitalSimulator.Circuits
{
    public class Circuit
    {
        public List<Comp> comps = new List<Comp>();
        Comp fromNode = null;
        Comp toNode = null;
        Wire link = null;
        Queue<Comp> queue = null;
        List<Comp> netlist = null;

        //// Recursive algorithm to transverse the circuit and process all components
        //public void Traverse(Comp comp)
        //{
        //    // Traverse the circuit starting from comp and toggle logic states
        //    foreach (Wire wire in comp.wires)
        //    {
        //        wire.logicState = comp.Pout[0];
        //        wire.printWire();
        //        Comp tempComp = new Comp();
        //        tempComp = wire.outComp;
        //        tempComp.Pin[wire.outCompPin] = wire.logicState;
        //        tempComp.SetOutput();
        //        Traverse(tempComp); // Recursion over the wire output components
        //    }
        //}

        // Queue algorithm to transverse the circuit and process all components
        public List<Comp> DigitalTraverse(Comp startNode)
        {
            int nodeIndex = 0;

            // Add the start node to the queue.
            queue = new Queue<Comp>();
            queue.Enqueue(startNode);

            // Visit the start node.
            netlist = new List<Comp>();
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
                    fromNode = link.inComp;
                    toNode = link.outComp;

                    createVirtualNode(ref nodeIndex);
                    visitNode();
                }
            }
            return netlist;
        }

        private void createVirtualNode(ref int nodeIndex)
        {
            // Create virtual nodes
            if (fromNode.Type != "Node" && toNode.Type != "Node")
            {
                nodeIndex++;

                fromNode.Nout[link.inCompPout - 1] = nodeIndex;
                toNode.Nin[link.outCompPin - 1] = nodeIndex;
            }
        }

        private void visitNode()
        {
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
}
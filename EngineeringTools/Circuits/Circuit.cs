using EngineeringTools.Components;
using EngineeringTools.Components.Digital;
using EngineeringTools.Wires;
using System;
using System.Collections.Generic;

namespace EngineeringTools.Circuits
{
    public class Circuit
    {
        public List<Comp> comps = new List<Comp>();

        // Recursive algorithm to transverse the circuit and process all components
        //public void Traverse(DigComp comp)
        //{
        //    // Traverse the circuit starting from comp and toggle logic states
        //    foreach (Wire wire in comp.wires)
        //    {
        //        wire.logicState = comp.Pout;
        //        DigComp tempComp = new DigComp();
        //        tempComp = wire.outComp;
        //        tempComp.Pin[wire.outCompPin] = wire.logicState;
        //        tempComp.setLogicState();
        //        Traverse(tempComp); // Recursion over the wire output components
        //    }
        //}

        // Queue algorithm to transverse the circuit and process all components
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
}
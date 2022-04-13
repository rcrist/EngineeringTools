using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using DigitalSimulator.Circuits;
using DigitalSimulator.Components;
using DigitalSimulator.Wires;

namespace DigitalSimulator.Circuits
{
    class Circuit
    {
        public List<Wire> wires = new List<Wire>();
        public List<DigitalComponent> components = new List<DigitalComponent>();

        public Circuit()
        {

        }

        // Save the network into the file.
        public void SaveCircuit(string filename)
        {
            // Open the file.
            StreamWriter writer = File.CreateText(filename);

            // Save the number of nodes.
            writer.WriteLine(components.Count);

            // Renumber the nodes.
            for (int i = 0; i < components.Count; i++) components[i].Index = i;

            // Save the node information.
            foreach (DigitalComponent node in components)
            {
                // Save this node's information.
                writer.Write(node.Name + "," +
                    node.Location.X + "," + node.Location.Y);

                // Save information about this node's links.
                foreach (Wire link in node.wires)
                {
                    DigitalComponent otherNode;
                    if (link.Nodes[0] == node) otherNode = link.Nodes[1];
                    else otherNode = link.Nodes[0];
                    writer.Write("," + otherNode.Index);
                }
                writer.WriteLine();
            }

            // Close the file.
            writer.Close();
        }

        // Craete a circuit from a circuit file.
        public static Circuit LoadCircuit(string filename)
        {
            // Make a new network.
            Circuit circuit = new Circuit();

            // Read the data.
            string[] allLines = File.ReadAllLines(filename);

            // Get the number of nodes.
            int numNodes = int.Parse(allLines[0]);

            // Create the nodes.
            for (int i = 0; i < numNodes; i++)
            {
                circuit.components.Add(new DigitalComponent());
                circuit.components[i].Index = i;
            }

            // Read the nodes.
            char[] separators = { ',' };
            for (int i = 1; i < allLines.Length; i++)
            {
                DigitalComponent node = circuit.components[i - 1];
                string[] nodeFields = allLines[i].Split(separators);

                // Get the node's text and coordinates.
                node.Name = nodeFields[0];
                node.Location = new PointF(
                    int.Parse(nodeFields[1]),
                    int.Parse(nodeFields[2])
                );

                // Get the node's links.
                for (int j = 3; j < nodeFields.Length; j += 3)
                {
                    // Get the next link.
                    Wire link = new Wire();
                    link.Nodes[0] = node;
                    int index = int.Parse(nodeFields[j]);
                    link.Nodes[1] = circuit.components[index];
                    node.wires.Add(link);
                }
            }

            return circuit;
        }

        public void Traverse(DigitalComponent startNode)
        {
            // Keep track of the number of nodes in the traversal.
            int numDone = 0;

            // Add the start node to the queue.
            Queue<DigitalComponent> queue = new Queue<DigitalComponent>();
            queue.Enqueue(startNode);

            // Visit the start node.
            //List<DigitalComponent> traversal = new List<DigitalComponent>();
            //traversal.Add(startNode);
            startNode.logicState = !startNode.logicState;
            startNode.Visited = true;
            startNode.Text = numDone.ToString();
            Debug.WriteLine("Start Node: " + startNode.ToString());
            numDone++;

            // Process the queue until it's empty.
            while (queue.Count > 0)
            {
                // Get the next node from the queue.
                DigitalComponent node = queue.Dequeue();
                Debug.WriteLine("Wires Count: " + node.wires.Count);

                // Process the node's links.
                foreach (Wire link in node.wires)
                {
                    Debug.WriteLine("Link found");
                    DigitalComponent toNode = link.Nodes[1];
                    link.logicState = !link.logicState;

                    // Only use the link if the destination
                    // node hasn't been visited.
                    if (!toNode.Visited)
                    {
                        // Mark the node as visited.
                        toNode.Visited = true;
                        toNode.Text = numDone.ToString();
                        Debug.WriteLine("To Node: " + toNode.Text);
                        numDone++;

                        // Add the node to the traversal.
                        //traversal.Add(toNode);
                        toNode.logicState = !toNode.logicState;

                        // Add the link to the traversal.
                        link.Visited = true;

                        // Add the node onto the queue.
                        queue.Enqueue(toNode);
                    }
                }             
            }
        }
    }
}

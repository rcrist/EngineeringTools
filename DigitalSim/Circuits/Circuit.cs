using DigitalSim.Components;
using DigitalSim.Wires;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace DigitalSim.Circuits
{
    class Circuit
    {
        public List<Comp> comps = new List<Comp>();

        // Recursive algorithm to transverse the circuit and process all components
        public void Traverse(Comp comp)
        {
            // Traverse the circuit starting from comp and toggle logic states
            foreach (Wire wire in comp.wires)
            {
                wire.logicState = comp.Pout[0];
                wire.printWire();
                Comp tempComp = new Comp();
                tempComp = wire.outComp;
                tempComp.Pin[wire.outCompPin] = wire.logicState;
                tempComp.SetOutput();
                Traverse(tempComp); // Recursion over the wire output components
            }
        }

        public void SaveCircuit(string filename)
        {
            // Open the file.
            StreamWriter writer = File.CreateText(filename);

            // Save the number of nodes.
            writer.WriteLine(comps.Count);

            // Renumber the nodes.
            for (int i = 0; i < comps.Count; i++) comps[i].Index = i;

            // Save the node information.
            foreach (Comp comp in comps)
            {
                // Save this node's information.
                writer.Write(comp.Name + "," +
                    comp.Location.X + "," + comp.Location.Y);
                writer.WriteLine();

                // Save information about this component's wires.
                foreach (Wire wire in comp.wires)
                {
                    Comp otherComp;
                    if (wire.inComp == comp) otherComp = wire.outComp;
                    else otherComp = wire.inComp;
                    writer.Write("Wire," + wire.Pt1.X + "," + wire.Pt1.Y + "," +
                                            wire.Pt2.X + "," + wire.Pt2.Y + "," +
                                            comp.Index + "," + otherComp.Index);
                }
                writer.WriteLine();
            }

            // Close the file.
            writer.Close();
        }

        public static Circuit LoadCircuit(string filename, Pen lightPen)
        {
            // Make a new network.
            Circuit schematic = new Circuit();

            // Read the data.
            string[] allLines = File.ReadAllLines(filename);

            // Get the number of components.
            int numComps = int.Parse(allLines[0]);

            // Read the components into the schematic comps list
            char[] separators = { ',' };
            for (int i = 1; i < allLines.Length; i++)
            {
                //Comp comp = schematic.comps[i - 1];
                string[] nodeFields = allLines[i].Split(separators);

                if (nodeFields[0] == "Switch")
                {
                    Components.Switch sw = new Components.Switch(lightPen);
                    GetNameLoc(sw, nodeFields);
                    schematic.comps.Add(sw);
                }
                else if (nodeFields[0] == "LED")
                {
                    LED led = new LED(lightPen, Color.White);
                    GetNameLoc(led, nodeFields);
                    schematic.comps.Add(led);
                }
                else if (nodeFields[0] == "AND")
                {
                    AND and = new AND(lightPen);
                    GetNameLoc(and, nodeFields);
                    schematic.comps.Add(and);
                }
                else if (nodeFields[0] == "OR")
                {
                    OR or = new OR(lightPen);
                    GetNameLoc(or, nodeFields);
                    schematic.comps.Add(or);
                }
                else if (nodeFields[0] == "NOT")
                {
                    NOT not = new NOT(lightPen);
                    GetNameLoc(not, nodeFields);
                    schematic.comps.Add(not);
                }
            }

            // Go through the file again and create the wires
            for (int i = 1; i < allLines.Length; i++)
            {
                string[] nodeFields = allLines[i].Split(separators);

                if (nodeFields[0] == "Wire")
                { 
                    // Get the next wire.                  
                    Wire wire = new Wire(lightPen);
                    wire.Pt1 = new PointF(
                        int.Parse(nodeFields[1]),
                        int.Parse(nodeFields[2])
                    );
                    wire.Pt2 = new PointF(
                       int.Parse(nodeFields[3]),
                       int.Parse(nodeFields[4])
                   );

                    // Setup the wire connection components
                    int index = int.Parse(nodeFields[5]);
                    wire.inComp = schematic.comps[index];
                    Comp comp = schematic.comps[index];
                    index = int.Parse(nodeFields[6]);
                    wire.outComp = schematic.comps[index];
                    comp.wires.Add(wire);
                }
            }

            return schematic;
        }

        private static void GetNameLoc(Comp comp, string[] nodeFields)
        {
            comp.Name = nodeFields[0];
            comp.Location = new PointF(
                int.Parse(nodeFields[1]),
                int.Parse(nodeFields[2])
            );
        }
    }
}
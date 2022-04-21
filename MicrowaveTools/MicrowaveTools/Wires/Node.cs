using MicrowaveTools.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MicrowaveTools.Wires
{
    class Node : Comp
    {
        Pen drawPen = new Pen(Color.White);
        int radius = 5;

        public Node()
        {
            this.Loc = new Point(300, 200);
        }

        public Node(float value, Point location, int[] nodes)
        {
            Type = "Node";
            Value = value;
            Loc = location;
            Nodes = nodes;
            print();
        }

        // Let the Node draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            //// Draw filled ellipse (circle)
            //SolidBrush myBrush = new SolidBrush(Color.White);
            //gr.FillEllipse(myBrush, new Rectangle(Location.X-radius, Location.Y-radius, 2*radius, 2 * radius));
        }

        public override void print()
        {
            Debug.WriteLine("Type: " + this.Type + "[" + this.Nodes[0] + "]");
        }
    }
}

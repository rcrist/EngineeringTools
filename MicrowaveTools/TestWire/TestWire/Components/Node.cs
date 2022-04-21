using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Drawing2D;


namespace TestWire.Components
{
    public class Node
    {
        private PointF centerPt;
        public PointF CenterPt { get; set; }

        public Brush brush = Brushes.Black;

        public Node(PointF centerpt)
        {
            this.CenterPt = centerpt;
            Debug.WriteLine($@"Node created at ({this.CenterPt.X}, {this.CenterPt.Y})");
        }

        // Let the node draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            DrawPoint(gr, this.CenterPt, Brushes.Black);
        }

        // Draw a point.
        private void DrawPoint(Graphics gr, PointF pt, Brush brush)
        {
            const int RADIUS = 4;
            gr.FillEllipse(brush,
                pt.X - RADIUS, pt.Y - RADIUS,
                2 * RADIUS, 2 * RADIUS);
        }

        public void printNode()
        {
            Debug.WriteLine("Node Pt1: " + CenterPt.ToString() + " Brush: Brushes.Black");
        }
    }
}

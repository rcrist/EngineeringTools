using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TestWire.Components;
using TestWire.Schematics;
using System.Diagnostics;

namespace TestWire
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Wire.lineShapeState = Wire.LineShape.Straight;
        }

        // The grid spacing.
        public const int grid_gap = 10;

        // Create a custom schematic
        private Schematic schematic = new Schematic();

        // The wire we are drawing
        private Wire NewWire = null;

        // Points for the new line.
        private bool IsDrawing = false;
        private Point NewPt1, NewPt2;

        /* **************************** Mouse Event Handlers **************************** */
        // Mouse DOWN event handler
        private void schematicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            // Set the drawing flag and capture the start point, make the end point the same as the start point
            IsDrawing = true;

            // Snap the start point to the Grid
            int x = e.X;
            int y = e.Y;
            SnapToGrid(ref x, ref y);
            NewPt1 = new Point(x, y);
            NewPt2 = new Point(x, y);

            // Create a new wire and add it to the schematic wires list
            NewWire = new Wire(Pens.Blue); // Use the constructor with no parameters
            NewWire.Pt1 = NewPt1;
            NewWire.Pt2 = NewPt2;
            NewWire.endcapsVisible = true;

            // Check for wire intersection at start point
            foreach (Wire wire in schematic.wires)
            {
                if (wire.hitTest(wire, NewPt1) && wire != NewWire)
                {
                    Node node = new Node(NewPt1);
                    schematic.nodes.Add(node);
                    schematicCanvas.Refresh();
                }
            }

            schematic.wires.Add(NewWire);

            // Debugs to show the line points on the console
            Debug.WriteLine("Line Start Points: (" + NewPt1.X + ", " + NewPt1.Y + " )");
        }

        // Mouse MOVE event handler
        private void schematicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (NewWire == null) return;
            // Snap the end point to the Grid
            int x = e.X;
            int y = e.Y;
            SnapToGrid(ref x, ref y);
            SnapToGrid(ref x, ref y);
            NewPt2 = new Point(x, y);
            NewWire.Pt2 = NewPt2;

            // Redraw, this creates the "rubber band" effect while drawing the wire
            schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox
        }

        // Mouse UP event handler
        private void schematicCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            // Check for wire intersection at end point
            foreach (Wire wire in schematic.wires)
            {
                if (wire.hitTest(wire, NewPt2) && wire != NewWire)
                {
                    Node node = new Node(NewPt2);
                    schematic.nodes.Add(node);
                    schematicCanvas.Refresh();
                }
            }

            // Update the new wire end points
            NewWire.Pt1 = NewPt1;
            NewWire.Pt2 = NewPt2;
            NewWire.endcapsVisible = false;

            // Reset the drawing flags
            IsDrawing = false;

            // Terminate any further updates to the new wire, this makes it permanent in the schematic wires list
            NewWire = null;

            // Redraw.
            schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox

 

            // Debugs to show the line points on the console
            Debug.WriteLine("Line End Points: (" + NewPt2.X + ", " + NewPt2.Y + " )");
        }

        /* **************************** Paint Event Handler **************************** */
        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Iterate over the schematic wire list and draw each wire
            foreach (Wire wire in schematic.wires)
            {
                // Draw the Wire on the schematic using the Wire class draw method
                wire.Draw(e.Graphics);
            }

            // Iterate over the node list and draw each node
            foreach (Node node in schematic.nodes)
            {
                // Draw the Node on the schematic using the Node class draw method
                node.Draw(e.Graphics);
            }
        }

        /* **************************** Helper Methods **************************** */
        private void SnapToGrid(ref int x, ref int y)
        {
            //if (!chkSnapToGrid.Checked) return;
            x = grid_gap * (int)Math.Round((double)x / grid_gap);
            y = grid_gap * (int)Math.Round((double)y / grid_gap);
        }

        private void schematicCanvas_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

        private void radioButtonStraignt_CheckedChanged(object sender, EventArgs e)
        {
            Wire.lineShapeState = Wire.LineShape.Straight;
        }

        private void radioButtonRectilinear_CheckedChanged(object sender, EventArgs e)
        {
            Wire.lineShapeState = Wire.LineShape.Rectilinear;
        }

        private void radioButtonSpline_CheckedChanged(object sender, EventArgs e)
        {
            Wire.lineShapeState = Wire.LineShape.Spline;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            foreach (Wire wire in schematic.wires)
            {
                wire.printWire();
            }
            foreach (Node node in schematic.nodes)
            {
                node.printNode();
            }
        }

        private void btnNode_Click(object sender, EventArgs e)
        {
            Node node = new Node(new PointF(200,200));
            schematic.nodes.Add(node);
            schematicCanvas.Refresh();
        }

        // Draw the background grid as a Bitmap
        private void DrawBackgroundGrid()
        {
                Bitmap bm = new Bitmap(
                    schematicCanvas.ClientSize.Width,
                    schematicCanvas.ClientSize.Height);
                for (int x = 0; x < schematicCanvas.ClientSize.Width; x += grid_gap)
                {
                    for (int y = 0; y < schematicCanvas.ClientSize.Height; y += grid_gap)
                    {
                        bm.SetPixel(x, y, Color.Black);
                    }
                }

            schematicCanvas.BackgroundImage = bm;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DigitalSimulator.Circuits;
using DigitalSimulator.Components;
using DigitalSimulator.Wires;

namespace DigitalSimulator
{
    public partial class MainForm : Form
    {
        // Create a custom schematic
        private Circuit schematic = new Circuit();

        // The grid spacing.
        public const int grid_gap = 10;

        // Mouse event variables
        bool isMouseDown = false;
        float offsetX, offsetY, startX, startY;
        DigitalComponent tempComp = new DigitalComponent();
        bool lineDrawing = false;

        // The wire we are drawing
        private Wire NewWire = null;

        // Points for the new line.
        private PointF NewPt1, NewPt2;

        // Mode for user menu selection
        private enum Modes
        {
            None,
            AND,
            OR,
            NOT,
            Switch,
            LED,
            Rect,
            Wire,
        }
        private Modes Mode = Modes.None;

        public MainForm()
        {
            InitializeComponent();
            //Components.Switch sw = new Components.Switch();
            //schematic.components.Add(sw);
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

        /* **************************** Helper Methods **************************** */
        private void SnapToGrid(ref int x, ref int y)
        {
            //if (!chkSnapToGrid.Checked) return;
            x = grid_gap * (int)Math.Round((double)x / grid_gap);
            y = grid_gap * (int)Math.Round((double)y / grid_gap);
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Iterate over the schematic component list and draw each component
            foreach (DigitalComponent comp in schematic.components)
            {
                //if (comp is AndGate)
                //{
                //    AndGate tempAndGate = comp as AndGate;
                //    tempAndGate.Draw(e.Graphics);
                //}

                //if (comp is OrGate)
                //{
                //    OrGate tempOrGate = comp as OrGate;
                //    tempOrGate.Draw(e.Graphics);
                //}

                //if (comp is Inverter)
                //{
                //    Inverter tempInvGate = comp as Inverter;
                //    tempInvGate.Draw(e.Graphics);
                //}

                if (comp is LED)
                {
                    LED tempLED = comp as LED;
                    tempLED.Draw(e.Graphics);
                }

                if (comp is Components.Switch)
                {
                    Components.Switch tempSw = comp as Components.Switch;
                    tempSw.Draw(e.Graphics);
                }
            }

            // Iterate over the schematic wire list and draw each wire
            foreach (Wire wire in schematic.wires)
            {
                // Draw the Wire on the schematic using the Wire class draw method
                wire.Draw(e.Graphics);
            }
        }

 

        /* **************************** Mouse Event Handlers **************************** */
        private void schematicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        isMouseDown = true;

                        if (!lineDrawing)
                        {// Snap the start point to the Grid
                            int x = e.X;
                            int y = e.Y;
                            SnapToGrid(ref x, ref y);
                            startX = x;
                            startY = y;

                            // Iterate over the schematic component list and draw each component
                            foreach (DigitalComponent comp in schematic.components)
                            {
                                if (hitTest(comp))
                                {
                                    tempComp = comp;
                                    offsetX = startX - comp.Location.X;
                                    offsetY = startY - comp.Location.Y;
                                }
                            }
                        }
                        if (lineDrawing)
                        {
                            // Snap the start point to the Grid
                            int x = e.X;
                            int y = e.Y;
                            SnapToGrid(ref x, ref y);
                            NewPt1 = new PointF(x, y);
                            NewPt2 = new PointF(x, y);
                            startX = x;
                            startY = y;

                            // Create a new wire and add it to the schematic wires list
                            NewWire = new Wire(); // Use the constructor with no parameters
                            NewWire.Pt1 = NewPt1;
                            NewWire.Pt2 = NewPt2;
                            NewWire.endcapsVisible = true;
                            schematic.wires.Add(NewWire);

                            // Debugs to show the line points on the console
                            Debug.WriteLine("Line Start Points: (" + NewPt1.X + ", " + NewPt1.Y + " )");

                            // Iterate over the schematic component list and draw each component
                            foreach (DigitalComponent comp in schematic.components)
                            {
                                if (hitTest(comp))
                                {
                                    Debug.WriteLine("Start Component hit!" + comp.ToString());
                                    NewWire.Nodes[0] = comp;
                                }
                            }
                        }
                        break;
                    }
                case MouseButtons.Right:
                    {
                        foreach (DigitalComponent comp in schematic.components)
                        {
                            if (comp is Components.Switch)
                            {
                                startX = e.X;
                                startY = e.Y;

                                if (hitTest(comp))
                                {
                                    Components.Switch tempSw = comp as Components.Switch;
                                    tempSw.logicStateChanged += tempSw_StateChanged;
                                    tempSw.ChangeState();
                                    schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox
                                }
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void schematicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (!lineDrawing)
                {
                    if (tempComp != null)
                    {
                        // Snap the end point to the Grid
                        int x = e.X;
                        int y = e.Y;
                        SnapToGrid(ref x, ref y);
                        SnapToGrid(ref x, ref y);
                        tempComp.Location.X = x - offsetX;
                        tempComp.Location.Y = y - offsetY;
                        schematicCanvas.Invalidate();
                    }
                }
                if (lineDrawing)
                {
                    if (NewWire == null) return;
                    // Snap the end point to the Grid
                    int x = e.X;
                    int y = e.Y;
                    SnapToGrid(ref x, ref y);
                    SnapToGrid(ref x, ref y);
                    NewPt2 = new PointF(x, y);
                    NewWire.Pt2 = NewPt2;

                    // Redraw, this creates the "rubber band" effect while drawing the wire
                    schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox
                }
            }
        }

        private void schematicCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            tempComp = null;

            if (lineDrawing)
            {
                int x = e.X;
                int y = e.Y;
                SnapToGrid(ref x, ref y);
                startX = x;
                startY = y;

                // Iterate over the schematic component list and draw each component
                foreach (DigitalComponent comp in schematic.components)
                {
                    if (hitTest(comp))
                    {
                        Debug.WriteLine("End Component hit! " + comp.ToString());
                        NewWire.Nodes[1] = comp;
                    }
                }

                // Update the new wire end points
                NewWire.Pt1 = NewPt1;
                NewWire.Pt2 = NewPt2;
                NewWire.endcapsVisible = false;

                // Terminate any further updates to the new wire, this makes it permanent in the schematic wires list
                NewWire = null;

                // Redraw.
                schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox

                // Debugs to show the line points on the console
                Debug.WriteLine("Line End Points: (" + NewPt2.X + ", " + NewPt2.Y + " )");
            }
            lineDrawing = false;
        }

        private void LEDBtn_Click(object sender, EventArgs e)
        {
            Components.LED led = new Components.LED();
            schematic.components.Add(led);
            schematicCanvas.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            schematic = new Circuit();
            schematicCanvas.Refresh();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load the network;
                    Circuit network = Circuit.LoadCircuit(openFileDialog1.FileName);

                    // Start using the new network.
                    schematic = network;

                    // Draw the new network.
                    schematicCanvas.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Save the network.
                    schematic.SaveCircuit(saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SwitchBtn_Click(object sender, EventArgs e)
        {
            Components.Switch sw = new Components.Switch();
            schematic.components.Add(sw);
            schematicCanvas.Invalidate();
        }

        // Switch event handler
        public void tempSw_StateChanged(object sender, EventArgs e)
        {
            // Get the sender switch object
            Components.Switch tempSw = (Components.Switch)sender;
            Debug.WriteLine("Switch state: " + tempSw.logicState);

            // Traverse the network.
            schematic.Traverse(tempSw);
        }

       private void WireBtn_Click(object sender, EventArgs e)
        {
            lineDrawing = true;
        }

        private bool hitTest(DigitalComponent comp)
        {
            bool hit;

            hit = false;
            if (startX >= comp.Location.X && startX <= comp.Location.X + comp.Width)
            {
                if (startY >= comp.Location.Y && startY <= comp.Location.Y + comp.Height)
                {
                    //Debug.WriteLine("Hit!");
                    hit = true;
                }
            }
            else
            {
                //Debug.WriteLine("No Hit!");
                hit = false;
            }

            return hit;
        }
    } // End class Form
} // End namespace

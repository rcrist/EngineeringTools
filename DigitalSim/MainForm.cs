// C# Libraries
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// Digital Simulator Libraries
using DigitalSim.Circuits;
using DigitalSim.Components;
using DigitalSim.Wires;

namespace DigitalSim
{
    public partial class MainForm : Form
    {
        // Create a custom schematic
        private Circuit schematic = new Circuit();

        // The grid spacing.
        public const int grid_gap = 10;

        // Define pin constants
        const int Pin1 = 0;
        const int Pin2 = 1;
        const int Pout = 0;

        // Mouse event variables
        bool isMouseDown = false;
        float offsetX, offsetY, startX, startY;
        Comp tempComp = new Comp();
        bool lineDrawing = false;

        Pen lightPen = new Pen(Color.Black);

        // The wire we are drawing
        private Wire NewWire = null;

        // Points for the new line.
        private PointF NewPt1, NewPt2;

        // ***************************** Constructors ***********************************
        public MainForm()
        {
            InitializeComponent();

            // Add 4 swtiches
            Components.Switch sw1 = new Components.Switch();
            sw1.Name = "Switch";
            sw1.Location = new PointF(50, 100);
            schematic.comps.Add(sw1);
            Components.Switch sw2 = new Components.Switch();
            sw2.Name = "Switch";
            sw2.Location = new PointF(50, 120);
            schematic.comps.Add(sw2);
            Components.Switch sw3 = new Components.Switch();
            sw3.Name = "Switch";
            sw3.Location = new PointF(50, 160);
            schematic.comps.Add(sw3);
            Components.Switch sw4 = new Components.Switch();
            sw4.Name = "Switch";
            sw4.Location = new PointF(50, 180);
            schematic.comps.Add(sw4);

            // Add 2 AND gates
            AND and1 = new AND();
            and1.Name = "AND";
            and1.Location = new PointF(140, 100);
            schematic.comps.Add(and1);
            AND and2 = new AND();
            and2.Name = "AND";
            and2.Location = new PointF(230, 130);
            schematic.comps.Add(and2);

            // Add 1 OR gate
            OR or1 = new OR();
            or1.Name = "OR";
            or1.Location = new PointF(140, 160);
            schematic.comps.Add(or1);

            // Add 1 LED
            LED led = new LED();
            led.Name = "LED";
            led.Location = new PointF(330, 140);
            schematic.comps.Add(led);

            // Add 7 wires
            Wire w1 = new Wire(sw1, Pout, and1, Pin1);
            w1.Pt1 = new PointF(sw1.Location.X + 60, sw1.Location.Y + 20);
            w1.Pt2 = new PointF(and1.Location.X, and1.Location.Y + 20);
            sw1.wires.Add(w1);

            Wire w2 = new Wire(sw2, Pout, and1, Pin2);
            w2.Pt1 = new PointF(sw2.Location.X + 60, sw2.Location.Y + 20);
            w2.Pt2 = new PointF(and1.Location.X, and1.Location.Y + 40);
            sw2.wires.Add(w2);

            Wire w3 = new Wire(sw3, Pout, or1, Pin1);
            w3.Pt1 = new PointF(sw3.Location.X + 60, sw3.Location.Y + 20);
            w3.Pt2 = new PointF(or1.Location.X, or1.Location.Y + 20);
            sw3.wires.Add(w3);

            Wire w4 = new Wire(sw4, Pout, or1, Pin2);
            w4.Pt1 = new PointF(sw4.Location.X + 60, sw4.Location.Y + 20);
            w4.Pt2 = new PointF(or1.Location.X, or1.Location.Y + 40);
            sw4.wires.Add(w4);

            Wire w5 = new Wire(and1, Pout, and2, Pin1);
            w5.Pt1 = new PointF(and1.Location.X + 60, and1.Location.Y + 30);
            w5.Pt2 = new PointF(and2.Location.X, and2.Location.Y + 20);
            and1.wires.Add(w5);

            Wire w6 = new Wire(or1, Pout, and2, Pin2);
            w6.Pt1 = new PointF(or1.Location.X + 60, or1.Location.Y + 30);
            w6.Pt2 = new PointF(and2.Location.X, and2.Location.Y + 40);
            or1.wires.Add(w6);

            Wire w7 = new Wire(and2, Pout, led, Pin1);
            w7.Pt1 = new PointF(and2.Location.X + 60, and2.Location.Y + 30);
            w7.Pt2 = new PointF(led.Location.X, led.Location.Y + 20);
            and2.wires.Add(w7);
        }

        // ***************************** Canvas Methods *********************************
        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Iterate over the schematic component list and draw each componen
            foreach (Comp comp in schematic.comps)
            {
                if (comp is AND)
                {
                    AND tempAndGate = comp as AND;
                    tempAndGate.Draw(e.Graphics);

                    foreach (Wire wire in tempAndGate.wires)
                    {
                        Wire tempWire = wire;
                        tempWire.Draw(e.Graphics);
                    }
                }

                if (comp is OR)
                {
                    OR tempOrGate = comp as OR;
                    tempOrGate.Draw(e.Graphics);

                    foreach (Wire wire in tempOrGate.wires)
                    {
                        Wire tempWire = wire;
                        tempWire.Draw(e.Graphics);
                    }
                }

                if (comp is NOT)
                {
                    NOT tempInvGate = comp as NOT;
                    tempInvGate.Draw(e.Graphics);

                    foreach (Wire wire in tempInvGate.wires)
                    {
                        Wire tempWire = wire;
                        tempWire.Draw(e.Graphics);
                    }
                }

                if (comp is LED)
                {
                    LED tempLED = comp as LED;
                    tempLED.Draw(e.Graphics);
                }

                if (comp is Components.Switch)
                {
                    Components.Switch tempSw = comp as Components.Switch;
                    tempSw.Draw(e.Graphics);

                    foreach(Wire wire in tempSw.wires)
                    {
                        Wire tempWire = wire;
                        tempWire.Draw(e.Graphics);
                    }
                }
            }
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

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

        private void SnapToGrid(ref int x, ref int y)
        {
            //if (!chkSnapToGrid.Checked) return;
            x = grid_gap * (int)Math.Round((double)x / grid_gap);
            y = grid_gap * (int)Math.Round((double)y / grid_gap);
        }

        // ***************************** Mouse Event Handlers ***************************
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
                            foreach (Comp comp in schematic.comps)
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
                            //schematic.wires.Add(NewWire);

                            // Debugs to show the line points on the console
                            Debug.WriteLine("Line Start Points: (" + NewPt1.X + ", " + NewPt1.Y + " )");

                            // Iterate over the schematic component list and draw each component
                            foreach (Comp comp in schematic.comps)
                            {
                                if (hitTest(comp))
                                {
                                    Debug.WriteLine("Start Component hit!" + comp.ToString());
                                    NewWire.inComp = comp;
                                    NewWire.inCompPout = Pout;
                                    comp.wires.Add(NewWire);
                                }
                            }
                        }
                        break;
                    }
                case MouseButtons.Right:
                    {
                        foreach (Comp comp in schematic.comps)
                        {
                            if (comp is Components.Switch)
                            {
                                startX = e.X;
                                startY = e.Y;

                                if (swHitTest(comp))
                                {
                                    Components.Switch tempSw = comp as Components.Switch;
                                    tempSw.ToggleSwitchState();
                                    schematic.Traverse(tempSw);
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
                foreach (Comp comp in schematic.comps)
                {
                    if (hitTest(comp))
                    {
                        Debug.WriteLine("End Component hit! " + comp.ToString());
                        NewWire.outComp = comp;
                        if ((int)startY == (int)comp.Location.Y + 20 || (int)startY == (int)comp.Location.Y + 30)
                            NewWire.outCompPin = Pin1;
                        else if ((int)startY == (int)comp.Location.Y + 40)
                            NewWire.outCompPin = Pin2;
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

        // ***************************** Menu Button Handlers ***************************
        private void ANDBtn_Click(object sender, EventArgs e)
        {
            AND and = new AND();
            and.Name = "AND";
            and.Location = new PointF(100, 100);
            schematic.comps.Add(and);
            schematicCanvas.Invalidate();
        }

        private void ORBtn_Click(object sender, EventArgs e)
        {
            OR or1 = new OR();
            or1.Name = "OR";
            or1.Location = new PointF(200, 100);
            schematic.comps.Add(or1);
            schematicCanvas.Invalidate();
        }

        private void NOTBtn_Click(object sender, EventArgs e)
        {
            NOT not1 = new NOT();
            not1.Name = "NOT";
            not1.Location = new PointF(300, 100);
            schematic.comps.Add(not1);
            schematicCanvas.Invalidate();
        }

        private void SwitchBtn_Click(object sender, EventArgs e)
        {
            DigitalSim.Components.Switch sw1 = new DigitalSim.Components.Switch();
            sw1.Name = "Switch";
            sw1.Location = new PointF(100, 100);
            schematic.comps.Add(sw1);
            schematicCanvas.Invalidate();
        }

        private void LEDBtn_Click(object sender, EventArgs e)
        {
            LED led = new LED();
            led.Name = "LED";
            led.Location = new PointF(300, 100);
            schematic.comps.Add(led);
            schematicCanvas.Invalidate();
        }

        private void WireBtn_Click(object sender, EventArgs e)
        {
            lineDrawing = true;
        }

        // ***************************** File Button Handlers ***************************
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            schematic = new Circuit();
            schematicCanvas.Refresh();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Save the circuit.
                    schematic.SaveCircuit(saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load the network;
                    Circuit circuit = Circuit.LoadCircuit(openFileDialog1.FileName, lightPen);

                    // Start using the new network.
                    schematic = circuit;

                    // Draw the new network.
                    schematicCanvas.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // ***************************** Modern UI Form *********************************
        private void ModernUI_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        // ***************************** Helper Methods *********************************
        private bool hitTest(Comp comp)
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

        private bool swHitTest(Comp comp)
        {
            bool hit;

            hit = false;
            if (startX >= comp.Location.X+10 && startX <= comp.Location.X + comp.Width - 10)
            {
                if (startY >= comp.Location.Y+20  && startY <= comp.Location.Y + comp.Height-20)
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
    }
}
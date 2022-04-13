using DigitalSim.Circuits;
using DigitalSim.Components;
using DigitalSim.Wires;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DigitalSim
{
    public partial class Form1 : Form
    {
        // Create a custom schematic
        private Circuit schematic = new Circuit();

        public Pen lightPen = new Pen(Color.LightGray, 2);

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

        // The wire we are drawing
        private Wire NewWire = null;

        // Points for the new line.
        private PointF NewPt1, NewPt2;

        public Form1()
        {
            InitializeComponent();
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
                    bm.SetPixel(x, y, Color.DarkGray);
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

        private void form1_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

 
        private void hideSubMenu()
        {
            if (panelCircuitSubmenu.Visible == true)
                panelComponentsSubmenu.Visible = false;
            //if (panelPlaylistSubmenu.Visible == true)
            //    panelPlaylistSubmenu.Visible = false;
            //if (panelToolsSubmenu.Visible == true)
            //    panelToolsSubmenu.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

                    foreach (Wire wire in tempSw.wires)
                    {
                        Wire tempWire = wire;
                        tempWire.Draw(e.Graphics);
                    }
                }
            }
        }

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
                            NewWire = new Wire(lightPen); // Use the constructor with no parameters
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

        private void btnOR_Click(object sender, EventArgs e)
        {
            OR or1 = new OR(lightPen);
            or1.Name = "OR";
            or1.Location = new PointF(200, 100);
            schematic.comps.Add(or1);
            schematicCanvas.Invalidate();
        }

        private void btnAND_Click(object sender, EventArgs e)
        {
            AND and = new AND(lightPen);
            and.Name = "AND";
            and.Location = new PointF(100, 100);
            schematic.comps.Add(and);
            schematicCanvas.Invalidate();
        }

       private void btnNOT_Click(object sender, EventArgs e)
        {
            NOT not1 = new NOT(lightPen);
            not1.Name = "NOT";
            not1.Location = new PointF(300, 100);
            schematic.comps.Add(not1);
            schematicCanvas.Invalidate();
        }

       private void btnSwitch_Click(object sender, EventArgs e)
        {
            DigitalSim.Components.Switch sw1 = new DigitalSim.Components.Switch(lightPen);
            sw1.Name = "Switch";
            sw1.Location = new PointF(100, 100);
            schematic.comps.Add(sw1);
            schematicCanvas.Invalidate();
        }

       private void btnLED_Click(object sender, EventArgs e)
        {
            LED led = new LED(lightPen, Color.White);
            led.Name = "LED";
            led.Location = new PointF(300, 100);
            schematic.comps.Add(led);
            schematicCanvas.Invalidate();
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                //hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnCircuit_Click(object sender, EventArgs e)
        {
            showSubMenu(panelCircuitSubmenu);
        }

        private void btnWire_Click(object sender, EventArgs e)
        {
            lineDrawing = true;
        }

        private void btnStore_Click(object sender, EventArgs e)
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

        private void btnLoad_Click(object sender, EventArgs e)
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

        private void btnComponents_Click(object sender, EventArgs e)
        {
            showSubMenu(panelComponentsSubmenu);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            schematic = new Circuit();
            schematicCanvas.Refresh();
        }

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
            if (startX >= comp.Location.X + 10 && startX <= comp.Location.X + comp.Width - 10)
            {
                if (startY >= comp.Location.Y + 20 && startY <= comp.Location.Y + comp.Height - 20)
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

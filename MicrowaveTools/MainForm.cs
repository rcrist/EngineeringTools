// C# Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// Microwave Tools Libraries
using MicrowaveTools.Circuits;
using MicrowaveTools.Components;
using MicrowaveTools.Components.Ideal;
using MicrowaveTools.Components.Lumped;
using MicrowaveTools.Components.Microstrip;
using MicrowaveTools.Wires;

namespace MicrowaveTools
{
    public partial class MainForm : Form
    {
        private Circuit ckt = new Circuit();

        // The grid spacing.
        public const int grid_gap = 10;

        // Keypress event variables
        public bool rotate = false; // Rotate component flag
        public bool delete = false; // Delete component flag

        // Mouse event variables
        bool isMouseDown = false;
        bool lineDrawing = false;
        int offsetX, offsetY, startX, startY;
        Comp tempComp = new Comp();

        // The wire we are drawing
        private Wire NewWire = null;

        // Points for the new line.
        private Point NewPt1, NewPt2;

        // Create the wire mode LED
        LED led = new LED(new Point(300, 200));

        // ************************ Constructors ****************************************
        public MainForm()
        {
            InitializeComponent();
        }

        #region Event_Handlers
        // ************************ Paint & Resize Events *******************************
        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (Comp comp in ckt.comps)
            {
                comp.Draw(e.Graphics);
                schematicCanvas.Invalidate();

                foreach (Wire wire in comp.wires)
                {
                    wire.Draw(e.Graphics);
                    schematicCanvas.Invalidate();
                }
            }

            // Display permanent wire mode LED
            showWireModeLED(e.Graphics);
        }

        private void schematicCanvas_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

        private void showWireModeLED(Graphics gr)
        {
            // Draw the wire mode LED in the lower left corner of the schematic canvas
            int x, y;
            x = 80;
            y = schematicCanvas.ClientSize.Height - 20; // Forces location to lower left corner if height changes
            led.Loc = new Point(x, y);
            led.Draw(gr);
        }

        private void schematicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        closeOtherPanels(null);
                        isMouseDown = true;

                        if (!lineDrawing)
                        {// Snap the start point to the Grid
                            int x = e.X;
                            int y = e.Y;
                            SnapToGrid(ref x, ref y);
                            startX = x;
                            startY = y;

                            // Iterate over the schematic component list & draw each component
                            foreach (Comp comp in ckt.comps)
                            {
                                if (hitTest(comp))
                                {
                                    tempComp = comp;
                                    offsetX = startX - comp.Loc.X;
                                    offsetY = startY - comp.Loc.Y;
                                }
                            }
                        }
                        if (lineDrawing)
                        {
                            // Snap the start point to the Grid
                            int x = e.X;
                            int y = e.Y;
                            SnapToGrid(ref x, ref y);
                            NewPt1 = new Point(x, y);
                            NewPt2 = new Point(x, y);
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

                            // Iterate over the schematic component list & draw each component
                            foreach (Comp comp in ckt.comps)
                            {
                                if (hitTest(comp))
                                {
                                    Debug.WriteLine("Start Component hit!" + comp.ToString());
                                    NewWire.Cin = comp;
                                    //NewWire.Cout = Pout;
                                    comp.wires.Add(NewWire);
                                }
                            }
                        }
                        break;
                    }
                case MouseButtons.Right:
                    {
                        int x = e.X;
                        int y = e.Y;
                        SnapToGrid(ref x, ref y);
                        startX = x;
                        startY = y;
                        Comp hitComp = new Comp();

                        foreach (Comp comp in ckt.comps)
                        {

                            if (hitTest(comp))
                            {
                                hitComp = comp;
                            }
                        }

                        // TODO: Add code for property grid here

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
                        if (rotate)
                        {
                            if (tempComp.Orientation == "Series")
                                tempComp.Orientation = "Shunt";
                            else if (tempComp.Orientation == "Shunt")
                                tempComp.Orientation = "Series";
                            rotate = false;
                        }

                        // TODO: Add delete code here

                        int x = e.X;
                        int y = e.Y;
                        SnapToGrid(ref x, ref y);
                        tempComp.Loc = new Point(x - offsetX, y - offsetY);
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
                    NewPt2 = new Point(x, y);
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
                foreach (Comp comp in ckt.comps)
                {
                    if (hitTest(comp))
                    {
                        Debug.WriteLine("End Component hit! " + comp.ToString());
                        NewWire.Cout = comp;
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
            //lineDrawing = false;
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 114 || e.KeyChar == 82)    // 114 = r, 82 = R
                rotate = !rotate;

            if (e.KeyChar == 100 || e.KeyChar == 68)    // 100 = d, 68 = D
                delete = !delete;
        }

        #endregion // Event_Handlers

        #region Main_Menu_Button_Handlers
        // *********************** Main Menu Button Handlers ****************************
        private void btnCircuit_Click(object sender, EventArgs e)
        {
            panelCircuitSubmenu.Visible = !panelCircuitSubmenu.Visible;
        }

        #endregion // Main_Menu_Button_Handlers

        #region Comp_Menu_Button_Handlers
        // *********************** Component Menu Button Handlers ***********************
        private void btnLumped_Click(object sender, EventArgs e)
        {
            panelLumpedSubmenu.Visible = !panelLumpedSubmenu.Visible;
            closeOtherPanels(panelLumpedSubmenu);
        }

        private void btnIdeal_Click(object sender, EventArgs e)
        {
            panelIdealSubmenu.Visible = !panelIdealSubmenu.Visible;
            closeOtherPanels(panelIdealSubmenu);
        }

        private void btnMicrostrip_Click(object sender, EventArgs e)
        {
            panelMicrostripSubmenu.Visible = !panelMicrostripSubmenu.Visible;
            closeOtherPanels(panelMicrostripSubmenu);
        }

        private void btnRES_Click(object sender, EventArgs e)
        {
            RES res = new RES(75.0f, new Point(200, 300), new int[] { 0, 0 });
            ckt.comps.Add(res);
            schematicCanvas.Invalidate();
        }

        private void btnIND_Click(object sender, EventArgs e)
        {
            IND ind = new IND(5.0f, new Point(300, 300), new int[] { 0, 0 });
            ckt.comps.Add(ind);
            schematicCanvas.Invalidate();
        }

        private void btnCAP_Click(object sender, EventArgs e)
        {
            CAP cap = new CAP(1.0f, new Point(400, 300), new int[] { 0, 0 });
            ckt.comps.Add(cap);
            schematicCanvas.Invalidate();
        }

        private void btnInPort_Click(object sender, EventArgs e)
        {
            InPort pin = new InPort(50.0f, new Point(300, 300), new int[] { 0, 0 });
            ckt.comps.Add(pin);
            schematicCanvas.Invalidate();
        }

        private void btnOutPort_Click(object sender, EventArgs e)
        {
            OutPort pout = new OutPort(50.0f, new Point(500, 300), new int[] { 0, 0 });
            ckt.comps.Add(pout);
            schematicCanvas.Invalidate();
        }

        private void btnGround_Click(object sender, EventArgs e)
        {
            Ground gnd = new Ground();
            ckt.comps.Add(gnd);
            schematicCanvas.Invalidate();
        }

        private void btnMLIN_Click(object sender, EventArgs e)
        {
            MLIN mlin = new MLIN(75.0f, new Point(200, 300), new int[] { 0, 0 });
            ckt.comps.Add(mlin);
            schematicCanvas.Invalidate();
        }

        private void btnMCROS_Click(object sender, EventArgs e)
        {
            MCROS mcros = new MCROS(100.0f, new Point(300, 300), new int[] { 0, 0 });
            ckt.comps.Add(mcros);
            schematicCanvas.Invalidate();
        }

        private void btnMTEE_Click(object sender, EventArgs e)
        {
            MTEE mtee = new MTEE(100.0f, new Point(400, 300), new int[] { 0, 0 });
            ckt.comps.Add(mtee);
            schematicCanvas.Invalidate();
        }

        private void btnRLC_Click(object sender, EventArgs e)
        {
            int[] nodes = new int[] { 0, 0 };
            InPort pin = new InPort(50.0f, new Point(160, 230), nodes);
            ckt.comps.Add(pin);

            nodes = new int[] { 0, 0 };
            RES res = new RES(75.0f, new Point(200, 230), nodes);
            ckt.comps.Add(res);

            nodes = new int[] { 0, 0 };
            IND ind = new IND(5.0f, new Point(300, 230), nodes);
            ckt.comps.Add(ind);

            nodes = new int[] { 0, 0 };
            CAP cap = new CAP(1.0f, new Point(400, 230), nodes);
            ckt.comps.Add(cap);

            nodes = new int[] { 0, 0 };
            OutPort pout = new OutPort(50.0f, new Point(500, 230), nodes);
            ckt.comps.Add(pout);

            //_ = new Wire(pin, res); // Use of discard _
            //_ = new Wire(res, ind);
            //_ = new Wire(ind, cap);
            //_ = new Wire(cap, pout);

            Wire w1 = new Wire(pin, res); ckt.comps.Add(w1);
            Wire w2 = new Wire(res, ind); ckt.comps.Add(w2);
            Wire w3 = new Wire(ind, cap); ckt.comps.Add(w3);
            Wire w4 = new Wire(cap, pout); ckt.comps.Add(w4);

            schematicCanvas.Invalidate();
        }

        private void btnWireMode_Click(object sender, EventArgs e)
        {
            lineDrawing = !lineDrawing;
            led.logicState = lineDrawing;
            schematicCanvas.Invalidate();
        }
        #endregion // Comp_Menu_Button_Handlers

        #region Helper_Methods
        // *********************** Helper Methods ***************************************
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

        // Snap XY point to the grid
        private void SnapToGrid(ref int x, ref int y)
        {
            //if (!chkSnapToGrid.Checked) return;
            x = grid_gap * (int)Math.Round((double)x / grid_gap);
            y = grid_gap * (int)Math.Round((double)y / grid_gap);
        }

        private void closeOtherPanels(Panel thisPanel)
        {
            if (thisPanel == panelLumpedSubmenu)
            {
                panelIdealSubmenu.Visible = false;
                panelMicrostripSubmenu.Visible = false;
            }

            if (thisPanel == panelIdealSubmenu)
            {
                panelLumpedSubmenu.Visible = false;
                panelMicrostripSubmenu.Visible = false;
            }

            if (thisPanel == panelMicrostripSubmenu)
            {
                panelLumpedSubmenu.Visible = false;
                panelIdealSubmenu.Visible = false;
            }

            if (thisPanel == null)
            {
                panelLumpedSubmenu.Visible = false;
                panelIdealSubmenu.Visible = false;
                panelMicrostripSubmenu.Visible = false;
            }
        }

        private bool hitTest(Comp comp)
        {
            Debug.WriteLine("Hit test on comp: " + comp.ToString() + " Loc: " + comp.Loc.ToString());
            bool hit;

            hit = false;
            if ((startX >= comp.Loc.X && startX <= comp.Loc.X + comp.Width) &&
                (startY >= comp.Loc.Y && startY <= comp.Loc.Y + comp.Height))
            {
                Debug.WriteLine("Hit!");
                hit = true;
            }
            else
            {
                Debug.WriteLine("No Hit!");
                hit = false;
            }

            return hit;
        }

        #endregion // Helper_Methods
    }
}
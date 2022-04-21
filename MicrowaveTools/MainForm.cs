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
using static MicrowaveTools.Circuits.Circuit;

namespace MicrowaveTools
{
    public partial class MainForm : Form
    {
        Circuit ckt = new Circuit();

        // The grid spacing.
        public const int grid_gap = 10;

        List<Comp> comps = new List<Comp>();

        private Point NewPt1, NewPt2, offset;
        Comp tempComp = new Comp();
        Wire NewWire = new Wire();

        // Mouse event variables
        bool isMouseDown = false;
        bool lineDrawing = false;

        private bool isSelectMode = false;

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

                        // Snap the start point to the Grid
                        int x = e.X;
                        int y = e.Y;
                        SnapToGrid(ref x, ref y);
                        NewPt1.X = x;
                        NewPt1.Y = y;

                        if (!lineDrawing)
                        {
                            // Iterate over the component list and test each to see if it is "hit"
                            foreach (Comp comp in ckt.comps)
                            {
                                if (hitTest(comp))
                                {
                                    tempComp = comp;
                                    offset.X = NewPt1.X - comp.Loc.X;
                                    offset.Y = NewPt1.Y - comp.Loc.Y;

                                    if (isSelectMode)
                                    {
                                        if (!comp.isSelected)
                                            comp.isSelected = true;
                                        else
                                            comp.isSelected = false;
                                    }

                                    schematicCanvas.Invalidate();
                                }

                                foreach (Wire wire in comp.wires)
                                {
                                    if (isSelectMode)
                                    {
                                        if (hitTest(wire))
                                        {
                                            if (!wire.isSelected)
                                                wire.isSelected = true;
                                            else
                                                wire.isSelected = false;
                                        }
                                    }

                                    schematicCanvas.Invalidate();
                                }
                            }
                        }
                        if (lineDrawing)
                        {
                            // Snap the start point to the Grid
                            x = e.X;
                            y = e.Y;
                            SnapToGrid(ref x, ref y);
                            NewPt1 = new Point(x, y);
                            NewPt2 = new Point(x, y);
                            NewPt1.X = x;
                            NewPt1.Y = y;

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
                        NewPt1.X = x;
                        NewPt1.Y = y;
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
                int x = e.X;
                int y = e.Y;
                SnapToGrid(ref x, ref y);

                if (!lineDrawing)
                {
                    if (tempComp != null)
                    {
                        tempComp.Loc = new Point(x - offset.X, y - offset.Y);

                        // Stretch output wires
                        if (tempComp.wires.Count != 0)
                            tempComp.wires[0].Pt1 = new Point(tempComp.Loc.X + tempComp.Width, tempComp.Loc.Y + 30);

                        // Stretch input wires
                        foreach (Comp comp in ckt.comps)
                        {
                            foreach (Wire wire in comp.wires)
                            {
                                if (wire.Cout == tempComp)
                                    comp.wires[0].Pt2 = new Point(tempComp.Loc.X, tempComp.Loc.Y + 30);
                            }
                        }
                    }
                    schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox
                }
                if (lineDrawing)
                {
                    if (NewWire == null) return;
                    // Snap the end point to the Grid
                    x = e.X;
                    y = e.Y;
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
                NewPt1.X = x;
                NewPt1.Y = y;

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
            {
                foreach (Comp comp in ckt.comps)
                {
                    if (comp.isSelected)
                    {
                        comp.isRotated = !comp.isRotated;
                        schematicCanvas.Invalidate();
                    }
                }
            }
        }

        private void TestDeleteMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)    // 46 - Delete Key
            {
                confirmDelete();
            }
        }

        private void confirmDelete()
        {
            DialogResult res = MessageBox.Show("Are you sure you want to Delete all selected components",
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                Debug.WriteLine("Delete confirmed!");
                deleteSelectedComps();
            }
            if (res == DialogResult.Cancel)
            {
                Debug.WriteLine("Delete cancelled...");
            }
        }

        private void deleteSelectedComps()
        {
            // Search for selected components
            for (int i = 0; i < ckt.comps.Count; i++)
            {
                if (ckt.comps[i].isSelected)
                {
                    // Iterate over all components again
                    for (int k = 0; k < ckt.comps.Count; k++)
                    {
                        // Iterate over the wire list for the current component
                        for (int j = 0; j < ckt.comps[k].wires.Count; j++)
                        {
                            // If wire Cout = selected component, delete the wire
                            if (ckt.comps[k].wires[j].Cout == ckt.comps[i])
                                ckt.comps[k].wires.RemoveAt(j);
                        }
                    }

                    // Delete the selected component
                    ckt.comps.RemoveAt(i);
                }
            }

            schematicCanvas.Invalidate();
        }

        #endregion // Event_Handlers

        #region Main_Menu_Button_Handlers
        // *********************** Main Menu Button Handlers ****************************
        private void btnCircuit_Click(object sender, EventArgs e)
        {
            panelCircuitSubmenu.Visible = !panelCircuitSubmenu.Visible;
        }

        private void btnWireMode_Click(object sender, EventArgs e)
        {
            lineDrawing = !lineDrawing;
            led.logicState = lineDrawing;
            schematicCanvas.Invalidate();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            isSelectMode = !isSelectMode;
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
            RES res = new RES(75.0f, new Point(200, 100));
            ckt.comps.Add(res);
            schematicCanvas.Invalidate();
        }

        private void btnIND_Click(object sender, EventArgs e)
        {
            IND ind = new IND(5.0f, new Point(300, 300));
            ckt.comps.Add(ind);
            schematicCanvas.Invalidate();
        }

        private void btnCAP_Click(object sender, EventArgs e)
        {
            CAP cap = new CAP(1.0f, new Point(400, 300));
            ckt.comps.Add(cap);
            schematicCanvas.Invalidate();
        }

        private void btnInPort_Click(object sender, EventArgs e)
        {
            InPort pin = new InPort(50.0f, new Point(300, 300));
            ckt.comps.Add(pin);
            schematicCanvas.Invalidate();
        }

        private void btnOutPort_Click(object sender, EventArgs e)
        {
            OutPort pout = new OutPort(50.0f, new Point(500, 300));
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
            MLIN mlin = new MLIN(20.0f, new Point(200, 300));
            ckt.comps.Add(mlin);
            schematicCanvas.Invalidate();
        }

        private void btnMCROS_Click(object sender, EventArgs e)
        {
            MCROS mcros = new MCROS(20.0f, new Point(300, 300));
            ckt.comps.Add(mcros);
            schematicCanvas.Invalidate();
        }

        private void btnMTEE_Click(object sender, EventArgs e)
        {
            MTEE mtee = new MTEE(20.0f, new Point(400, 300));
            ckt.comps.Add(mtee);
            schematicCanvas.Invalidate();
        }

        private void btnRLC_Click(object sender, EventArgs e)
        {
            InPort pin = new InPort(50.0f, new Point(100, 200));
            ckt.comps.Add(pin);

            RES res = new RES(75.0f, new Point(200, 200));
            ckt.comps.Add(res);

            IND ind = new IND(5.0f, new Point(300, 200));
            ckt.comps.Add(ind);

            CAP cap = new CAP(1.0f, new Point(400, 200));
            ckt.comps.Add(cap);

            OutPort pout = new OutPort(50.0f, new Point(500, 200));
            ckt.comps.Add(pout);

            Wire w1 = new Wire(pin, res); ckt.comps.Add(w1);
            Wire w2 = new Wire(res, ind); ckt.comps.Add(w2);
            Wire w3 = new Wire(ind, cap); ckt.comps.Add(w3);
            Wire w4 = new Wire(cap, pout); ckt.comps.Add(w4);

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

        private void SnapToGrid(ref int x, ref int y)
        {
            //if (!chkSnapToGrid.Checked) return;
            x = grid_gap * (int)Math.Round((double)x / grid_gap);
            y = grid_gap * (int)Math.Round((double)y / grid_gap);
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
 
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
            Debug.WriteLine("Mouse Pos: " + NewPt1.ToString());
            bool hit;

            hit = false;
            if ((NewPt1.X >= comp.boundBox.X && NewPt1.X <= comp.boundBox.X + comp.boundBox.Width) &&
                (NewPt1.Y >= comp.boundBox.Y && NewPt1.Y <= comp.boundBox.Y + comp.boundBox.Height))
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
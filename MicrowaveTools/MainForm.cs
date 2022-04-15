// C# Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCircuit_Click(object sender, EventArgs e)
        {
            panelCircuitSubmenu.Visible = !panelCircuitSubmenu.Visible;
        }

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

        // *********************** Helper Methods ***************************************
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

        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (Comp comp in ckt.comps)
            {
                comp.Draw(e.Graphics);
                schematicCanvas.Invalidate();
            }
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
    }
}
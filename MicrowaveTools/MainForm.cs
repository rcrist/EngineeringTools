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
    }
}
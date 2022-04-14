using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicrowaveTools
{
    public partial class MainForm : Form
    {
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
    }
}

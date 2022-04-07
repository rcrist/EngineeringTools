using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineeringTools
{
    public partial class EngineeringToolsMainForm : Form
    {
        public EngineeringToolsMainForm()
        {
            InitializeComponent();
        }

        private void btnCircuit_Click(object sender, EventArgs e)
        {
            panelCircuitSubmenu.Visible = !panelCircuitSubmenu.Visible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                    }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnDigital_Click(object sender, EventArgs e)
        {
            panelDigitalSubmenu.Visible = !panelDigitalSubmenu.Visible;
        }
    }
}

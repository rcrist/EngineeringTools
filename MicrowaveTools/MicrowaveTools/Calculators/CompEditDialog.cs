using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MicrowaveTools.Components;
using MicrowaveTools.Components.Ideal;
using MicrowaveTools.Components.Lumped;

namespace MicrowaveTools
{
    public partial class CompEditDialog : Form
    {
        private Comp tempComp = new Comp();
        public Comp updatedComp = new Comp();
        public string ReturnName;
        public float ReturnValue;

        public CompEditDialog(Comp comp)
        {
            InitializeComponent();

            // Cast the component to its subclass
            if (comp is Resistor)
            {
                Resistor tempComp = comp as Resistor;
                setParams("Resistor Editor", tempComp);
            }
            if (comp is Inductor)
            {
                Inductor tempComp = comp as Inductor;
                setParams("Inductor Editor", tempComp);
            }
            if (comp is Capacitor)
            {
                Capacitor tempComp = comp as Capacitor;
                setParams("Capacitor Editor", tempComp);
            }
            if (comp is InPort)
            {
                InPort tempComp = comp as InPort;
                setParams("Input Port Editor", tempComp);
            }
            if (comp is OutPort)
            {
                OutPort tempComp = comp as OutPort;
                setParams("Output Port Editor", tempComp);
            }
        }

        private void setParams(string Title, Comp comp)
        {
            lblTitle.Text = Title;
            lblOldType.Text = comp.Type;
            lblOldName.Text = comp.Name;
            lblOldValue.Text = comp.Value.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ReturnName = tempComp.Name;
            this.ReturnValue = tempComp.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            tempComp.Name = tbName.Text;
        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {
            tempComp.Value = float.Parse(tbValue.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

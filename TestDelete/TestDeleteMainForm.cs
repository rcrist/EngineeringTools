// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// Test Delete Libraries
using TestDelete.Circuits;
using TestDelete.Components;
using TestDelete.Components.Ideal;
using TestDelete.Components.Lumped;
using TestDelete.Wires;

namespace TestDelete
{
    public partial class TestDeleteMainForm : Form
    {
        Point hitPt;

        Circuit ckt = new Circuit();

        private bool isSelectMode = false;

        public TestDeleteMainForm()
        {
            InitializeComponent();
        }

        private void initCkt()
        {
            int[] nodes = new int[] { 0, 0 };
            InPort pin = new InPort(50.0f, new Point(160, 230), nodes);
            ckt.comps.Add(pin);

            nodes = new int[] { 0, 0 };
            RES res = new RES(75.0f, new Point(200, 230), nodes);
            ckt.comps.Add(res);

            nodes = new int[] { 0, 0 };
            OutPort pout = new OutPort(50.0f, new Point(300, 230), nodes);
            ckt.comps.Add(pout);

            Wire w1 = new Wire(pin, res);
            Wire w2 = new Wire(res, pout);

            schematicCanvas.Invalidate();
        }

        private void TestDeleteMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)    // 46 - Delete Key
            {
                confirmDelete();
            }
        }

        void confirmDelete()
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

        private void btnInit_Click(object sender, EventArgs e)
        {
            initCkt();
        }

        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (Comp comp in ckt.comps)
            {
                comp.Draw(e.Graphics);

                foreach (Wire wire in comp.wires)
                {
                    wire.Draw(e.Graphics);
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            isSelectMode = !isSelectMode;
        }

        private void schematicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            hitPt.X = e.X;
            hitPt.Y = e.Y;

            // Iterate over the schematic component list & perform hitText
            foreach (Comp comp in ckt.comps)
            {
                if (hitTest(comp))
                {
                    if (!comp.isSelected)
                        comp.isSelected = true;
                    else
                        comp.isSelected = false;

                    schematicCanvas.Invalidate();
                }
            }
        }

        private bool hitTest(Comp comp)
        {
            bool hit;

            hit = false;
            if ((hitPt.X >= comp.Loc.X && hitPt.X <= comp.Loc.X + comp.Width) &&
                (hitPt.Y >= comp.Loc.Y && hitPt.Y <= comp.Loc.Y + comp.Height))
                    hit = true;
            else
                hit = false;

            return hit;
        }
    }
}
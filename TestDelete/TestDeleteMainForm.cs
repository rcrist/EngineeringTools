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
        Circuit ckt = new Circuit();

        // The grid spacing.
        public const int grid_gap = 10;

        List<Comp> comps = new List<Comp>();

        private Point NewPt1, NewPt2, offset;
        Comp tempComp = new Comp();

        // Mouse event variables
        bool isMouseDown = false;
        bool lineDrawing = false;

        private bool isSelectMode = false;

        public TestDeleteMainForm()
        {
            InitializeComponent();
        }

        private void initCkt()
        {
            InPort pin = new InPort(50.0f, new Point(160, 230));
            ckt.comps.Add(pin);

            RES res = new RES(75.0f, new Point(260, 230));
            ckt.comps.Add(res);

            OutPort pout = new OutPort(50.0f, new Point(360, 230));
            ckt.comps.Add(pout);

            Wire w1 = new Wire(pin, res);
            Wire w2 = new Wire(res, pout);

            schematicCanvas.Invalidate();
        }

        private void TestDeleteMainForm_KeyPress(object sender, KeyPressEventArgs e)
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

                    foreach(Wire wire in comp.wires)
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
                    }
                }

                schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox
            }
        }

        private void schematicCanvas_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

        private void schematicCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            tempComp = null;
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

    }
}
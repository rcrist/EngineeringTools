using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestDiagram
{
    public partial class TestDigramMainForm : Form
    {
        // The grid spacing.
        public const int grid_gap = 10;

        // Create a component list
        List<Comp> comps = new List<Comp>();

        public TestDigramMainForm()
        {
            InitializeComponent();
        }

        /* **************************** Grid & Snap Methods **************************** */
        private void DrawBackgroundGrid()
        {
            Bitmap bm = new Bitmap(
                2000,
                2000);
            for (int x = 0; x < 2000; x += grid_gap)
            {
                for (int y = 0; y < 2000; y += grid_gap)
                {
                    bm.SetPixel(x, y, Color.Black); //Color.Black);
                }
            }
            schematicCanvas.BackgroundImage = bm;
        }

        private void schematicCanvas_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach(Comp comp in comps)
            {
                comp.Draw(e.Graphics);
            }
        }

        private void btnCreateComponent_Click(object sender, EventArgs e)
        {
            Comp comp = new Comp();
            comps.Add(comp);
            schematicCanvas.Invalidate();
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle();
            comps.Add(rect);
            schematicCanvas.Invalidate();
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            Circle circ = new Circle();
            comps.Add(circ);
            schematicCanvas.Invalidate();
        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {
            Triangle tri = new Triangle();
            comps.Add(tri);
            schematicCanvas.Invalidate();
        }
    }
}

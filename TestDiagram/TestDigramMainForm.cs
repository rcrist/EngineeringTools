using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        // Mouse event attributes
        bool isMouseDown = false;
        private Point NewPt1, offset;
        Comp tempComp = new Comp();

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

        private void schematicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            NewPt1.X = e.X;
            NewPt1.Y = e.Y;

            // Iterate over the component list and test each to see if it is "hit"
            foreach (Comp comp in comps)
            {
                if (hitTest(comp))
                {
                    tempComp = comp;
                    offset.X = NewPt1.X - comp.loc.X;
                    offset.Y = NewPt1.Y - comp.loc.Y;
                }
            }
        }

        private void schematicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (tempComp != null)
                {
                    int x = e.X;
                    int y = e.Y;
                    tempComp.loc = new Point(x - offset.X, y - offset.Y);
                    schematicCanvas.Invalidate();
                }
            }
        }

        private void schematicCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            tempComp = null;
        }

        /* **************************** Helper Functions **************************** */
        private bool hitTest(Comp comp)
        {
            bool hit;

            hit = false;
            Debug.WriteLine("NewPt1: " + NewPt1.ToString());
            if ((NewPt1.X >= comp.loc.X && NewPt1.X <= comp.loc.X + comp.width) &&
                (NewPt1.Y >= comp.loc.Y && NewPt1.Y <= comp.loc.Y + comp.height))
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

    }
}

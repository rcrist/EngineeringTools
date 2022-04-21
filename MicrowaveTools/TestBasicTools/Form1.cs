using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TestBasicTools
{
    public partial class Form1 : Form
    {
        // Create the circuit
        Circuit ckt = new Circuit();
        List<Comp> netlist = new List<Comp>();

        // The grid spacing.
        public const int grid_gap = 10;

        public Form1()
        {
            InitializeComponent();

            int[] nodes = new int[] { 1, 2 };
        }

        /* **************************** Paint Method **************************** */
        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (Comp comp in ckt.comps)
            {
                comp.Draw(e.Graphics);
                schematicCanvas.Invalidate();

                foreach (Wire wire in comp.wires)
                {
                    wire.Draw(e.Graphics);
                }
                schematicCanvas.Invalidate();
        }
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
                    bm.SetPixel(x, y, Color.DarkGray); //Color.DarkGray);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void schematicCanvas_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int[] nodes = new int[] { 0, 0 };
            InPort pin = new InPort(50.0f, new Point(100, 200), nodes);
            ckt.comps.Add(pin);

            nodes = new int[] { 0, 0 };
            Resistor res = new Resistor(75.0f, new Point(200, 200), nodes);
            ckt.comps.Add(res);

            nodes = new int[] { 0, 0 };
            Inductor ind = new Inductor(5.0e-9f, new Point(300, 200), nodes);
            ckt.comps.Add(ind);

            nodes = new int[] { 0, 0 };
            Capacitor cap = new Capacitor(1.0e-12f, new Point(400, 200), nodes);
            ckt.comps.Add(cap);

            nodes = new int[] { 0, 0 };
            OutPort pout = new OutPort(50.0f, new Point(500, 200), nodes);
            ckt.comps.Add(pout);

            Wire w1 = new Wire(pin, res);
            Wire w2 = new Wire(res, ind);
            Wire w3 = new Wire(ind, cap);
            Wire w4 = new Wire(cap, pout);

            schematicCanvas.Invalidate();
        }

        private void btnNetlist_Click(object sender, EventArgs e)
        {
            ckt.Traverse();
        }

        private void printNetlist()
        {
            foreach (Comp comp in netlist)
            {
                comp.print();
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            ckt.AnalyzeCircuit();
            Console.WriteLine("Analysis complete!");
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (ckt.S != null)
                Console.WriteLine("S: " + ckt.S.ToString());
            else
                Console.WriteLine("Hit the Analyze button first!");
        }
    }
}

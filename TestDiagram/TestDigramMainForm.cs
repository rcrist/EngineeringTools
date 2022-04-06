using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDiagram
{
    public partial class TestDigramMainForm : Form
    {
        // The grid spacing.
        public const int grid_gap = 10;

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
    }
}

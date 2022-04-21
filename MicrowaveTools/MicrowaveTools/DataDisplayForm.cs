using MicrowaveTools.Circuits;
using System;
using System.Windows.Forms;
using static MicrowaveTools.Circuits.Circuit;

namespace MicrowaveTools
{
    public partial class DataDisplayForm : Form
    {
        public DataDisplayForm(Circuit ckt)
        {
            InitializeComponent();

            // Plot format key
            // re-im: Sxx_1 = re, Sxx_2 = im
            // mag-ang: S11_1 = mag, Sxx_2 = ang (deg)
            // magDB-ang: S11_1 = magDB, Sxx_2 = ang (deg)

            // Plat data in clt.results list to XY Chart
            foreach (ResultRecord result in ckt.results)
            {
                chartDataDisplay.Series[0].Points.AddXY(result.f, result.S11_1); // Line 0 plots S11 = S22 versus f
                chartDataDisplay.Series[1].Points.AddXY(result.f, result.S12_1); // Line 1 plots S12 = S21 versus f
            }

            // Print data in ckt.results list on Data Display textbox
            int numResults = ckt.results.Count;

            for (int i = 0; i < numResults; i++)
            {
                rtbDataDisplay.Text += ckt.results[i].f + "  \t" + String.Format("{0:0.###}", ckt.results[i].S11_1) + "<" + String.Format("{0:0.###}", ckt.results[i].S11_2) +
                      "  \t" + String.Format("{0:0.###}", ckt.results[i].S12_1) + "<" + String.Format("{0:0.###}", ckt.results[i].S12_2) +
                      "  \t" + String.Format("{0:0.###}", ckt.results[i].S21_1) + "<" + String.Format("{0:0.###}", ckt.results[i].S21_2) +
                      "  \t" + String.Format("{0:0.###}", ckt.results[i].S22_1) + "<" + String.Format("{0:0.###}", ckt.results[i].S22_2) + "\n";
            }
        }

        // Data button hides data display giving more screen space to XY Chart
        private void btnData_Click(object sender, EventArgs e)
        {
            rtbDataDisplay.Visible = !rtbDataDisplay.Visible;
        }
    }
}
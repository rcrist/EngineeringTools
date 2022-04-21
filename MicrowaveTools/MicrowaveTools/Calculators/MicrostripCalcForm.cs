using MicrowaveTools.Components.Microstrip;
using System;
using System.Windows.Forms;

namespace MicrowaveTools.Calculators
{
    public partial class MicrostripCalcForm : Form
    {
        // Initialize substrate parameters
        double Er = 9.9;
        double H = 25;          // mils
        double T = 1.5;         // mils
        double Tand = 0.0002;
        double Rho = 1.72e-8 / 0.0000254;   // ohms/mil
        double D = 0.055;       // mil

        // Initialize microstrip parameters
        double W = 77;          // mils
        double L = 1000;        // mills
        double F = 2.0;         // GHz
        double sigma;   // S/m          

        double Zo, ErEff, ac, ad, Angle, SkinDepth;


        public MicrostripCalcForm()
        {
            InitializeComponent();

            sigma = 1.0 / Rho;
            Substrate subst = new Substrate(Er, H, T, Tand, Rho, D);
            Microstrip mlin = new Microstrip(subst, W, L, sigma);

            mlin.calcPropagation(F);

            // Get the results
            Zo = mlin.ZlEffFreq;
            ErEff = mlin.ErEffFreq;
            ac = mlin.ac_db;
            ad = mlin.ad_db;
            Angle = mlin.eLen * 180.0 / Math.PI;
            SkinDepth = mlin.skindepth;

            // Display the results
            tbZ0.Text = Zo.ToString();
            tbErEff.Text = ErEff.ToString();
            tbCondLoss.Text = ac.ToString();
            tbDielLoss.Text = ad.ToString();
            tbAngle.Text = Angle.ToString();
            tbSkinDepth.Text = SkinDepth.ToString();
        }
    }
}

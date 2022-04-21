// C# class libraries
using System;
using System.Diagnostics;
using System.Drawing;

// MathNet.Numerics math libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Microstrip
{
    class MOPEN : Comp
    {
        public double W = 1e-3f;     // Width
        public double L = 10e-3f;    // Length
        public Substrate subst;     // Substrate
        public double Temp = 26.85f; // Temperature
        public double z0 = 50;       // Reference impedance
        public double Z0 = 50;
        public double U0 = 4e-7 * Math.PI;
        public double C0 = 3e8;   // Speed of light

        public double er;
        public double h;
        public double t;
        public double tand;
        public double rho;
        public double D;
        public double ac, ad;
        public double ZlEff, ErEff, WEff, ZlEffFreq, ErEffFreq;
        public double alpha, beta, zl, ereff;
        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);

        public MOPEN()
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;
        }

        public MOPEN(float value, Point location, int[] nodes)
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;

            Type = "MOPEN";
            Value = value;
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
        }

        // Returns the microstrip open end capacitance.
        public double calcCend(double frequency, double W, double h, double t, double er) 
        {
            double ZlEff=0, ErEff=0, WEff=0, ZlEffFreq=0, ErEffFreq=0;
            MLIN mlin = new MLIN();
            mlin.calcQuasiStatic(W, h, t, er, ref ZlEff, ref ErEff, ref WEff);
            mlin.calcDispersion(WEff, h, er, ZlEff, ErEff, frequency, ref ZlEffFreq, ref ErEffFreq);

            W /= h;
            double dl = 0;

            /* Hammerstad */
            dl = 0.102 * (W + 0.106) / (W + 0.264) * (1.166 + (er + 1) / er* (0.9 + Math.Log(W + 2.475)));
            return dl * h * Math.Sqrt(ErEffFreq) / C0 / ZlEffFreq;
        }

        void calcSP(double frequency)
        {
            S[0,0] = ztor(1.0f / calcY(frequency));
        }

        Complex32 calcY(double frequency)
        {
            /* local variables */
            Complex32 y;
            double o = 2 * Math.PI * frequency;

            double c = calcCend(frequency, W, h, t, er);
            y = new Complex32(0, (float)(c * o));

            return y;
        }

        /*!\brief Converts impedance to reflexion coefficient
           \param[in] z impedance
           \param[in] zref normalisation impedance
           \return reflexion coefficient
        */
        Complex32 ztor(Complex32 zref) 
        {
            Complex32 z = new Complex32(50,0);
            return (z - zref) / (z + zref);
        }

        // Let the MOPEN draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + 10, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 10); // Location of MLIN rectangle
                Point p4 = new Point(p2.X + 40, p2.Y);
                //Point p5 = new Point(p4.X + 10, p4.Y);

                gr.DrawLine(drawPen, p1, p2);
                //gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 40, 20);

                // Create string to draw.
                String drawString = "MOPEN";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 5;
                float y = p1.Y - 30;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if (Orientation == "Shunt")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X, p1.Y + 10);
                Point p3 = new Point(p2.X - 10, p2.Y); // Location of MLIN rectangle
                Point p4 = new Point(p2.X, p2.Y + 40);
                //Point p5 = new Point(p4.X, p4.Y + 10);

                gr.DrawLine(drawPen, p1, p2);
                //gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 20, 40);

                // Create string to draw.
                String drawString = "MOPEN";

                // Create point for upper-left corner of drawing.
                float x = p1.X - 70;
                float y = p1.Y + 25;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }
    }
}

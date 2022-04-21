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
    class MVIA : Comp
    {
        public double R;
        public Complex32 Z;
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
        public double r;
        public double ac, ad;
        public double ZlEff, ErEff, WEff, ZlEffFreq, ErEffFreq;
        public double alpha, beta, zl, ereff;
        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);

        public MVIA()
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;
            r = D / 2;
        }

        public MVIA(float r, float z, Point location, int[] nodes)
        {
            R = r;
            Z = z;
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;

            Type = "MSTEP";
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
        }

        void initSP()
        {
            R = calcResistance();
        }

        void calcSP(double frequency)
        {
            // calculate s-parameters
            Z = calcImpedance(frequency);
            Complex32 z = new Complex32((float)(Z.Real / z0), (float)(Z.Imaginary / z0));
            S[0,0] = z / (z + 2.0f);
            S[1,1] = z / (z + 2.0f);
            S[0,1] = 2.0f / (z + 2.0f);
            S[1,0] = 2.0f / (z + 2.0f);
        }

        Complex32 calcImpedance(double frequency)
        {
            // check frequency validity
            if (frequency * h >= 0.03 * C0)
            {
                Debug.WriteLine("LOG_ERROR" + "WARNING: Model for microstrip via hole defined for " +           
                      "freq/C0*h < 0.03 (is %g)\n", frequency / C0 * h);
            }

            // create Z-parameter
            double fs = Math.PI * U0 * Math.Pow(t,2) / rho;
            double res = R * Math.Sqrt(1 + frequency * fs);
            double a = Math.Sqrt(Math.Pow(r,2) + Math.Pow(h,2));
            double ind = U0 * (h * Math.Log((h + a) / r) + 1.5 * (r - a));
            return Z = new Complex32((float)res, (float)(frequency * ind));
        }

        private double calcResistance()
        {
            double v = h / Math.PI / (Math.Pow(r,2) - Math.Pow(r - t, 2));
            return R = rho * v;
        }

        // Let the MGAP draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + 20, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 10);      // Location of outer circle
                Point p4 = new Point(p2.X + 5, p2.Y - 5);   // Location of inner circle
                Point p5 = new Point(p2.X + 15, p2.Y);
                Point p6 = new Point(p5.X + 25, p2.Y);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p5, p6);
                gr.DrawEllipse(drawPen, p3.X, p3.Y, 20, 20);
                gr.DrawEllipse(drawPen, p4.X, p4.Y, 10, 10);

                // Create string to draw.
                String drawString = "MVIA";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 8;
                float y = p1.Y - 30;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if (Orientation == "Shunt")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X, p1.Y + 20);
                Point p3 = new Point(p2.X - 10, p2.Y);      // Location of outer circle
                Point p4 = new Point(p2.X - 5, p2.Y + 5);   // Location of inner circle
                Point p5 = new Point(p2.X, p2.Y + 15);
                Point p6 = new Point(p5.X, p2.Y + 35);

                gr.DrawLine(drawPen, p1, p2);
                gr.DrawLine(drawPen, p5, p6);
                gr.DrawEllipse(drawPen, p3.X, p3.Y, 20, 20);
                gr.DrawEllipse(drawPen, p4.X, p4.Y, 10, 10);

                // Create string to draw.
                String drawString = "MVIA";

                // Create point for upper-left corner of drawing.
                float x = p1.X - 55;
                float y = p1.Y + 25;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }
    }
}

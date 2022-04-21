// C# class libraries
using System;
using System.Diagnostics;
using System.Drawing;

// MathNet.Numerics math libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;
using MicrowaveTools.Components.Microstrip;

namespace MicrowaveTools.Components.CPW
{
    class CPWOPEN : Comp
    {
        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(2, 2);
        public double W;
        public double s;
        public double g;
        public double len;
        public int backMetal = 1;
        public int approx = 0;

        public double er;
        public double h;
        public double t;
        public double tand;
        public double rho;
        public double D;

        double Z0 = 50;
        double z0 = 50;
        double C0 = 3e8; // Speed of light m/s
        double MU0 = 4e-7 * Math.PI;

        public CPWOPEN()
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;
        }

        public CPWOPEN(double w, double S, double l, double G, int BackMetal, int Approx, Point location, int[] nodes)
        {
            Substrate subst = new Substrate();
            er = subst.er;
            h = subst.h;
            t = subst.t;
            tand = subst.tand;
            rho = subst.rho;
            D = subst.d;

            Type = "MLIN";
            W = w;
            s = S;
            len = l;
            g = G;
            backMetal = BackMetal;
            approx = Approx;
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
        }
        double calcCend(double frequency)
        {
            double ZlEff=0, ErEff=0, ZlEffFreq=0, ErEffFreq=0;
            CPWLIN clin = new CPWLIN();
            clin.analyseQuasiStatic(W, s, h, t, er, backMetal, ref ZlEff, ref ErEff);
            clin.analyseDispersion(W, s, h, er, ZlEff, ErEff, frequency,
                             ref ZlEffFreq, ref ErEffFreq);
            double dl = (W / 2 + s) / 2;
            return dl * ErEffFreq / C0 / ZlEffFreq;
        }

        void initSP()
        {
            checkProperties();
        }

        void calcSP(double frequency)
        {
            S[0,0] = ztor(1.0f / calcY(frequency));
        }

        void checkProperties()
        {
            if (g <= W + s + s)
            {
                Debug.WriteLine("LOG_ERROR" + "WARNING: Model for coplanar open end valid for " +
                                "g > 2b (2b = %g)\n", W + s + s);
            }
           double ab = W / (W + s + s);
            if (ab < 0.2 || ab > 0.8)
            {
                Debug.WriteLine("LOG_ERROR" + "WARNING: Model for coplanar open end valid for " +
                                "0.2 < a/b < 0.8 (a/b = %g)\n", ab);
            }
        }

        Complex32 calcY(double frequency)
        {
            double o = 2 * Math.PI * frequency;
            double c = calcCend(frequency);
            return new Complex32(0, (float)(c * o));
        }

        /*!\brief Converts impedance to reflexion coefficient
        \param[in] z impedance
        \param[in] zref normalisation impedance
        \return reflexion coefficient
        */
        Complex32 ztor(Complex32 zref)
        {
            Complex32 z = new Complex32(50, 0);
            return (z - zref) / (z + zref);
        }

        // Let the CPWOPEN draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X + 10, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 10); // Location of rectangle
                Point p4 = new Point(p2.X + 40, p2.Y);
                //Point p5 = new Point(p4.X + 10, p4.Y);

                Point p6 = new Point(p2.X, p2.Y - 20);
                Point p7 = new Point(p6.X + 40, p6.Y);

                Point p8 = new Point(p2.X, p2.Y + 20);
                Point p9 = new Point(p8.X + 40, p8.Y);

                gr.DrawLine(drawPen, p1, p2);
                //gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 40, 20);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawLine(drawPen, p8, p9);

                // Create string to draw.
                String drawString = "CPWOPEN";

                // Create point for upper-left corner of drawing.
                float x = p1.X + 0;
                float y = p1.Y - 40;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if (Orientation == "Shunt")
            {
                Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
                Point p2 = new Point(p1.X, p1.Y + 10);
                Point p3 = new Point(p2.X - 10, p2.Y); // Location of rectangle
                Point p4 = new Point(p2.X, p2.Y + 40);
                //Point p5 = new Point(p4.X, p4.Y + 10);

                Point p6 = new Point(p2.X - 20, p2.Y);
                Point p7 = new Point(p6.X, p6.Y + 40);

                Point p8 = new Point(p2.X + 20, p2.Y);
                Point p9 = new Point(p8.X, p8.Y + 40);

                gr.DrawLine(drawPen, p1, p2);
                //gr.DrawLine(drawPen, p4, p5);
                gr.DrawRectangle(drawPen, p3.X, p3.Y, 20, 40);
                gr.DrawLine(drawPen, p6, p7);
                gr.DrawLine(drawPen, p8, p9);

                // Create string to draw.
                String drawString = "CPWOPEN";

                // Create point for upper-left corner of drawing.
                float x = p1.X - 100;
                float y = p1.Y + 25;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }

    }
}

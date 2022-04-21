// C# class libraries
using System;
using System.Diagnostics;
using System.Drawing;

// MathNet.Numerics math libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;
using MicrowaveTools.Circuits;

namespace MicrowaveTools.Components.Microstrip
{
    class MTEE : Comp
    {
        double Wa, Wb, W2;
        double h, er, t;
        MLIN lineA, lineB, line2;
        float Bt, La, Lb, L2, Ta2, Tb2, z0, Z0, C0;

        public Matrix<Complex32> S = Matrix<Complex32>.Build.Dense(3, 3);

        public MTEE(float w1, float w2, float w3, Point location, int[] nodes)
        {
            Substrate subst = new Substrate();
            Wa = w1;
            Wb = w2;
            W2 = w3;
            h = subst.h;
            er = subst.er;
            t = subst.t;

            Type = "MTEE";
            Orientation = "Series";
            Loc = location;
            Nodes = nodes;
            print();
            Width = 60;
            Height = 60;
            Pout = Loc;
            z0 = 50;
            Z0 = 50;
            C0 = 3e8f; // m/s - Speed of light
        }

        // Analysis initializer
        public override void initComp(float f)
        {
            //Y = calcMatrixY(f);
            //N = this.Nodes;
        }

        private void calcSP(float f)
        {
            lineA.L = La;
            lineB.L = Lb;
            line2.L = L2;
            lineA.calcSP(f);
            lineB.calcSP(f);
            line2.calcSP(f);

            // calculate S-parameters
            Complex32 n1 = Ta2 * new Complex32(1f + 1f / Tb2, Bt * z0);
            Complex32 n2 = Tb2 * new Complex32(1 + 1 / Ta2, Bt * z0);
            Complex32 n3 = new Complex32(1 / Ta2 + 1 / Tb2, Bt * z0);
            S[0, 0] = (1.0f - n1) / (1.0f + n1);
            S[1, 1] = (1.0f - n2) / (1.0f + n2);
            S[2, 2] = (1.0f - n3) / (1.0f + n3);
            S[0, 2] = 2.0f * (float)Math.Sqrt(Ta2) / (1.0f + n1);
            S[2, 0] = 2.0f * (float)Math.Sqrt(Ta2) / (1.0f + n1);
            S[1, 2] = 2.0f * (float)Math.Sqrt(Tb2) / (1.0f + n2);
            S[2, 1] = 2.0f * (float)Math.Sqrt(Tb2) / (1.0f + n2);
            S[0, 1] = 2.0f / ((float)Math.Sqrt(Ta2 * Tb2) * new Complex32(1f, Bt * z0) +
                             (float)Math.Sqrt(Ta2 / Tb2) + (float)Math.Sqrt(Tb2 / Ta2));
            S[1, 0] = 2.0f / ((float)Math.Sqrt(Ta2 * Tb2) * new Complex32(1f, Bt * z0) +
                             (float)Math.Sqrt(Ta2 / Tb2) + (float)Math.Sqrt(Tb2 / Ta2));
        }

        void calcPropagation(double f)
        {

            double Zla=0, Zlb=0, Zl2=0, Era=0, Erb=0, Er2=0;

            // computation of impedances and effective dielectric constants
            double ZlEff=0, ErEff=0, WEff=0;
            lineA.calcQuasiStatic(Wa, h, t, er, ref ZlEff, ref ErEff, ref WEff);
            lineA.calcDispersion(Wa, h, er, ZlEff, ErEff, f, ref Zla, ref Era);
            lineB.calcQuasiStatic(Wb, h, t, er, ref ZlEff, ref ErEff, ref WEff);
            lineB.calcDispersion(Wb, h, er, ZlEff, ErEff, f, ref Zlb, ref Erb);
            line2.calcQuasiStatic(W2, h, t, er, ref ZlEff, ref ErEff, ref WEff);
            line2.calcDispersion(W2, h, er, ZlEff, ErEff, f, ref Zl2, ref Er2);

            // local variables
            double Da, Db, D2, fpa, fpb, lda, ldb, da, db, d2, r, q;

            // equivalent parallel plate line widths
            Da = Z0 / Zla * h / Math.Sqrt(Era);
            Db = Z0 / Zlb * h / Math.Sqrt(Erb);
            D2 = Z0 / Zl2 * h / Math.Sqrt(Er2);

            // first higher order mode cut-off frequencies
            fpa = 0.4e6 * Zla / h;
            fpb = 0.4e6 * Zlb / h;

            // effective wavelengths of quasi-TEM mode
            lda = C0 / Math.Sqrt(Era) / f;
            ldb = C0 / Math.Sqrt(Erb) / f;

            // main arm displacements
            da = 0.055 * D2 * Zla / Zl2 * (1 - 2 * Zla / Zl2 * Math.Pow(f / fpa, 2));
            db = 0.055 * D2 * Zlb / Zl2 * (1 - 2 * Zlb / Zl2 * Math.Pow(f / fpb, 2));

            // length of lines in the main arms
            La = (float)(0.5 * W2 - da);
            Lb = (float)(0.5 * W2 - db);

            // displacement and length of line in the side arm
            r = Math.Sqrt(Zla * Zlb) / Zl2;
            q = Math.Pow(f,2) / fpa / fpb;
            d2 = Math.Sqrt(Da * Db) * (0.5 - r * (0.05 + 0.7 * Math.Exp(-1.6 * r) +
                              0.25 * r * q - 0.17 * Math.Log(r)));
            L2 = (float)(0.5 * Math.Max(Wa, Wb) - d2);

            // turn ratio of transformers in main arms
            Ta2 = (float)(1 - Math.PI * Math.Pow(f / fpa, 2) *
                  (Math.Pow(Zla / Zl2, 2) / 12 + Math.Pow(0.5 - d2 / Da, 2)));
            Tb2 = (float)(1 - Math.PI * Math.Pow(f / fpb, 2) *
                  (Math.Pow(Zlb / Zl2, 2) / 12 + Math.Pow(0.5 - d2 / Db, 2)));
            Ta2 = Math.Max(Ta2, Single.Epsilon);
            Tb2 = Math.Max(Tb2, Single.Epsilon);

            // shunt susceptance
            Bt = (float)(5.5 * Math.Sqrt(Da * Db / lda / ldb) * (er + 2) / er /
              Zl2 / Math.Sqrt(Ta2 * Tb2) * Math.Sqrt(da * db) / D2 *
              (1 + 0.9 * Math.Log(r) + 4.5 * r * q - 4.4 * Math.Exp(-1.3 * r) -
               20 * Math.Pow(Zl2 / Z0,2)));
        }

        /* This function can be used to create an extra microstrip circuit.
        If the 'line' argument is NULL then the new circuit is created, the
        nodes get re-arranged and it is inserted into the given
        netlist. The given arguments can be explained as follows.
        base:     calling circuit (this)
        line:     additional microstrip line circuit (can be NULL)
        subnet:   the netlist object
        c:        name of the additional circuit
        n:        name of the inserted (internal) node
        Internal: number of new internal node (the original external node) */
        private Comp splitMicrostrip(Comp line, Circuit subnet, String c, String n, int Internal)
        {
            if (line == null)
            {
                line = new MLIN();
                String name = this.Name;
                String node = this.Name;
                line.Name = name;
                line.Nodes[0] = this.Nodes[0];
                line.Nodes[1] = this.Nodes[1] + 1;
                subnet.comps.Add(line);
            }
            this.Nodes[1] = line.Nodes[1];
            return line;
        }

        /* This function is the counterpart of the above routine.  It removes
       the microstrip circuit from the netlist and re-assigns the original
       node. */
       void disableMicrostrip(Comp line, Circuit subnet, int Internal) 
        {
           if (line != null) 
           {
                subnet.comps.Clear();
                this.Nodes[1] = line.Nodes[0];
           }
        }

        // Let the MLIN draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            if (Orientation == "Series")
            {
                Point[] p = new Point[21];
                p[1] = Loc;            // Assume p1 is the input lead to the left
                p[2] = new Point(p[1].X + 10, p[1].Y);
                p[3] = new Point(p[2].X, p[2].Y - 10);
                p[4] = new Point(p[3].X + 10, p[3].Y);
                p[5] = new Point(p[4].X, p[4].Y - 10);
                p[6] = new Point(p[5].X + 20, p[5].Y);
                p[7] = new Point(p[6].X, p[6].Y + 10);
                p[8] = new Point(p[7].X + 10, p[7].Y);
                p[9] = new Point(p[8].X, p[8].Y + 20);
                p[14] = new Point(p[9].X - 40, p[9].Y);
                p[15] = new Point(p[5].X + 10, p[5].Y - 10);
                p[16] = new Point(p[15].X, p[15].Y + 10);
                p[17] = new Point(p[8].X + 10, p[8].Y + 10);
                p[18] = new Point(p[17].X - 10, p[17].Y);

                for (int i = 1; i < 9; i++)
                    gr.DrawLine(drawPen, p[i], p[i + 1]);
                gr.DrawLine(drawPen, p[9], p[14]);
                gr.DrawLine(drawPen, p[14], p[2]);
                for (int i = 15; i < 19; i += 2)
                    gr.DrawLine(drawPen, p[i], p[i + 1]);


                // Create string to draw.
                String drawString = "MTEE";

                // Create point for upper-left corner of drawing.
                float x = p[1].X + 10;
                float y = p[1].Y + 20;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
            else if(Orientation == "Shunt")
            {
                Point[] p = new Point[21];
                p[1] = Loc;            // Assume p1 is the input lead to the left
                p[2] = new Point(p[1].X + 10, p[1].Y);
                p[3] = new Point(p[2].X, p[2].Y - 10);
                p[8] = new Point(p[3].X + 40, p[3].Y);
                p[9] = new Point(p[8].X, p[8].Y + 20);
                p[10] = new Point(p[9].X - 10, p[9].Y);
                p[11] = new Point(p[10].X, p[10].Y + 10);
                p[12] = new Point(p[11].X - 20, p[11].Y);
                p[13] = new Point(p[12].X, p[12].Y - 10);
                p[14] = new Point(p[13].X - 10, p[13].Y);
                p[17] = new Point(p[8].X + 10, p[8].Y + 10);
                p[18] = new Point(p[17].X - 10, p[17].Y);
                p[19] = new Point(p[11].X - 10, p[11].Y + 10);
                p[20] = new Point(p[19].X, p[19].Y - 10);

                for (int i = 1; i < 3; i++)
                    gr.DrawLine(drawPen, p[i], p[i + 1]);
                for (int i = 8; i < 14; i++)
                    gr.DrawLine(drawPen, p[i], p[i + 1]);
                for (int i = 17; i < 21; i += 2)
                    gr.DrawLine(drawPen, p[i], p[i + 1]);
                gr.DrawLine(drawPen, p[3], p[8]);
                gr.DrawLine(drawPen, p[14], p[2]);

                // Create string to draw.
                String drawString = "MTEE";

                // Create point for upper-left corner of drawing.
                float x = p[1].X + 10;
                float y = p[1].Y - 30;

                // Draw string to screen.
                gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }
        }
    }
}
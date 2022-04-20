// RLCCKT1
// Name: Rick A. Crist
// Date: Mar 2022
// Purpose: C++ Microwave Linear Simulation for RLC Network in "Microwave Circuit
// Design Using Programmable Calculators", pg. 27.
//
// Pin >--- 1 --- R=75 ---- 2 ---- L=5 --- 3 --- C=1 ---< Pout
//
// * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
//
// *********************** Circuit analysis process ***********************
// 1) Define default units
// 2) Deine the frequency range
// 3) Define the circuit as a netlist
// 4) Add each component admittance Y-matrix to circuit Y-matrix
// 5) Reduce circuit Y-matrix to N-port Y-matrix
// 6) Convert Y-matrix to S-matrix
// 7) Repeat steps 4-6 for each frequency
// 8) Display the results on plot or/and data table
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace YMatrixConsole
{
    class Program
    {

        public class Comp
        {
            // Global units variables
            public static float GHz = 1e9f;
            public static float nH = 1e-9f;
            public static float pF = 1e-12f;

            public Matrix<Complex32> Y;
            public int[] N;
        }

        public class Resistor : Comp
        {
            public Resistor(int[] nodes, float res)
            {
                Matrix<Complex32> Yres = Matrix<Complex32>.Build.Dense(2, 2);
                Yres[0, 0] = 1;
                Yres[0, 1] = -1;
                Yres[1, 0] = -1;
                Yres[1, 1] = 1;

                Yres = Yres / res; // Won't work with a double, must be a float
                Y = Yres;
                N = nodes;
            }
        }

        public class Inductor : Comp
        {
            public Inductor(int[] nodes, float ind, float f)
            {
                Matrix<Complex32> Yind = Matrix<Complex32>.Build.Dense(2, 2);
                Yind[0, 0] = 1;
                Yind[0, 1] = -1;
                Yind[1, 0] = -1;
                Yind[1, 1] = 1;

                Complex32 denom = new Complex32(0, (float)(2 * Constants.Pi * f * ind));
                Yind = Yind / denom; // Won't work with a double, must be a float
                Y = Yind;
                N = nodes;
            }
        }

        public class Capacitor : Comp
        {
            public Capacitor(int[] nodes, float cap, float f)
            {
                Matrix<Complex32> Ycap = Matrix<Complex32>.Build.Dense(2, 2);
                Ycap[0, 0] = 1;
                Ycap[0, 1] = -1;
                Ycap[1, 0] = -1;
                Ycap[1, 1] = 1;

                Complex32 denom = new Complex32(0, (float)(-1 / (2 * Constants.Pi * f * cap)));
                Ycap = Ycap / denom; // Won't work with a double, must be a float
                Y = Ycap;
                N = nodes;
            }
        }

        public class Circuit
        {
            public List<Comp> netlist = new List<Comp>();
            public Matrix<Complex32> Y = Matrix<Complex32>.Build.Dense(0,0);
            public Matrix<Complex32> Yreduced;
            public Matrix<Complex32> S;

            public void UpdateY(Comp C)
            {
                if (Y.RowCount == 0) // Y is empty
                {
                    Y = C.Y;
                }
                else
                {
                    Y = Y.Resize(Y.RowCount+1, Y.ColumnCount+1);

                    Y[C.N[0] - 1, C.N[0] - 1] = Y[C.N[0] - 1, C.N[0] - 1] + C.Y[0, 0];
                    Y[C.N[0] - 1, C.N[1] - 1] = Y[C.N[0] - 1, C.N[1] - 1] + C.Y[0, 1];
                    Y[C.N[1] - 1, C.N[0] - 1] = Y[C.N[1] - 1, C.N[0] - 1] + C.Y[1, 0];
                    Y[C.N[1] - 1, C.N[1] - 1] = Y[C.N[1] - 1, C.N[1] - 1] + C.Y[1, 1];
                }
            }

            public void ShuffleY(int[] extN)
            {
                var M = Matrix<Complex32>.Build;
                var V = Vector<Complex32>.Build;

                Matrix<Complex32> Ytmp = Matrix<Complex32>.Build.Dense(0, 0);
                Ytmp = Ytmp.Resize(Y.RowCount, Y.ColumnCount);
                Vector<Complex32> Vtmp = Vector<Complex32>.Build.Dense(Y.ColumnCount);

                int[] allN = new int[0];
                int[] intN = new int[0];

                Array.Resize(ref allN, Y.RowCount);
                for (int i = 1; i < allN.Length + 1; i++)
                {
                    allN[i - 1] = i;
                }

                bool foundFlag;
                for (int i = 1; i < allN.Length; i++)
                {
                    foundFlag = false;
                    for (int j = 1; j < extN.Length; j++)
                    {
                        if (allN[i - 1] == extN[j - 1]) foundFlag = true;
                    }
                    if (!foundFlag)
                    {
                        Array.Resize(ref intN, intN.Length + 1);
                        intN[intN.Length - 1] = allN[i - 1];
                    }
                }
                var swapN = new int[extN.Length + intN.Length];
                extN.CopyTo(swapN, 0);
                intN.CopyTo(swapN, extN.Length);

                // Swap the rows
                for (int i = 0; i < swapN.Length; i++)
                {
                    Vtmp=V.DenseOfVector(Y.Row(swapN[i] - 1));
                    Ytmp.SetRow(i, Vtmp);
                }
                Y = M.DenseOfMatrix(Ytmp);

                // Swap the cols
                for (int i = 0; i < swapN.Length; i++)
                {
                    Vtmp = V.DenseOfVector(Y.Column(swapN[i] - 1));
                    Ytmp.SetColumn(i, Vtmp);
                }
                Y = M.DenseOfMatrix(Ytmp);
            }

            public void ReduceY(int[] extN)
            {
                var M = Matrix<Complex32>.Build;
                var V = Vector<Complex32>.Build;

                int nExtN = extN.Length;
                int nIntN = Y.RowCount - nExtN;
                Matrix<Complex32> Yee = Matrix<Complex32>.Build.Dense(nExtN, nExtN);
                Matrix<Complex32> Yei = Matrix<Complex32>.Build.Dense(nExtN, nIntN);
                Matrix<Complex32> Yie = Matrix<Complex32>.Build.Dense(nIntN, nExtN);
                Matrix<Complex32> Yii = Matrix<Complex32>.Build.Dense(nIntN, nIntN);

                //Yee = Y.topLeftCorner(nExtN, nExtN);
                //Yei = Y.block(0, nExtN, nIntN, nIntN);
                //Yie = Y.bottomLeftCorner(nIntN, nExtN);
                //Yii = Y.block(nExtN, nExtN, nIntN, nIntN);

                Yee = M.DenseOfMatrix(Y.SubMatrix(0, nExtN, 0, nExtN)); // SubMatrix(int rowIndex, int rowCount, int columnIndex, int columnCount)
                Yei = M.DenseOfMatrix(Y.SubMatrix(0, nExtN, nIntN, nIntN));
                Yie = M.DenseOfMatrix(Y.SubMatrix(nIntN, nExtN, 0, nExtN));
                Yii = M.DenseOfMatrix(Y.SubMatrix(nExtN, nExtN, nIntN, nIntN));

                Yreduced = Yee - (Yei * Yii.Inverse() * Yie);
            }

            public void ConvertY()
            {
                var M = Matrix<Complex32>.Build;
                var V = Vector<Complex32>.Build;

                Matrix<Complex32> I = Matrix<Complex32>.Build.Dense(2, 2);
                I = DiagonalMatrix.CreateIdentity(2);
                Matrix<Complex32> Yo = Matrix<Complex32>.Build.Dense(2, 2);
                float Zo = 50.0f;
                Yo = I * 1.0f / Zo;

                Matrix<Complex32> Stemp1 = M.Dense(2, 2);
                Stemp1 = M.DenseOfMatrix(Yo + Yreduced);
                Stemp1 = Stemp1.Inverse();

                Matrix<Complex32> Stemp2 = M.Dense(2, 2);
                Stemp2 = M.DenseOfMatrix((Yo - Yreduced));

                S = M.DenseOfMatrix(Stemp1 * Stemp2);
            }

            public void printVector(int[] vec)
            {
                for (int i = 0; i < vec.Length; i++)
                    Console.WriteLine(vec[i]);
            }
        }

        static void Main(string[] args)
        {
            // *********************** FREQUENCY ***********************
            float f = 2.0f * Comp.GHz;

            // *********************** NETLIST ***********************
            Circuit ckt = new Circuit();

            // Define the resistor
            int[] resNodes = new int[] { 1, 2 };
            Resistor r1 = new Resistor(resNodes, 75.0f);
            ckt.netlist.Add(r1);

            // Define the inductor
            int[] indNodes = new int[] { 2, 3 };
            Inductor l1 = new Inductor(indNodes, 5.0f * Comp.nH, f);
            ckt.netlist.Add(l1);

            //Define the capacitor
            int[] capNodes = new int[] { 3, 4 };
            Capacitor c1 = new Capacitor(capNodes, 1.0f * Comp.pF, f);
            ckt.netlist.Add(c1);

            // *********************** Create Y-Matrix ***********************
            foreach (Comp C in ckt.netlist)
            {
                ckt.UpdateY(C);
            }

            // Define external nodes
            int[] extNodes = new int[] { 1, 4 };
            ckt.ShuffleY(extNodes);
            ckt.ReduceY(extNodes);
            ckt.ConvertY();

            // *********************** Display Results ***********************
            Console.WriteLine("f\t" + "S11\t\t\t" + "S12\t\t\t" + "S21\t\t\t" + "S22");
            Console.WriteLine(f + "  \t" + String.Format("{0:0.###}", ckt.S[0, 0]) + 
                                  "  \t" + String.Format("{0:0.###}", ckt.S[0, 1]) + 
                                  "  \t" + String.Format("{0:0.###}", ckt.S[1, 0]) + 
                                  "  \t" + String.Format("{0:0.###}", ckt.S[1, 1]));

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}

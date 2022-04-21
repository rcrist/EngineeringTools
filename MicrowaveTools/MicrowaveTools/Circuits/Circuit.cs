// .NET class libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;

// MathNet.Numerics math libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

// Microwave Tools class libraries
using MicrowaveTools.Components;
using MicrowaveTools.Components.Ideal;
using MicrowaveTools.Wires;

namespace MicrowaveTools.Circuits
{
    public class Circuit
    {
        public List<Comp> comps = new List<Comp>();
        public Matrix<Complex32> Y = Matrix<Complex32>.Build.Dense(0, 0);
        public Matrix<Complex32> Yreduced;
        public Matrix<Complex32> S;
        public int[] extN = new int[2];

        public List<ResultRecord> results = new List<ResultRecord>();
        public struct ResultRecord
        {
            public float f;
            public double S11_1;
            public double S11_2;
            public double S12_1;
            public double S12_2;
            public double S21_1;
            public double S21_2;
            public double S22_1;
            public double S22_2;
        }

        // Traverse assigns nodes to components in preparation for S-parameter analysis
        public void Traverse()
        {
            int nodeCount = 0;

            Debug.WriteLine("Traverse Started");
            foreach (Comp comp in this.comps)
            {
                foreach (Wire wire in comp.wires)
                {
                    nodeCount++;
                    wire.Cin.Nodes[1] = nodeCount;  // Assign output node - Cin.Nodes[1] - to input component - Cin
                    wire.Cout.Nodes[0] = nodeCount; // Assign input node - Cout.Nodes[0] - to output component - Cout
                }
                comp.print(); // Print the component with the assigned nodes
            }
            Debug.WriteLine("Traverse Ended");
        }

        public void AnalyzeCircuit(float f)
        {
            // float f = 2e9f;

            // Initialize components and extNode list
            foreach (Comp comp in this.comps)
            {
                comp.initComp(f);

                if (comp is InPort)
                {
                    extN[0] = comp.Nodes[1]; // Nodes[1] is the output port of a 2-port component
                }
                if (comp is OutPort)
                {
                    extN[1] = comp.Nodes[0]; // Nodes[0] is the input port of a 2-port component
                }
            }

            // *********************** Create Y-Matrix ***********************
            foreach (Comp C in this.comps)
            {
                if (!(C is InPort) && !(C is OutPort))
                    UpdateY(C);
            }

            //Debug.WriteLine("this.comps.Count: " + this.comps.Count);
            if (this.comps.Count > 3)
            {
                ShuffleY();
                ReduceY();
            }
            else
                Yreduced = Y;
            ConvertY();
        }

        public void UpdateY(Comp C)
        {
            var M = Matrix<Complex32>.Build;
            var V = Vector<Complex32>.Build;

            //Debug.WriteLine("Y Size: " + Y.RowCount.ToString());

            if (Y.RowCount.ToString() == "0") // Y is empty
            {
                Y = M.DenseOfMatrix(C.Y);
            }
            else
            {
                Y = Y.Resize(Y.RowCount + 1, Y.ColumnCount + 1);

                Y[C.N[0] - 1, C.N[0] - 1] = Y[C.N[0] - 1, C.N[0] - 1] + C.Y[0, 0];
                Y[C.N[0] - 1, C.N[1] - 1] = Y[C.N[0] - 1, C.N[1] - 1] + C.Y[0, 1];
                Y[C.N[1] - 1, C.N[0] - 1] = Y[C.N[1] - 1, C.N[0] - 1] + C.Y[1, 0];
                Y[C.N[1] - 1, C.N[1] - 1] = Y[C.N[1] - 1, C.N[1] - 1] + C.Y[1, 1];
            }
        }

        public void ShuffleY()
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
                Vtmp = V.DenseOfVector(Y.Row(swapN[i] - 1));
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

        public void ReduceY()
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

        public void printCircuit()
        {
            foreach (Comp comp in comps)
            {
                Console.WriteLine(comp.GetType().Name);
            }
        }
    }
}

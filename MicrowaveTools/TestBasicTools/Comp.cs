using System;
using System.Collections.Generic;
using System.Drawing;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace TestBasicTools
{

    public class Comp
    {
        // Protected variables - available to derived subclasses
        protected int compSize = 60;
        protected int halfCompSize = 30;
        protected int leadLength = 10;

        // public variables
        public List<Wire> wires = new List<Wire>();
        public int[] Nodes;
        public string Type;
        public float Value;
        public Point Location;

        // Analysis variables
        public Matrix<Complex32> Y;
        public int[] N;

        // Polymorphism virtual methods used in foreach(Comp comp in ckt.comps) iteration
        public virtual void print() { /* Do noting */ }
        public virtual void initComp(float f) { /* Do noting */ }
        public virtual void Draw(Graphics gr) { /* Do noting */ }
    }
}

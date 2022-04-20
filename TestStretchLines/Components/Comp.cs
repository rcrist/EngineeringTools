// C# Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

// Microwave Tools Libraries
using MicrowaveTools.Wires;

namespace MicrowaveTools.Components
{
    public class Comp
    {
        // Component location, rotation, and size variables
        public Point Loc;
        public double Value;
        public float angle = 0.0F;
        public int Width, Height;

        // Component rotation flag
        public bool isRotated = false;

        // Component bounding box for hit test
        public Rectangle boundBox = new Rectangle();

        // Component pens
        protected Pen drawPen = new Pen(Color.Black, 1);
        protected Pen redPen = new Pen(Color.Red, 1);

        // Component String variables
        protected FontFamily family = new FontFamily("Arial");
        protected int fontStyle = (int)FontStyle.Regular;
        protected int emSize = 12;
        protected StringFormat format = StringFormat.GenericDefault;

        // Component text variables
        protected String compText;
        protected Point pt;

        // Protected variables - available to derived subclasses
        protected int compSize = 60;
        protected int halfCompSize = 30;
        protected int leadL = 10;  // Length of input and output leads
        protected int bodyL = 40;  // Length of the component body (without leads)
        protected int compL = 60;  // Length of the component with leads

        // public variables
        public List<Wire> wires = new List<Wire>();
        public Point Pin;
        public Point Pout;

        // End caps variables
        protected const int endcap_radius = 3;
        protected bool endcapsVisible = false;

        // Selection flag
        public bool isSelected = false;

        // Constructors
        public Comp()
        {

        }

        // Polymorphism: Virtual methods used in circuit component iteration
        public virtual void Draw(Graphics gr) { /* Do nothing */ }

        public void checkSelect()
        {
            if (isSelected)
                drawPen = new Pen(Color.Red);
            else
                drawPen = new Pen(Color.Black);
        }

        public void drawSelectRect(Graphics gr, Point p1)
        {
            if (isSelected)
            {
                Rectangle rect1 = new Rectangle(
                     p1.X - endcap_radius, p1.Y - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect1);     // Rectangular end cap
            }
        }
    }
}
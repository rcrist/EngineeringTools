﻿// C# Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

// Microwave Tools Libraries
using TestDelete.Wires;

namespace TestDelete.Components
{
    public class Comp
    {
        // Protected property variables for Property Grid
        protected String _orientation = "Series";
        protected String _type;
        protected String _name;
        protected float _value;
        protected Point _loc;
        protected int[] _nodes;
        protected int _width = 60;
        protected int _height = 40;

        // Orientation property with category attribute and description attribute added   
        [CategoryAttribute("Location Settings"), DescriptionAttribute("Orientation of the Component")]
        public string Orientation
        {
            get
            {
                return _orientation;
            }
            set
            {
                _orientation = value;
            }
        }

        // Location property with category attribute and description attribute added   
        [CategoryAttribute("Location Settings"), DescriptionAttribute("Orientation of the Component")]
        public Point Loc
        {
            get
            {
                return _loc;
            }
            set
            {
                _loc = value;
            }
        }

        // Width property with category attribute and description attribute added   
        [CategoryAttribute("Location Settings"), DescriptionAttribute("Orientation of the Component")]
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        // Location property with category attribute and description attribute added   
        [CategoryAttribute("Location Settings"), DescriptionAttribute("Orientation of the Component")]
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        // Value property with category attribute and description attribute added   
        [CategoryAttribute("Configuration Settings"), DescriptionAttribute("Configuration of the Component")]
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        // Type property with category attribute and description attribute added   
        [CategoryAttribute("Configuration Settings"), DescriptionAttribute("Configuration of the Component")]
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        // Type property with category attribute and description attribute added   
        [CategoryAttribute("Configuration Settings"), DescriptionAttribute("Configuration of the Component")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        // Type property with category attribute and description attribute added   
        [CategoryAttribute("Configuration Settings"), DescriptionAttribute("Configuration of the Component")]
        public int[] Nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                _nodes = value;
            }
        }

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

        //Components draw variables
        public Pen drawPen = new Pen(Color.Black);
        public Pen selectPen = new Pen(Color.Red);

        // Component text variables
        // Create font and brush.
        private Font drawFont = new Font("Arial", 10);
        private SolidBrush drawBrush = new SolidBrush(Color.Black);
        private StringFormat drawFormat = new StringFormat();

        // Component text variables
        protected String compText;
        protected Point pt;

        // Polymorphism: Virtual methods used in circuit component iteration
        public virtual void print() { /* Do noting */ }
        public virtual void Draw(Graphics gr) { /* Do noting */ }

        public virtual void drawCompText(Graphics gr, Point p1, String drawString)
        {
            // Convert Point ints to floats
            float x = p1.X;
            float y = p1.Y;

            // Draw string to screen.
            gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        public void checkSelect()
        {
            if (this.isSelected)
                this.drawPen = new Pen(Color.Red);
            else
                this.drawPen = new Pen(Color.Black);
        }

        public void drawSelectRect(Graphics gr, Point p1)
        {
            if (this.isSelected)
            {
                Rectangle rect1 = new Rectangle(
                     p1.X - endcap_radius, p1.Y - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect1);     // Rectangular end cap
            }
        }
    }
}
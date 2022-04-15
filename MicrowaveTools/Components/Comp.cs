using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace MicrowaveTools.Components
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
        protected int _width;
        protected int _height;

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
        public Point Pin;
        public Point Pout;

        //Components draw variables
        public Pen drawPen = new Pen(Color.White);

        // Polymorphism: Virtual methods used in circuit component iteration
        public virtual void print() { /* Do noting */ }
        public virtual void Draw(Graphics gr) { /* Do noting */ }
    }
}

using MicrowaveTools.Wires;
using System;
using System.Collections.Generic;
using System.Drawing;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;
using System.Diagnostics;
using System.ComponentModel;

namespace MicrowaveTools.Components
{
    // Base class for all component classes
    public class Comp
    {
        // Protected propert variables for Property Grid
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

        // Protected units
        protected float nH = 1e-9f;
        protected float pF = 1e-12f;

        // Public Analysis variables
        public Matrix<Complex32> Y; // Analysis Y-matrix
        public int[] N;             // Analysis node list

        // public variables
        public List<Wire> wires = new List<Wire>();
        public Point Pin;
        public Point Pout;

        //Components draw variables
        public Pen drawPen = new Pen(Color.White);

        // Component text variables
        // Create font and brush.
        public Font drawFont = new Font("Arial", 10);
        public SolidBrush drawBrush = new SolidBrush(Color.White);
        public StringFormat drawFormat = new StringFormat();

        public Comp()
        {
            Width = compSize;
            Height = compSize;
        }

        public Comp(Point loc)
        {
            Loc = Loc;
        }

        // Polymorphism: Virtual methods used in circuit component iteration
        // Iterate over the circuit component list using foreach(Comp comp in ckt.comps) 
        public virtual void print() { /* Do noting */ }
        public virtual void initComp(float f) { /* Do noting */ }
        public virtual void Draw(Graphics gr) { /* Do noting */ }

        // Draw a 1 component lumped element in series - Resistor, Inductor, Capacitor
        public void drawSeriesLump1(Graphics gr, String compType, Point loc, String compString)
        {
            // p1 - input lead - p2 - comp body - p3 - output lead - p4, p5 - comp label location
            Point p1 = loc;                                         // Start point - input lead
            Point p2 = new Point(loc.X + leadL, loc.Y);             // End point - input lead & Start point - comp body
            Point p3 = new Point(loc.X + bodyL + leadL, loc.Y);     // End point - comp body & Start point - output lead
            Point p4 = new Point(loc.X + compL, loc.Y);             // End point - output lead
            Point p5 = new Point(p2.X - 5, p3.Y - 35);              // Text location point

            drawLead(gr, p1, p2); // Draw input lead
            drawLead(gr, p3, p4); // Draw output lead

            if (compType == "Res")
                drawSeriesResBody(gr, p2);      // Draw series resistor body
            else if (compType == "Ind")
                drawSeriesIndBody(gr, p2);      // Draw series inductor body
            else if (compType == "Cap")
                drawSeriesCapBody(gr, p2);      // Draw series capacitor body

            drawCompText(gr, p5, compString); // Draw a label over the component body
        }

        // Draw a 1 component lumped element in shunt - Resistor, Inductor, Capacitor
        public void drawShuntLump1(Graphics gr, String compType, Point loc, String compString)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)
            int compL = 60;  // Length of the component with leads

            // p1 - input lead - p2 - comp body - p3 - output lead - p4
            // p5 - comp label location
            Point p1 = loc;                                         // Start point - input lead
            Point p2 = new Point(loc.X, loc.Y + leadL);             // End point - input lead & Start point - comp body
            Point p3 = new Point(loc.X, loc.Y + bodyL + leadL);     // End point - comp body & Start point - output lead
            Point p4 = new Point(loc.X, loc.Y + compL);             // End point - output lead
            Point p5 = new Point(p2.X + 20, p2.Y + leadL);          // Label location point

            drawLead(gr, p1, p2); // Draw input lead
            drawLead(gr, p3, p4); // Draw output lead

            if (compType == "Res")
                drawShuntResBody(gr, p2);      // Draw series resistor body
            else if (compType == "Ind")
                drawShuntIndBody(gr, p2);      // Draw series inductor body
            else if (compType == "Cap")
                drawShuntCapBody(gr, p2);      // Draw series capacitor body

            drawCompText(gr, p5, compString); // Draw a label over the component body
        }

        // Draw a 2 component lumped element - Series SRC, SRL, SLC
        public void drawSxxSeriesLump2(Graphics gr, String compType, Point loc, String compString1, String compString2)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)
            int compL = 60;  // Length of the component with leads

            // p1 - lead1 - p2 - comp1 body - p3 - lead2 - p4 - comp2 body - p5 - lead3 - p6
            // p7 - comp1 label, p8 - comp2 label
            Point p1 = loc;                                       // Start point - lead1
            Point p2 = new Point(p1.X + leadL, p1.Y);             // End point - lead1 & Start point - comp1 body
            Point p3 = new Point(p2.X + bodyL, p2.Y);             // End point - comp1 body & Start point - lead2
            Point p4 = new Point(p3.X + leadL, p3.Y);             // End point - lead2 & Start point - comp2 body
            Point p5 = new Point(p4.X + bodyL, p4.Y);             // End Point - comp2 body & Start point - lead3
            Point p6 = new Point(p5.X + leadL, p5.Y);             // End Point - lead3
            Point p7 = new Point(p2.X - 5, p3.Y - 35);            // Label1 location point
            Point p8 = new Point(p2.X + 50, p3.Y - 35);           // Label2 location point

            drawLead(gr, p1, p2); // Draw lead1
            drawLead(gr, p3, p4); // Draw lead2
            drawLead(gr, p5, p6); // Draw lead3

            if (compType == "SRL")
            {
                drawSeriesResBody(gr, p2);      // Draw series resistor body
                drawSeriesIndBody(gr, p4);      // Draw series inductor body
            }
            else if (compType == "SRC")
            {
                drawSeriesResBody(gr, p2);      // Draw series resistor body
                drawSeriesCapBody(gr, p4);      // Draw series capacitor body
            }
            else if (compType == "SLC")
            {
                drawSeriesIndBody(gr, p2);      // Draw series inductor body
                drawSeriesCapBody(gr, p4);      // Draw series capacitor body
            }

            drawCompText(gr, p7, compString1);
            drawCompText(gr, p8, compString2);
        }

        // Draw a 2 component lumped element - Shunt SRC, SRL, SLC
        public void drawSxxShuntLump2(Graphics gr, String compType, Point loc, String compString1, String compString2)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)
            int compL = 60;  // Length of the component with leads

            // p1 - lead1 - p2 - comp1 body - p3 - lead2 - p4 - comp2 body - p5 - lead3 - p6
            // p7 - comp1 label, p8 - comp2 label
            Point p1 = loc;                                       // Start point - lead1
            Point p2 = new Point(p1.X, p1.Y + leadL);             // End point - lead1 & Start point - comp1 body
            Point p3 = new Point(p2.X, p2.Y + bodyL);             // End point - comp1 body & Start point - lead2
            Point p4 = new Point(p3.X, p3.Y + leadL);             // End point - lead2 & Start point - comp2 body
            Point p5 = new Point(p4.X, p4.Y + bodyL);             // End Point - comp2 body & Start point - lead3
            Point p6 = new Point(p5.X, p5.Y + leadL);             // End Point - lead3
            Point p7 = new Point(p2.X + 20, p2.Y + 10);            // Label1 location point
            Point p8 = new Point(p4.X + 20, p4.Y + 10);           // Label2 location point

            drawLead(gr, p1, p2); // Draw lead1
            drawLead(gr, p3, p4); // Draw lead2
            drawLead(gr, p5, p6); // Draw lead3

            if (compType == "SRL")
            {
                drawShuntResBody(gr, p2);      // Draw resistor body
                drawShuntIndBody(gr, p4);      // Draw inductor body
            }
            else if (compType == "SRC")
            {
                drawShuntResBody(gr, p2);      // Draws resistor body
                drawShuntCapBody(gr, p4);      // Draw capacitor body
            }
            else if (compType == "SLC")
            {
                drawShuntIndBody(gr, p2);      // Draw inductor body
                drawShuntCapBody(gr, p4);      // Draw capacitor body
            }

            drawCompText(gr, p7, compString1);
            drawCompText(gr, p8, compString2);
        }

        // Draw a 2 component lumped element - Series PRC, PRL, PLC
        public void drawPxxSeriesLump2(Graphics gr, String compType, Point loc, String compString1, String compString2)
        {
            {
                int leadL = 10;  // Length of input and output leads
                int bodyL = 40;  // Length of the component body (without leads)

                // Define points for input lead branch
                Point p1 = loc;
                Point p2 = new Point(p1.X + leadL, p1.Y);
                Point p3 = new Point(p2.X, p2.Y - 20);
                Point p4 = new Point(p3.X + leadL, p3.Y);
                Point p5 = new Point(p2.X, p2.Y + 20);
                Point p6 = new Point(p5.X + leadL, p5.Y);

                // Define points for output lead branch
                Point p7 = new Point(p4.X + bodyL, p4.Y);
                Point p8 = new Point(p7.X + leadL, p7.Y);
                Point p9 = new Point(p6.X + bodyL, p6.Y);
                Point p10 = new Point(p9.X + leadL, p9.Y);
                Point p11 = new Point(p8.X, p8.Y + 20);
                Point p12 = new Point(p11.X + leadL, p11.Y);

                // Define points for comp labels
                Point p13 = new Point(p1.X + leadL, p1.Y - bodyL - leadL - 5);
                Point p14 = new Point(p1.X + leadL, p1.Y + bodyL - 5);

                if (compType == "PRL")
                {
                    drawSeriesResBody(gr, p6);      // Draw resistor body
                    drawSeriesIndBody(gr, p4);      // Draw inductor body
                }
                else if (compType == "PRC")
                {
                    drawSeriesResBody(gr, p4);      // Draws resistor body
                    drawSeriesCapBody(gr, p6);      // Draw capacitor body
                }
                else if (compType == "PLC")
                {
                    drawSeriesIndBody(gr, p4);      // Draw inductor body
                    drawSeriesCapBody(gr, p6);      // Draw capacitor body
                }

                drawSeriesLead2(gr, p1); // Draw input and output leads

                drawCompText(gr, p13, compString1);
                drawCompText(gr, p14, compString2);
            }
        }

        // Draw a 2 component lumped element - Shunt PRC, PRL, PLC
        public void drawPxxShuntLump2(Graphics gr, String compType, Point loc, String compString1, String compString2)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)
            int compL = 60;  // Length of the component with leads

            Point p1 = loc;
            Point p2 = new Point(p1.X, p1.Y + leadL);
            Point p3 = new Point(p2.X - 20, p2.Y);
            Point p4 = new Point(p3.X, p3.Y + leadL);
            Point p5 = new Point(p2.X + 20, p2.Y);
            Point p6 = new Point(p5.X, p5.Y + leadL);

            // Define points for output lead branch
            Point p7 = new Point(p4.X, p4.Y + bodyL);
            Point p8 = new Point(p7.X, p7.Y + leadL);
            Point p9 = new Point(p6.X, p6.Y + bodyL);
            Point p10 = new Point(p9.X, p9.Y + leadL);
            Point p11 = new Point(p8.X + 20, p8.Y);
            Point p12 = new Point(p11.X, p11.Y + leadL);

            // Define points for comp labels
            Point p13 = new Point(p1.X - 2*bodyL- leadL, p1.Y + bodyL - leadL);
            Point p14 = new Point(p1.X + bodyL, p1.Y + bodyL - leadL);

            drawShuntLead2(gr, p1); // Draw input and output leads

            if (compType == "PRL")
            {
                drawShuntResBody(gr, p4);      // Draw resistor body
                drawShuntIndBody(gr, p6);      // Draw inductor body
            }
            else if (compType == "PRC")
            {
                drawShuntResBody(gr, p4);      // Draws resistor body
                drawShuntCapBody(gr, p6);      // Draw capacitor body
            }
            else if (compType == "PLC")
            {
                drawShuntIndBody(gr, p6);      // Draw inductor body
                drawShuntCapBody(gr, p4);      // Draw capacitor body
            }

            drawCompText(gr, p13, compString1);
            drawCompText(gr, p14, compString2);
        }

        // Draw a 3 component lumped element - Series SRLC
        public void drawSeriesLump3(Graphics gr, String compType, Point loc, String compString1, String compString2, String compString3)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)
            int compL = 60;  // Length of the component with leads

            Point p1 = loc;                                       
            Point p2 = new Point(p1.X + leadL, p1.Y);             
            Point p3 = new Point(p2.X + bodyL, p2.Y);             
            Point p4 = new Point(p3.X + leadL, p3.Y);             
            Point p5 = new Point(p4.X + bodyL, p4.Y);             
            Point p6 = new Point(p5.X + leadL, p5.Y);             
            Point p7 = new Point(p6.X + bodyL, p6.Y);             
            Point p8 = new Point(p7.X + leadL, p3.Y);           

            Point p9 = new Point(p2.X - 10, p3.Y - 35);            
            Point p10 = new Point(p2.X + 45, p3.Y + 10);          
            Point p11 = new Point(p2.X + 100, p3.Y - 35);          

            drawLead(gr, p1, p2); // Draw lead1
            drawLead(gr, p3, p4); // Draw lead2
            drawLead(gr, p5, p6); // Draw lead3
            drawLead(gr, p7, p8); // Draw lead4

            drawSeriesResBody(gr, p2);      // Draw series resistor body
            drawSeriesIndBody(gr, p4);      // Draw series inductor body
            drawSeriesCapBody(gr, p6);      // Draw series capacitor body

            drawCompText(gr, p9, compString1);
            drawCompText(gr, p10, compString2);
            drawCompText(gr, p11, compString3);
        }

        // Draw a 3 component lumped element - Shunt SRLC
        public void drawShuntLump3(Graphics gr, String compType, Point loc, String compString1, String compString2, String compString3)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)
            int compL = 60;  // Length of the component with leads

            Point p1 = loc;
            Point p2 = new Point(p1.X, p1.Y + leadL);
            Point p3 = new Point(p2.X, p2.Y + bodyL);
            Point p4 = new Point(p3.X, p3.Y + leadL);
            Point p5 = new Point(p4.X, p4.Y + bodyL);
            Point p6 = new Point(p5.X, p5.Y + leadL);
            Point p7 = new Point(p6.X, p6.Y + bodyL);
            Point p8 = new Point(p7.X, p7.Y + leadL);

            Point p9 =  new Point(p3.X + 20, p2.Y + 15);
            Point p10 = new Point(p5.X + 20, p4.Y + 15);
            Point p11 = new Point(p7.X + 20, p6.Y + 15);

            drawLead(gr, p1, p2); // Draw lead1
            drawLead(gr, p3, p4); // Draw lead2
            drawLead(gr, p5, p6); // Draw lead3
            drawLead(gr, p7, p8); // Draw lead4

            drawShuntResBody(gr, p2);      // Draw series resistor body
            drawShuntIndBody(gr, p4);      // Draw series inductor body
            drawShuntCapBody(gr, p6);      // Draw series capacitor body

            drawCompText(gr, p9,  compString1); // Resistor label
            drawCompText(gr, p10, compString2); // Inductor label
            drawCompText(gr, p11, compString3); // Capacitor label
        }

        // Draw a 3 component lumped element - Series PRLC
        public void drawSeriesPRLC(Graphics gr, String compType, Point loc, String compString1, String compString2, String compString3)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)

            // Define points for input lead branch
            Point p1 = loc;
            Point p2 = new Point(p1.X + leadL, p1.Y);
            Point p3 = new Point(p2.X, p2.Y - 30);
            Point p4 = new Point(p3.X + leadL, p3.Y);
            Point p5 = new Point(p2.X, p2.Y + 30);
            //Point p6 = new Point(p5.X + leadL, p5.Y);
            Point p6 = new Point(p5.X + leadL, p5.Y);
            // Debug.WriteLine("p6.X: " + p6.X + " p6.Y " + p6.Y);

            // Define points for output lead branch
            Point p7 = new Point(p4.X + bodyL, p4.Y);
            Point p8 = new Point(p7.X + leadL, p7.Y);
            Point p9 = new Point(p6.X + bodyL, p6.Y);
            Point p10 = new Point(p9.X + leadL, p9.Y);
            Point p11 = new Point(p8.X, p8.Y + 30);
            Point p12 = new Point(p11.X + leadL, p11.Y);
            Point p15 = new Point(p2.X + leadL, p2.Y);
            Point p16 = new Point(p15.X + bodyL, p15.Y);

            // Define points for comp labels
            Point p13 = new Point(p6.X-5, p6.Y+15);                 // Res label loc
            Point p14 = new Point(p4.X-5, p4.Y - bodyL+5);          // Ind label loc
            Point p17 = new Point(p15.X-5, p15.Y - 2*leadL - 5);    // Cap label loc

            drawSeriesResBody(gr, p6);      // Draw resistor body
            drawSeriesIndBody(gr, p4);      // Draw inductor body
            drawSeriesCapBody(gr, p15);     // Draw capacitor body

            drawSeriesLead3(gr, p1); // Draw input and output leads
            drawLead(gr, p2, p15);
            drawLead(gr, p16, p11);

            drawCompText(gr, p13, compString1); // Res
            drawCompText(gr, p14, compString2); // Ind
            drawCompText(gr, p17, compString3); // Cap
        }

        // Draw a 3 component lumped element - Series PRLC
        public void drawShuntPRLC(Graphics gr, String compType, Point loc, String compString1, String compString2, String compString3)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)

            // Define points for input lead branch
            Point p1 = loc;
            Point p2 = new Point(p1.X, p1.Y + leadL);
            Point p3 = new Point(p2.X - 30, p2.Y);
            Point p4 = new Point(p3.X, p3.Y + leadL);
            Point p5 = new Point(p2.X + 30, p2.Y);
            Point p6 = new Point(p5.X, p5.Y + leadL);

            // Define points for output lead branch
            Point p7 = new Point(p4.X, p4.Y + bodyL);
            Point p8 = new Point(p7.X, p7.Y + leadL);
            Point p9 = new Point(p6.X, p6.Y + bodyL);
            Point p10 = new Point(p9.X, p9.Y + leadL);
            Point p11 = new Point(p8.X + 30, p8.Y);
            Point p12 = new Point(p11.X, p11.Y + leadL);
            Point p13 = new Point(p2.X, p2.Y + leadL);
            Point p14 = new Point(p11.X, p11.Y - leadL);

            // Define points for comp labels
            Point p15 = new Point(p4.X - 2*bodyL + 10, p4.Y + 15);                 // Res label loc
            Point p16 = new Point(p6.X + bodyL-20, p4.Y+10);          // Ind label loc
            Point p17 = new Point(p15.X + 110, p15.Y + bodyL);    // Cap label loc

            drawShuntLead3(gr, p1); // Draw input and output leads

            drawShuntResBody(gr, p4);      // Draw resistor body
            drawShuntIndBody(gr, p6);      // Draw inductor body
            drawShuntCapBody(gr, p13);     // Draw capacitor body

            drawCompText(gr, p15, compString1); // Res
            drawCompText(gr, p16, compString2); // Ind
            drawCompText(gr, p17, compString3); // Cap
        }

        public void drawLead(Graphics gr, Point p1, Point p2)
        {
            gr.DrawLine(drawPen, p1, p2);
        }

        public void drawSeriesLead2(Graphics gr, Point p1)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)

            // Define points for input lead branch
            Point p2 = new Point(p1.X + leadL, p1.Y);
            Point p3 = new Point(p2.X, p2.Y - 20);
            Point p4 = new Point(p3.X + leadL, p3.Y);
            Point p5 = new Point(p2.X, p2.Y + 20);
            Point p6 = new Point(p5.X + leadL, p5.Y);

            // Define points for output lead branch
            Point p7 = new Point(p4.X + bodyL, p4.Y);
            Point p8 = new Point(p7.X + leadL, p7.Y);
            Point p9 = new Point(p6.X + bodyL, p6.Y);
            Point p10 = new Point(p9.X + leadL, p9.Y);
            Point p11 = new Point(p8.X, p8.Y + 20);
            Point p12 = new Point(p11.X + leadL, p11.Y);

            gr.DrawLine(drawPen, p1, p2);
            gr.DrawLine(drawPen, p3, p5);
            gr.DrawLine(drawPen, p3, p4);
            gr.DrawLine(drawPen, p5, p6);
            gr.DrawLine(drawPen, p7, p8);
            gr.DrawLine(drawPen, p9, p10);
            gr.DrawLine(drawPen, p8, p10);
            gr.DrawLine(drawPen, p11, p12);
        }

        public void drawShuntLead2(Graphics gr, Point p1)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)

            // Define points for input lead branch

            Point p2 = new Point(p1.X, p1.Y + leadL);
            Point p3 = new Point(p2.X - 20, p2.Y);
            Point p4 = new Point(p3.X, p3.Y + leadL);
            Point p5 = new Point(p2.X + 20, p2.Y);
            Point p6 = new Point(p5.X, p5.Y + leadL);

            // Define points for output lead branch
            Point p7 = new Point(p4.X, p4.Y + bodyL);
            Point p8 = new Point(p7.X, p7.Y + leadL);
            Point p9 = new Point(p6.X, p6.Y + bodyL);
            Point p10 = new Point(p9.X, p9.Y + leadL);
            Point p11 = new Point(p8.X + 20, p8.Y);
            Point p12 = new Point(p11.X, p11.Y + leadL);

            gr.DrawLine(drawPen, p1, p2);
            gr.DrawLine(drawPen, p3, p5);
            gr.DrawLine(drawPen, p3, p4);
            gr.DrawLine(drawPen, p5, p6);
            gr.DrawLine(drawPen, p7, p8);
            gr.DrawLine(drawPen, p9, p10);
            gr.DrawLine(drawPen, p8, p10);
            gr.DrawLine(drawPen, p11, p12);
        }

        public void drawSeriesLead3(Graphics gr, Point p1)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)

            // Define points for input lead branch
            Point p2 = new Point(p1.X + leadL, p1.Y);
            Point p3 = new Point(p2.X, p2.Y - 30);
            Point p4 = new Point(p3.X + leadL, p3.Y);
            Point p5 = new Point(p2.X, p2.Y + 30);
            Point p6 = new Point(p5.X + leadL, p5.Y);

            // Define points for output lead branch
            Point p7 = new Point(p4.X + bodyL, p4.Y);
            Point p8 = new Point(p7.X + leadL, p7.Y);
            Point p9 = new Point(p6.X + bodyL, p6.Y);
            Point p10 = new Point(p9.X + leadL, p9.Y);
            Point p11 = new Point(p8.X, p8.Y + 30);
            Point p12 = new Point(p11.X + leadL, p11.Y);

            gr.DrawLine(drawPen, p1, p2);
            gr.DrawLine(drawPen, p3, p5);
            gr.DrawLine(drawPen, p3, p4);
            gr.DrawLine(drawPen, p5, p6);
            gr.DrawLine(drawPen, p7, p8);
            gr.DrawLine(drawPen, p9, p10);
            gr.DrawLine(drawPen, p8, p10);
            gr.DrawLine(drawPen, p11, p12);
        }

        public void drawShuntLead3(Graphics gr, Point p1)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)

            // Define points for input lead branch
            Point p2 = new Point(p1.X, p1.Y + leadL);
            Point p3 = new Point(p2.X - 30, p2.Y);
            Point p4 = new Point(p3.X, p3.Y + leadL);
            Point p5 = new Point(p2.X + 30, p2.Y);
            Point p6 = new Point(p5.X, p5.Y + leadL);

            // Define points for output lead branch
            Point p7 = new Point(p4.X, p4.Y + bodyL);
            Point p8 = new Point(p7.X, p7.Y + leadL);
            Point p9 = new Point(p6.X, p6.Y + bodyL);
            Point p10 = new Point(p9.X, p9.Y + leadL);
            Point p11 = new Point(p8.X + 30, p8.Y);
            Point p12 = new Point(p11.X, p11.Y + leadL);

            Point p13 = new Point(p2.X, p2.Y + leadL);
            Point p14 = new Point(p11.X, p11.Y - leadL);

            gr.DrawLine(drawPen, p1, p2);
            gr.DrawLine(drawPen, p3, p5);
            gr.DrawLine(drawPen, p3, p4);
            gr.DrawLine(drawPen, p5, p6);
            gr.DrawLine(drawPen, p7, p8);
            gr.DrawLine(drawPen, p9, p10);
            gr.DrawLine(drawPen, p8, p10);
            gr.DrawLine(drawPen, p11, p12);
            gr.DrawLine(drawPen, p2, p13);
            gr.DrawLine(drawPen, p11, p14);
        }

        // Draw series resistor body used by Resistor, SRC, SRL, SRLC, PRC, PRL, PRLC
        public void drawSeriesResBody(Graphics gr, Point p1)
        {
            int leadL = 10;

            for (int i = 0; i < 4; i++)
            {
                gr.DrawLine(drawPen, p1.X + leadL * (i), p1.Y, p1.X + leadL * (i) + 3, p1.Y - leadL);
                gr.DrawLine(drawPen, p1.X + leadL * (i) + 3, p1.Y - leadL, p1.X + leadL * (i) + 6, p1.Y + leadL);
                gr.DrawLine(drawPen, p1.X + leadL * (i) + 6, p1.Y + leadL, p1.X + leadL * (i + 1), p1.Y);
            }
        }

        // Draw series inductor body used by Inductor, SRL, SLC, SRLC, PRL, PLC, PRLC
        public void drawSeriesIndBody(Graphics gr, Point p1)
        {
            int leadL = 10;

            // Draw the inductor body curves
            for (int i = 0; i < 4; i++)
            {
                float startAngle = 180;
                float sweepAngle = 180;
                Rectangle rect = new Rectangle(p1.X + leadL * (i), p1.Y - leadL - 5, leadL, leadL);
                gr.DrawArc(drawPen, rect, startAngle, sweepAngle);
            }

            // Draw the inductor body vertical lines
            for (int i = 0; i < 5; i++)
            {
                gr.DrawLine(drawPen, p1.X + leadL * (i), p1.Y, p1.X + leadL * (i), p1.Y - leadL);
            }
        }

        // Draw series capacitor body used by Capacitor, SRC, SLC, SRLC, PRC, PLC, PRLC
        public void drawSeriesCapBody(Graphics gr, Point p1)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)
            int compL = 60;  // Length of the component with leads

            // Draw the capacitor body curves
            float startAngle = 90;
            float sweepAngle = 180;
            Rectangle rect = new Rectangle(p1.X + 2 * leadL, p1.Y - 10, leadL, leadL * 2);
            gr.DrawArc(drawPen, rect, startAngle, sweepAngle);

            // Draw the capacitor body vertical line
            gr.DrawLine(drawPen, p1.X + leadL + 5, p1.Y - leadL, p1.X + leadL + 5, p1.Y + leadL);

            // Draw the capacitor lead extenders
            gr.DrawLine(drawPen, p1.X, p1.Y, p1.X + leadL + 5, p1.Y);
            gr.DrawLine(drawPen, p1.X + 2 * leadL, p1.Y, p1.X + bodyL, p1.Y);
        }

        // Draw shunt resistor body used by Resistor, SRC, SRL, SRLC, PRC, PRL, PRLC
        public void drawShuntResBody(Graphics gr, Point p1)
        {
            int leadL = 10;

            for (int i = 0; i < 4; i++)
            {
                gr.DrawLine(drawPen, p1.X, p1.Y + leadL * (i), p1.X + leadL, p1.Y + leadL * (i) + 3);
                gr.DrawLine(drawPen, p1.X + leadL, p1.Y + leadL * (i) + 3, p1.X - leadL, p1.Y + leadL * (i) + 6);
                gr.DrawLine(drawPen, p1.X - leadL, p1.Y + leadL * (i) + 6, p1.X, p1.Y + leadL * (i) + leadL);
            }
        }

        // Draw shunt inductor body used by Inductor, SRL, SLC, SRLC, PRL, PLC, PRLC
        public void drawShuntIndBody(Graphics gr, Point p1)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)
            int compL = 60;  // Length of the component with leads

            // Draw the inductor body curves
            for (int i = 0; i < 4; i++)
            {
                float startAngle = 90;
                float sweepAngle = -180;
                Rectangle rect = new Rectangle(p1.X + leadL - 5, p1.Y + leadL * (i), leadL, leadL);
                gr.DrawArc(drawPen, rect, startAngle, sweepAngle);
            }

            // Draw the inductor body vertical lines
            for (int i = 0; i < 5; i++)
            {
                gr.DrawLine(drawPen, p1.X, p1.Y + leadL * (i), p1.X + leadL, p1.Y + leadL * (i));
            }
        }

        // Draw shunt capacitor body used by Capacitor, SRC, SLC, SRLC, PRC, PLC, PRLC
        public void drawShuntCapBody(Graphics gr, Point p1)
        {
            int leadL = 10;  // Length of input and output leads
            int bodyL = 40;  // Length of the component body (without leads)
            int compL = 60;  // Length of the component with leads

            // Draw the capacitor body curves
            float startAngle = 180;
            float sweepAngle = 180;
            Rectangle rect = new Rectangle(p1.X-leadL, p1.Y + 20, leadL * 2, leadL);
            gr.DrawArc(drawPen, rect, startAngle, sweepAngle);

            // Draw the capacitor body horizontal line
            gr.DrawLine(drawPen, p1.X-leadL, p1.Y + 15, p1.X+leadL, p1.Y + 15);

            // Draw the capacitor lead extenders
            gr.DrawLine(drawPen, p1.X, p1.Y, p1.X, p1.Y + 15);
            gr.DrawLine(drawPen, p1.X, p1.Y + 20, p1.X, p1.Y + bodyL);
        }

        // Draw series ground symbol used by Ground, TLIN
        public void drawSeriesGnd(Graphics gr, Point p1)
        {
            // Define the points
            Point p2 = new Point(p1.X, p1.Y + leadL);
            Point p3 = new Point(p2.X - 10, p2.Y);
            Point p4 = new Point(p2.X + 10, p2.Y);
            Point p5 = new Point(p3.X + 5, p3.Y + 5);
            Point p6 = new Point(p5.X + 10, p5.Y);
            Point p7 = new Point(p5.X + 3, p5.Y + 5);
            Point p8 = new Point(p7.X + 4, p7.Y);

            // Draw the input lead
            gr.DrawLine(drawPen, p1, p2);
            gr.DrawLine(drawPen, p3, p4);
            gr.DrawLine(drawPen, p5, p6);
            gr.DrawLine(drawPen, p7, p8);
        }

        // Draw shunt ground symbol used by Ground, TLIN
        public void drawShuntGnd(Graphics gr, Point p1)
        {
            // Define the points
            Point p2 = new Point(p1.X + leadL, p1.Y);
            Point p3 = new Point(p2.X, p2.Y - 10);
            Point p4 = new Point(p2.X, p2.Y + 10);
            Point p5 = new Point(p3.X + 5, p3.Y + 5);
            Point p6 = new Point(p5.X, p5.Y + 10);
            Point p7 = new Point(p5.X + 5, p5.Y + 3);
            Point p8 = new Point(p7.X, p7.Y + 4);

            // Draw the input lead
            gr.DrawLine(drawPen, p1, p2);
            gr.DrawLine(drawPen, p3, p4);
            gr.DrawLine(drawPen, p5, p6);
            gr.DrawLine(drawPen, p7, p8);
        }

        public virtual void drawCompText(Graphics gr, Point p1, String drawString)
        {
            // Convert Point ints to floats
            float x = p1.X;
            float y = p1.Y;

            // Draw string to screen.
            gr.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }
    }
}

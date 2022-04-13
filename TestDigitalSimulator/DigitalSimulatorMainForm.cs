// C# Libraries
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

// Test Digital Simulator Libraries
using TestDigitalSimulator.Circuits;
using TestDigitalSimulator.Components;
using TestDigitalSimulator.Wires;

namespace TestDigitalSimulator
{
    public partial class DigitalSimulatorMainForm : Form
    {
        // ******************** Private Class Variables/Attributes ************************
        // Create a schematic controller
        Circuit schematic = new Circuit();

        // The grid spacing.
        const int grid_gap = 10;

        // Mouse event attributes
        bool isMouseDown = false;
        bool wireDrawing = false;
        Point NewPt1, NewPt2, currentPt, offset;
        Comp tempComp = new Comp();

        // The wire we are drawing
        Wire NewWire = null;

        // ******************** Class Constructors **************************************
        public DigitalSimulatorMainForm()
        {
            InitializeComponent();
        }

        // ******************** Schematic Canvas Event Handlers *************************
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach(Comp comp in schematic.comps)
            {
                comp.Draw(e.Graphics);
            }
        }

        // ******************** Mouse Event Handler *************************************
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        isMouseDown = true;
                        snapStartPointToGrid(e);

                        if (!wireDrawing)
                        {
                            checkCompSelected();
                        }
                        if (wireDrawing)
                        {
                            startWireDraw();
                        }
                        break;
                    }
                case MouseButtons.Right:
                    {
                        switchClicked(e);
                        break;
                    }
                default:
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                int x = e.X;
                int y = e.Y;
                SnapToGrid(ref x, ref y);

                if (!wireDrawing)
                {
                    if (tempComp != null)
                    {
                        tempComp.loc = new Point(x - offset.X, y - offset.Y);
                    }
                }
                if (wireDrawing)
                {
                    if (NewWire == null) return;
                    NewPt2 = new Point(x, y);
                    NewWire.Pt2 = NewPt2;
                }

                schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            tempComp = null;

            if (wireDrawing)
            {
                int x = e.X;
                int y = e.Y;
                SnapToGrid(ref x, ref y);
                NewPt1.Y = y;

                currentPt = NewPt2;

                // Iterate over the schematic component list and draw each component
                foreach (Comp comp in schematic.comps)
                {
                    if (hitTest(comp))
                    {
                        Debug.WriteLine("Start Component hit!" + comp.ToString());
                        NewWire.inComp = comp;
                        comp.wires.Add(NewWire);
                    }
                }
                //connectWireToOutCompPin();

                // Update the new wire end points
                NewWire.Pt2 = NewPt2;
                NewWire.endcapsVisible = false;
                NewWire = null;
                schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox

                // Debugs to show the line points on the console
                Debug.WriteLine("Line End Points: (" + NewPt2.X + ", " + NewPt2.Y + " )");
            }
        }

        // ******************** UI Button Handlers **************************************
        private void btnSWITCH_Click(object sender, EventArgs e)
        {
            TestDigitalSimulator.Components.Switch sw = new TestDigitalSimulator.Components.Switch();
            schematic.comps.Add(sw);
            schematicCanvas.Invalidate();

        }

        private void btnLED_Click(object sender, EventArgs e)
        {
            LED led = new LED();
            schematic.comps.Add(led);
            schematicCanvas.Invalidate();
        }

        private void btnAND_Click(object sender, EventArgs e)
        {
            AND and = new AND();
            schematic.comps.Add(and);
            schematicCanvas.Invalidate();
        }

        private void btnOR_Click(object sender, EventArgs e)
        {
            OR or = new OR();
            schematic.comps.Add(or);
            schematicCanvas.Invalidate();
        }

        private void btnNOT_Click(object sender, EventArgs e)
        {
            NOT not = new NOT();
            schematic.comps.Add(not);
            schematicCanvas.Invalidate();
        }

        private void btnWIRE_Click(object sender, EventArgs e)
        {
            wireDrawing = !wireDrawing;
        }

        // ******************** Helper Methods ******************************************
        private void DrawBackgroundGrid()
        {
            Bitmap bm = new Bitmap(
                2000,
                2000);
            for (int x = 0; x < 2000; x += grid_gap)
            {
                for (int y = 0; y < 2000; y += grid_gap)
                {
                    bm.SetPixel(x, y, Color.Black); //Color.Black);
                }
            }
            schematicCanvas.BackgroundImage = bm;
        }

        private void SnapToGrid(ref int x, ref int y)
        {
            x = grid_gap * (int)Math.Round((double)x / grid_gap);
            y = grid_gap * (int)Math.Round((double)y / grid_gap);
        }

        private void snapStartPointToGrid(MouseEventArgs e)
        {
            // Snap the start point to the Grid
            int x = e.X;
            int y = e.Y;
            SnapToGrid(ref x, ref y);
            NewPt1.X = x;
            NewPt1.Y = y;
            currentPt = NewPt1;
        }

        private void checkCompSelected()
        {
            // Iterate over the component list and test each to see if it is "hit"
            foreach (Comp comp in schematic.comps)
            {
                if (hitTest(comp))
                {
                    tempComp = comp;
                    offset.X = NewPt1.X - comp.loc.X;
                    offset.Y = NewPt1.Y - comp.loc.Y;
                }
            }
        }

        private void startWireDraw()
        {
            // Snap the line end point to the Grid
            NewPt2 = new Point(NewPt1.X, NewPt1.Y);

            // Create a new wire and add it to the schematic wires list
            NewWire = new Wire(); // Use the constructor with no parameters
            NewWire.Pt1 = NewPt1;
            NewWire.Pt2 = NewPt2;
            NewWire.endcapsVisible = true;
            schematic.comps.Add(NewWire);

            // Iterate over the schematic component list and draw each component
            foreach (Comp comp in schematic.comps)
            {
                if (hitTest(comp))
                {
                    Debug.WriteLine("Start Component hit!" + comp.ToString());
                    NewWire.inComp = comp;
                    comp.wires.Add(NewWire);
                }
            }
            //connectWireToInCompPin();

            // Debugs to show the line points on the console
            Debug.WriteLine("Line Start Points: (" + NewPt1.X + ", " + NewPt1.Y + " )");
        }

        private void switchClicked(MouseEventArgs e)
        {
            foreach (Comp comp in schematic.comps)
            {
                if (comp is Components.Switch)
                {
                    NewPt1.X = e.X;
                    NewPt1.Y = e.Y;

                    if (swHitTest(comp))
                    {
                        Components.Switch tempSw = comp as Components.Switch;
                        tempSw.ToggleSwitchState();
                        schematic.DigitalTraverse(tempSw);
                        schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox
                    }
                }
            }
        }

        private void connectWireToInCompPin()
        {
            // Iterate over the schematic component list and draw each component
            foreach (Comp comp in schematic.comps)
            {
                if (hitTest(comp))
                {
                    Debug.WriteLine("Start Component hit!" + comp.ToString());
                    NewWire.inComp = comp;
                    comp.wires.Add(NewWire);
                }
            }
        }

        private void connectWireToOutCompPin()
        {
            // Iterate over the schematic component list and draw each component
            foreach (Comp comp in schematic.comps)
            {
                Debug.WriteLine("Checking comp: " + comp.ToString());
                if (hitTest(comp))
                {
                    Debug.WriteLine("End Component hit! " + comp.ToString());
                    NewWire.outComp = comp;

                    if ((int)NewPt1.Y == (int)comp.loc.Y + 20 || (int)NewPt1.Y == (int)comp.loc.Y + 30)
                        NewWire.outCompPin = 0;
                    else if ((int)NewPt1.Y == (int)comp.loc.Y + 40)
                        NewWire.outCompPin = 1;
                }
            }
        }

        private bool hitTest(Comp comp)
        {
            bool hit;

            hit = false;
            if ((currentPt.X >= comp.loc.X && currentPt.X <= comp.loc.X + comp.width) &&
                (currentPt.Y >= comp.loc.Y && currentPt.Y <= comp.loc.Y + comp.height))
            {
                Debug.WriteLine("Hit!");
                hit = true;
            }
            else
            {
                Debug.WriteLine("No Hit!");
                hit = false;
            }
            return hit;
        }

        private bool swHitTest(Comp comp)
        {
            bool hit;

            hit = false;
            if (NewPt1.X >= comp.loc.X + 10 && NewPt1.X <= comp.loc.X + comp.width - 10)
            {
                if (NewPt1.Y >= comp.loc.Y + 20 && NewPt1.Y <= comp.loc.Y + comp.height - 20)
                {
                    Debug.WriteLine("Switch Hit!");
                    hit = true;
                }
            }
            else
            {
                Debug.WriteLine("No Switch Hit!");
                hit = false;
            }

            return hit;
        }
    }
} 
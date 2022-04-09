﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using EngineeringTools.Components;
using EngineeringTools.Components.Digital;
using EngineeringTools.Wires;

namespace EngineeringTools
{
    public partial class EngineeringToolsMainForm : Form
    {
        // The grid spacing.
        public const int grid_gap = 10;

        // Create a component list
        List<Comp> comps = new List<Comp>();

        // Mouse event attributes
        bool isMouseDown = false;
        bool lineDrawing = false;
        private Point NewPt1, NewPt2, offset;
        Comp tempComp = new Comp();

        // The wire we are drawing
        private Wire NewWire = null;

        // ******************** Constructors ************************
        public EngineeringToolsMainForm()
        {
            InitializeComponent();
        }

        // ******************** schematicCanvas Paint and Resize Event Handlers ************************
        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (Comp comp in comps)
            {
                comp.Draw(e.Graphics);
            }
        }

        private void schematicCanvas_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

        // ******************** schematicCanvas Mouse Event Handlers ************************
        private void schematicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

            // Snap the start point to the Grid
            int x = e.X;
            int y = e.Y;
            SnapToGrid(ref x, ref y);
            NewPt1.X = x;
            NewPt1.Y = y;

            if (!lineDrawing)
            {
                // Iterate over the component list and test each to see if it is "hit"
                foreach (Comp comp in comps)
                {
                    if (hitTest(comp))
                    {
                        tempComp = comp;
                        offset.X = NewPt1.X - comp.loc.X;
                        offset.Y = NewPt1.Y - comp.loc.Y;
                    }
                }
            }
            if (lineDrawing)
            {
                // Snap the line end point to the Grid
                NewPt2 = new Point(x, y);

                // Create a new wire and add it to the schematic wires list
                NewWire = new Wire(); // Use the constructor with no parameters
                NewWire.Pt1 = NewPt1;
                NewWire.Pt2 = NewPt2;
                NewWire.endcapsVisible = true;
                comps.Add(NewWire);

                // Debugs to show the line points on the console
                Debug.WriteLine("Line Start Points: (" + NewPt1.X + ", " + NewPt1.Y + " )");
            }
        }

        private void schematicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                int x = e.X;
                int y = e.Y;
                SnapToGrid(ref x, ref y);

                if (!lineDrawing)
                {
                    if (tempComp != null)
                    {
                        tempComp.loc = new Point(x - offset.X, y - offset.Y);
                    }
                }
                if (lineDrawing)
                {
                    if (NewWire == null) return;
                    NewPt2 = new Point(x, y);
                    NewWire.Pt2 = NewPt2;
                }
                schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox
            }
        }

        private void schematicCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            tempComp = null;

            if (lineDrawing)
            {
                int x = e.X;
                int y = e.Y;
                SnapToGrid(ref x, ref y);
                NewPt1.Y = y;

                // Update the new wire end points
                NewWire.Pt2 = NewPt2;
                NewWire.endcapsVisible = false;

                // Terminate any further updates to the new wire, this makes it permanent in the schematic wires list
                NewWire = null;

                // Redraw.
                schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox

                // Debugs to show the line points on the console
                Debug.WriteLine("Line End Points: (" + NewPt2.X + ", " + NewPt2.Y + " )");
            }
        }

        // ******************** Button Handlers ************************
        private void btnCircuit_Click(object sender, EventArgs e)
        {
            panelCircuitSubmenu.Visible = !panelCircuitSubmenu.Visible;
        }

        private void btnDigital_Click(object sender, EventArgs e)
        {
            panelDigitalSubmenu.Visible = !panelDigitalSubmenu.Visible;
        }

        private void btnWireMode_Click(object sender, EventArgs e)
        {
            lineDrawing = !lineDrawing;
        }

        private void btnAND_Click(object sender, EventArgs e)
        {
            AND and = new AND();
            comps.Add(and);
            schematicCanvas.Invalidate();
        }

        private void btnOR_Click(object sender, EventArgs e)
        {
            OR or = new OR();
            comps.Add(or);
            schematicCanvas.Invalidate();
        }

        private void btnNOT_Click(object sender, EventArgs e)
        {
            NOT not = new NOT();
            comps.Add(not);
            schematicCanvas.Invalidate();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            Components.Digital.Switch sw = new Components.Digital.Switch();
            comps.Add(sw);
            schematicCanvas.Invalidate();
        }

        private void btnLED_Click(object sender, EventArgs e)
        {
            LED led = new LED();
            comps.Add(led);
            schematicCanvas.Invalidate();
        }

        // ******************** Helper Methods ************************
        private void DrawBackgroundGrid()
        {
            Bitmap bm = new Bitmap(
                2000,
                2000);
            for (int x = 0; x < 2000; x += grid_gap)
            {
                for (int y = 0; y < 2000; y += grid_gap)
                {
                    bm.SetPixel(x, y, Color.White); //Color.Black);
                }
            }
            schematicCanvas.BackgroundImage = bm;
        }

        private void SnapToGrid(ref int x, ref int y)
        {
            x = grid_gap * (int)Math.Round((double)x / grid_gap);
            y = grid_gap * (int)Math.Round((double)y / grid_gap);
        }

        private bool hitTest(Comp comp)
        {
            bool hit;

            hit = false;
            if ((NewPt1.X >= comp.loc.X && NewPt1.X <= comp.loc.X + comp.width) &&
                (NewPt1.Y >= comp.loc.Y && NewPt1.Y <= comp.loc.Y + comp.height))
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
    }
}
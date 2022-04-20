// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MicrowaveTools.Components.Microstrip
{
    public class MLIN : Comp
    {
        public MLIN()
        {

        }

        public MLIN(double value, Point pt)
        {
            Loc.X = pt.X;
            Loc.Y = pt.Y;
            Width = 60;
            Height = 60;
            Value = value;
            boundBox = new Rectangle(Loc.X, Loc.Y, Width, Height);

            Pin = new Point(Loc.X, Loc.Y + 30);
            Pout = new Point(Loc.X + compSize, Loc.Y + 30);
        }

        // Let the MLIN draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Component selection
            checkSelect();
            drawSelectRect(gr, new Point(Loc.X, Loc.Y + 30));
            drawSelectRect(gr, new Point(Loc.X + compL - endcap_radius, Loc.Y + 30));

            GraphicsPath gp = new GraphicsPath();

            Point p1 = Loc;             // Assume p1 is the end of the lead at the output of Pin
            Point p2 = new Point(p1.X + 10, p1.Y);
            Point p3 = new Point(p2.X, p2.Y - 10); // Location of MLIN rectangle
            Point p4 = new Point(p2.X + 40, p2.Y);
            Point p5 = new Point(p4.X + 10, p4.Y);

            gp.AddLine(p1, p2);
            gp.AddLine(p4, p5);
            gp.AddRectangle(new Rectangle(p3.X, p3.Y, 40, 20));

            // Draw the component text
            compText = "W = " + this.Value + " mils";
            pt = new Point(Loc.X + 5, Loc.Y + 5);
            gp.AddString(compText, family, fontStyle, emSize, pt, format);

            // Set rotation angle
            if (isRotated)
                angle = 90;
            else
                angle = 0;

            // Rotate the component by angle deg
            PointF rotatePoint = new PointF(Loc.X + 30, Loc.Y + 30); // Rotate about component center point
            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt(angle, rotatePoint, MatrixOrder.Append);
            gr.Transform = myMatrix;

            // Update the bounding box location
            boundBox = new Rectangle(Loc.X, Loc.Y, Width, Height);

            // Draw the component path
            gr.DrawPath(drawPen, gp);

            // Draw the bounding box for debug
            //gr.DrawRectangle(redPen, boundBox);

            gp.Dispose();
        }
    }
}

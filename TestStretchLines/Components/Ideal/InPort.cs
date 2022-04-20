// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TestStretchLInes.Components.Ideal
{
    public class InPort : Comp
    {
        public InPort()
        {
 
        }

        public InPort(double value, Point pt)
        {
            Loc.X = pt.X;
            Loc.Y = pt.Y;
            Width = 60;
            Height = 60;
            Value = value;
            boundBox = new Rectangle(Loc.X, Loc.Y, Width, Height);

            Pout = new Point(Loc.X + compSize, Loc.Y + halfCompSize);
        }

        // Let the InPort draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Component selection
            checkSelect();
            drawSelectRect(gr, new Point(Loc.X + compSize, Loc.Y+30));

            GraphicsPath gp = new GraphicsPath();

            Point p1 = new Point(Loc.X + compSize, Loc.Y + halfCompSize);             // Assume p1 is the end of the lead at the output of Pin
            Point p2 = new Point(p1.X - leadL, p1.Y);
            Point p3 = new Point(p2.X - 10, p2.Y - 10);
            Point p4 = new Point(p3.X - 30, p3.Y);
            Point p5 = new Point(p4.X, p4.Y + 20);
            Point p6 = new Point(p5.X + 30, p5.Y);

            gp.AddLine(p1, p2);
            gp.AddLine(p2, p3);
            gp.AddLine(p3, p4);
            gp.AddLine(p4, p5);
            gp.AddLine(p5, p6);

            gp.AddLine(p6, p2);

            // Draw the component text
            compText = "Pin";
            pt = new Point(Loc.X+10, Loc.Y+5);
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

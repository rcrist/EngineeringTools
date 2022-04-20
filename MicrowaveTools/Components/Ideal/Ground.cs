using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MicrowaveTools.Components.Ideal
{
    public class Ground : Comp
    {
        public Ground()
        {

        }

        public Ground(Point pt)
        {
            Loc.X = pt.X;
            Loc.Y = pt.Y;
            Width = 60;
            Height = 60;
            boundBox = new Rectangle(Loc.X, Loc.Y, Width, Height);

            Pout = new Point(Loc.X + compSize, Loc.Y + halfCompSize);
        }

        // Let the Ground draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            checkSelect();
            drawSelectRect(gr, new Point(Loc.X, Loc.Y + 30));

            GraphicsPath gp = new GraphicsPath();

            // Define the points
            Point p1 = Loc;
            Point p2 = new Point(p1.X, p1.Y + leadL);
            Point p3 = new Point(p2.X - 10, p2.Y);
            Point p4 = new Point(p2.X + 10, p2.Y);
            Point p5 = new Point(p3.X + 5, p3.Y + 5);
            Point p6 = new Point(p5.X + 10, p5.Y);
            Point p7 = new Point(p5.X + 3, p5.Y + 5);
            Point p8 = new Point(p7.X + 4, p7.Y);

            // Draw the input lead
            gp.AddLine(p1, p2);
            gp.AddLine(p3, p4);
            gp.AddLine(p5, p6);
            gp.AddLine(p7, p8);

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

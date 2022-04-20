// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MicrowaveTools.Components.Microstrip
{
    public class MCROS : Comp
    {
        Point[] p = new Point[21];

        public MCROS()
        {

        }

        public MCROS(double value, Point pt)
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

        // Let the MCROS draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Component selection
            checkSelect();
            drawSelectRect(gr, new Point(Loc.X, Loc.Y + 30));
            drawSelectRect(gr, new Point(Loc.X + compL - endcap_radius, Loc.Y + 30));

            GraphicsPath gp = new GraphicsPath();

            p[1] = Loc;            // Assume p1 is the input lead to the left
            p[2] = new Point(p[1].X + 10, p[1].Y);
            p[3] = new Point(p[2].X, p[2].Y - 10);
            p[4] = new Point(p[3].X + 10, p[3].Y);
            p[5] = new Point(p[4].X, p[4].Y - 10);
            p[6] = new Point(p[5].X + 20, p[5].Y);
            p[7] = new Point(p[6].X, p[6].Y + 10);
            p[8] = new Point(p[7].X + 10, p[7].Y);
            p[9] = new Point(p[8].X, p[8].Y + 20);
            p[10] = new Point(p[9].X - 10, p[9].Y);
            p[11] = new Point(p[10].X, p[10].Y + 10);
            p[12] = new Point(p[11].X - 20, p[11].Y);
            p[13] = new Point(p[12].X, p[12].Y - 10);
            p[14] = new Point(p[13].X - 10, p[13].Y);
            p[15] = new Point(p[5].X + 10, p[5].Y - 10);
            p[16] = new Point(p[15].X, p[15].Y + 10);
            p[17] = new Point(p[8].X + 10, p[8].Y + 10);
            p[18] = new Point(p[17].X - 10, p[17].Y);
            p[19] = new Point(p[11].X - 10, p[11].Y + 10);
            p[20] = new Point(p[19].X, p[19].Y - 10);

            for (int i = 1; i < 14; i++)
                gp.AddLine(p[i], p[i + 1]);
            gp.AddLine(p[14], p[2]);
            for (int i = 15; i < 21; i += 2)
                gp.AddLine(p[i], p[i + 1]);

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

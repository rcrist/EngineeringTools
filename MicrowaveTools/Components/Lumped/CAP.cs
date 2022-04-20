// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MicrowaveTools.Components.Lumped
{
    public class CAP : Comp
    {
        public CAP()
        {

        }

        public CAP(double value, Point pt)
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

        // Let the Capacitor draw itself called from the canvas paint event
        public override void Draw(Graphics gr)
        {
            // Component selection
            checkSelect();
            drawSelectRect(gr, new Point(Loc.X, Loc.Y + 30));
            drawSelectRect(gr, new Point(Loc.X + compL - endcap_radius, Loc.Y + 30));

            GraphicsPath gp = new GraphicsPath();

            // Draw the input leads
            gp.AddLine(Loc.X, Loc.Y + 30, Loc.X + 2 * leadL + 5, Loc.Y + 30);

            // Draw the capacitor body vertical line
            gp.AddLine(Loc.X + 2 * leadL + 5, Loc.Y - leadL + 30, Loc.X + 2 * leadL + 5, Loc.Y + leadL + 30);

            gp.StartFigure(); // Starts a new figure without closing the current figure.

            // Draw the capacitor body curves
            float startAngle = 90;
            float sweepAngle = 180;
            Rectangle rect = new Rectangle(Loc.X + 3 * leadL, Loc.Y - 10 + 30, leadL, leadL * 2);
            gp.AddArc(rect, startAngle, sweepAngle);

            // Draw the output leads
            gp.AddLine(Loc.X + 3 * leadL, Loc.Y + 30, Loc.X + compL, Loc.Y + 30);

            // Draw the component text
            compText = "C = " + this.Value + "pF";
            pt = new Point(Loc.X + 5, Loc.Y);
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

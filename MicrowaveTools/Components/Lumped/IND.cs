// C# Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

// MathNet.Numberics Libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

namespace MicrowaveTools.Components.Lumped
{
    public class IND : Comp
    {
        public IND()
        {

        }

        public IND(double value, Point pt)
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

        // Analysis initializer
        public override void initComp(float f)
        {
            Matrix<Complex32> Yind = Matrix<Complex32>.Build.Dense(2, 2);
            Yind[0, 0] = 1;
            Yind[0, 1] = -1;
            Yind[1, 0] = -1;
            Yind[1, 1] = 1;

            Complex32 denom = new Complex32(0, (float)(2 * Constants.Pi * f * this.Value * Settings.Settings.nH));
            if (denom != 0)
                Yind = Yind / denom; // Won't work with a double, must be a float
            else
                Debug.WriteLine("ERROR: Divide by 0 in initComp(): " + "f: " + f + " Value: " + this.Value);
            Y = Yind;
            N = new int[] { 0, 0 };
        }

        public override void Draw(Graphics gr)
        {
            // Component selection
            checkSelect();
            drawSelectRect(gr, new Point(Loc.X, Loc.Y + 30));
            drawSelectRect(gr, new Point(Loc.X + compL - endcap_radius, Loc.Y + 30));

            GraphicsPath gp = new GraphicsPath();

            // Draw the input leads
            gp.AddLine(Loc.X, Loc.Y + 30, Loc.X + leadL, Loc.Y + 30);

            // Draw the inductor body curves
            for (int i = 1; i < 5; i++)
            {
                float startAngle = 180;
                float sweepAngle = 180;
                Rectangle rect = new Rectangle(Loc.X + leadL * (i), Loc.Y - leadL - 5 + 30, leadL, leadL);
                gp.AddArc(rect, startAngle, sweepAngle);
            }

            // Draw the inductor body vertical lines
            for (int i = 1; i < 6; i++)
            {
                gr.DrawLine(drawPen, Loc.X + leadL * (i), Loc.Y + 30, Loc.X + leadL * (i), Loc.Y - leadL + 30);
            }

            // Draw the output leads
            gp.AddLine(Loc.X + bodyL + leadL, Loc.Y + 30, Loc.X + compL, Loc.Y + 30);   // Output lead

            // Draw the component text
            compText = "L = " + this.Value + "nH";
            pt = new Point(Loc.X + 5, Loc.Y);
            gp.AddString(compText, family, fontStyle, emSize, pt, format);

            // Set rotation angle
            if (isRotated)
                angle = 90;
            else
                angle = 0;

            // Rotate the component by angle deg
            PointF rotatePoint = new PointF(Loc.X + 30, Loc.Y + 30); // Rotate about component center point
            System.Drawing.Drawing2D.Matrix myMatrix = new System.Drawing.Drawing2D.Matrix();
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

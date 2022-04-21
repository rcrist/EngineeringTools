using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace TestWire.Components
{
    public class Wire
    {
        public Point Pt1 = new Point();
        public Point Pt2 = new Point();
        public Pen Pen = Pens.Black;

        private const int endcap_radius = 3;
        public bool endcapsVisible = false;

        public enum LineShape { Straight, Rectilinear, Spline }
        static public LineShape lineShapeState;

        // constructors
        public Wire()
        {

        }

        public Wire(Pen pen)
        {
            Pen = pen;
        }

        public Wire(Point pt1, Point pt2, Pen pen)
        {
            Pt1 = pt1;
            Pt2 = pt2;
            Pen = pen;
        }

        // Let the wire draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            if (lineShapeState == LineShape.Straight)
                drawNormalLine(gr);
            else if (lineShapeState == LineShape.Rectilinear)
                drawRectilinearLine(gr);
            else if (lineShapeState == LineShape.Spline)
                drawCurvedRectilinearLine(gr);
        }

        private void drawNormalLine(Graphics gr)
        {
            if (Pt1.X != Pt2.X || Pt1.Y != Pt2.Y)
            {
                // Draw L-shaped wire
                gr.DrawLine(this.Pen, Pt1.X, Pt1.Y, Pt1.X, Pt2.Y); // Vertical line
                gr.DrawLine(this.Pen, Pt1.X, Pt2.Y, Pt2.X, Pt2.Y); // Horizontal line
            }
            else
            {
                // Draw straight wire
                gr.DrawLine(this.Pen, Pt1, Pt2);
            }

            // Draw the wire end caps
            drawEndCaps(gr);
        }

        private void drawRectilinearLine(Graphics gr)
        {
            //Debug.WriteLine("Rectilinear line shape!");

            // Draw the rectilinear wire using path
            GraphicsPath path = new GraphicsPath();
            if (Pt2.Y - Pt1.Y >= Pt2.X - Pt1.X) // Y >= X
            {
                path.AddLine(Pt1, new Point(Pt1.X, Pt1.Y - (Pt1.Y - Pt2.Y) / 2));
                path.AddLine(new Point(Pt1.X, Pt1.Y - (Pt1.Y - Pt2.Y) / 2), new Point(Pt2.X, Pt1.Y - (Pt1.Y - Pt2.Y) / 2));
                path.AddLine(new Point(Pt2.X, Pt1.Y - (Pt1.Y - Pt2.Y) / 2), Pt2);
            }
            else // X > Y
            {
                path.AddLine(Pt1, new Point(Pt1.X + (Pt2.X - Pt1.X) / 2, Pt1.Y));
                path.AddLine(new Point(Pt1.X + (Pt2.X - Pt1.X) / 2, Pt1.Y), new Point(Pt1.X + (Pt2.X - Pt1.X) / 2, Pt2.Y));
                path.AddLine(new Point(Pt1.X + (Pt2.X - Pt1.X) / 2, Pt2.Y), Pt2);
            }

            // Draw the path
            gr.DrawPath(this.Pen, path);

            // Draw the wire end caps
            drawEndCaps(gr);
        }

        private void drawCurvedRectilinearLine(Graphics gr)
        {
            PointF p1c, p2c;
            float dX, dY;

            dX = Pt2.X - Pt1.X;
            dY = Pt2.Y - Pt1.Y;

            p1c = new PointF(Pt1.X + dX / 4, Pt1.Y);
            p2c = new PointF(Pt1.X + dX * 3 / 4, Pt2.Y);

            gr.DrawLine(this.Pen, Pt1, p1c);
            drawSpline(gr, p1c, p2c);
            gr.DrawLine(this.Pen, p2c, Pt2);

            // Draw the wire end caps
            drawEndCaps(gr);
        }

        private void drawSpline(Graphics gr, PointF p1c, PointF p2c)
        {
            GraphicsPath path = new GraphicsPath();
            PointF[] pathPts = new PointF[11];

            for (int i=0; i<=10; i++)
            {
                pathPts[i] = evaluateSpline(i * 0.1f, p1c, p2c);
            }

            path.AddLines(pathPts);
            gr.DrawPath(this.Pen, path);
        }

        private PointF evaluateSpline(float t, PointF p1c, PointF p2c)
        {
            float h00, h10, h01, h11;
            float t2 = t * t;
            float t3 = t2 * t;
            float m0x, m0y, m1x, m1y, px, py;


            h00 = 2 * t3 - 3*t2 + 1;
            h10 = t3 - 2 * t2 + t;
            h01 = -2 * t3 + 3 * t2;
            h11 = t3 - t2;

            float xDif = (p2c.X - p1c.X);

            if (xDif > 0.0f && xDif < 0.001f) xDif = 0.001f;
            if (xDif <= 0.0f && xDif > -0.001f) xDif = -0.001f;

            m0x = xDif;
            m0y = 0.0f;
            m1x = xDif;
            m1y = 0.0f;

            px = h00 * p1c.X + h10 * m0x + h01 * p2c.X + h11 * m1x;
            py = h00 * p1c.Y + h10 * m0y + h01 * p2c.Y + h11 * m1y;

            return new PointF(px, py);
        }

        private void drawEndCaps(Graphics gr)
        {
            if (this.endcapsVisible)
            {
                // Draw custom end cap for Pt1
                Rectangle rect1 = new Rectangle(
                     Pt1.X - endcap_radius, Pt1.Y - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect1);     // Rectangular end cap

                // Draw custom end cap for Pt2
                Rectangle rect2 = new Rectangle(
                     Pt2.X - endcap_radius, Pt2.Y - endcap_radius,
                     2 * endcap_radius, 2 * endcap_radius);
                gr.DrawRectangle(Pens.Red, rect2);    // Rectangular end cap
            }
        }

        public void printWire()
        {
            Debug.WriteLine("Wire Pt1: " + Pt1.ToString() + " Pt2: " + Pt2.ToString() +
                            " Shape: " + lineShapeState + " Pen: " + this.Pen.Color.ToString());
        }

        public bool hitTest(Wire wire, Point pt)
        {
            bool hit = false;

            if (Pt1.X != Pt2.X || Pt1.Y != Pt2.Y) // L-shaped line
            {
                // Check for hit on L-shaped wire
                if (wire.Pt1.X == pt.X) // Vertical line hit
                {
                    if (wire.Pt2.Y > wire.Pt1.Y && pt.Y >= wire.Pt1.Y && pt.Y <= wire.Pt2.Y)
                        hit = true;
                    else if (wire.Pt1.Y > wire.Pt2.Y && pt.Y >= wire.Pt2.Y && pt.Y <= wire.Pt1.Y)
                        hit = true;
                }
                else if (wire.Pt2.Y == pt.Y) // Horizontal wire hit
                {
                    if (wire.Pt2.X > wire.Pt1.X && pt.X >= wire.Pt1.X && pt.X <= wire.Pt2.X)
                        hit = true;
                    else if (wire.Pt1.X > wire.Pt2.X && pt.X >= wire.Pt2.X && pt.X <= wire.Pt1.X)
                        hit = true;
                }
                else
                    hit = false;
            }
            else // Straight line
            {
                // Check for hit on straight wire
                if (wire.Pt1.X == wire.Pt2.X && wire.Pt1.X == pt.X) // Vertical wire hit
                    hit = true;
                else if (wire.Pt1.Y == wire.Pt2.Y && wire.Pt1.Y == pt.Y) // Horizontal wire hit
                    hit = true;
                else
                    hit = false;
            }         

            Debug.WriteLine("hit: " + hit);
            return hit;
        }
    }
}

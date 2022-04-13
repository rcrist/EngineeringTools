using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSimulator.Components
{
    public class Switch : DigitalComponent
    {
        // Switch specific variables
        SolidBrush fillColor = new SolidBrush(Color.Black);

        // Logic state changed event declaration
        public event EventHandler logicStateChanged;

        // constructors
        public Switch()
        {
            Debug.WriteLine("Switch Created");
        }

        public Switch(Point pt)
        {
            Debug.WriteLine("Switch Created");
            Location = pt;
        }

        protected virtual void OnLogicStateChanged(EventArgs e)
        {
            logicStateChanged?.Invoke(this, e);
        }

        public void ChangeState()
        {
            logicState = !logicState;
            OnLogicStateChanged(EventArgs.Empty);
        }

        // Let the switch draw itself called from the canvas paint event
        public void Draw(Graphics gr)
        {
            // Draw outer Rectangle
            Rectangle rect = new Rectangle((int)Location.X + 20, (int)Location.Y + 10, 30, 20);
            gr.DrawRectangle(Pens.Black, rect);

            // Draw switch based on switch on/off state
            if (logicState)
            {
                gr.DrawLine(onPen, new Point((int)Location.X + 50, (int)Location.Y + 20), new Point((int)Location.X + 60, (int)Location.Y + 20));
                rect = new Rectangle((int)Location.X + 25, (int)Location.Y + 15, 10, 10);
                gr.DrawRectangle(Pens.Black, rect);
                rect = new Rectangle((int)Location.X + 35, (int)Location.Y + 15, 10, 10);
                gr.FillRectangle(new SolidBrush(Color.Black), rect);
            }
            else
            {
                gr.DrawLine(offPen, new Point((int)Location.X + 50, (int)Location.Y + 20), new Point((int)Location.X + 60, (int)Location.Y + 20));
                rect = new Rectangle((int)Location.X + 35, (int)Location.Y + 15, 10, 10);
                gr.DrawRectangle(Pens.Black, rect);
                rect = new Rectangle((int)Location.X + 25, (int)Location.Y + 15, 10, 10);
                gr.FillRectangle(new SolidBrush(Color.Black), rect);
            }
        }
    }
}

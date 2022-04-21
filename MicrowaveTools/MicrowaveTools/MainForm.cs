// C# class libraries
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

// MathNet.Numerics math libraries
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;

// Custome application class libraries
using MicrowaveTools.Circuits;
using MicrowaveTools.Wires;
using MicrowaveTools.Settings;
using MicrowaveTools.Components;
using MicrowaveTools.Components.Lumped;
using MicrowaveTools.Components.Ideal;
using MicrowaveTools.Components.Microstrip;
using MicrowaveTools.Components.CPW;
using MicrowaveTools.Calculators;
using static MicrowaveTools.Circuits.Circuit;
using static MicrowaveTools.Helper.Globals;

namespace MicrowaveTools
{
    public partial class MainForm : Form
    {
        private Circuit ckt = new Circuit();

        // The grid spacing.
        public const int grid_gap = 10;

        // Mouse event variables
        bool isMouseDown = false;
        int offsetX, offsetY, startX, startY;
        Comp tempComp = new Comp();
        bool lineDrawing = false;
        public bool rotate = false;

        // The wire we are drawing
        private Wire NewWire = null;

        // Points for the new line.
        private Point NewPt1, NewPt2;

        public MainForm()
        {
            InitializeComponent();

            //Debug.WriteLine("Global constants test: Z0 = " + Z0);

            //Resistor res = new Resistor(75.0f, new Point(200, 400), new int[] { 1, 2 });
            //ckt.comps.Add(res);
            //schematicCanvas.Invalidate();
            //propertyGrid1.SelectedObject = res;

            //Inductor ind = new Inductor(5.0f, new Point(300, 400), new int[] { 2, 3 });
            //ckt.comps.Add(ind);
            //schematicCanvas.Invalidate();

            //Capacitor cap = new Capacitor(1.0f, new Point(400, 400), new int[] { 3, 4 });
            //ckt.comps.Add(cap);
            //schematicCanvas.Invalidate();

            //SRL srl = new SRL(75.0f, 5.0f, new Point(500, 400), new int[] { 4, 5 });
            //srl.Orientation = "Shunt";
            //ckt.comps.Add(srl);
            //schematicCanvas.Invalidate();

            //SRC src = new SRC(75.0f, 1.0f, new Point(650, 400), new int[] { 4, 5 });
            //src.Orientation = "Shunt";
            //ckt.comps.Add(src);
            //schematicCanvas.Invalidate();

            //SLC slc = new SLC(5.0f, 1.0f, new Point(800, 400), new int[] { 4, 5 });
            //slc.Orientation = "Shunt";
            //ckt.comps.Add(slc);
            //schematicCanvas.Invalidate();

            //PRL prl = new PRL(75.0f, 5.0f, new Point(500, 400), new int[] { 4, 5 });
            ////prl.Orientation = "Shunt";
            //ckt.comps.Add(prl);
            //schematicCanvas.Invalidate();

            //PRC prc = new PRC(75.0f, 1.0f, new Point(300, 400), new int[] { 4, 5 });
            ////prc.Orientation = "Shunt";
            //ckt.comps.Add(prc);
            //schematicCanvas.Invalidate();

            //PLC plc = new PLC(5.0f, 1.0f, new Point(700, 400), new int[] { 4, 5 });
            ////plc.Orientation = "Shunt";
            //ckt.comps.Add(plc);
            //schematicCanvas.Invalidate();

            //PRLC prlc = new PRLC(75.0f, 5.0f, 1.0f, new Point(500, 400), new int[] { 4, 5 });
            //prlc.Orientation = "Shunt";
            //ckt.comps.Add(prlc);
            //schematicCanvas.Invalidate();
        }

        /* **************************** Grid & Snap Methods **************************** */
        private void DrawBackgroundGrid()
        {
            Bitmap bm = new Bitmap(
                2000,
                2000);
            for (int x = 0; x < 2000; x += grid_gap)
            {
                for (int y = 0; y < 2000; y += grid_gap)
                {
                    bm.SetPixel(x, y, Color.DarkGray); //Color.DarkGray);
                }
            }
            schematicCanvas.BackgroundImage = bm;
        }

        private void SnapToGrid(ref int x, ref int y)
        {
            //if (!chkSnapToGrid.Checked) return;
            x = grid_gap * (int)Math.Round((double)x / grid_gap);
            y = grid_gap * (int)Math.Round((double)y / grid_gap);
        }

        /* **************************** Mouse & Keyboard Handlers **************************** */
        private void schematicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        isMouseDown = true;

                        if (!lineDrawing)
                        {// Snap the start point to the Grid
                            int x = e.X;
                            int y = e.Y;
                            SnapToGrid(ref x, ref y);
                            startX = x;
                            startY = y;

                            // Iterate over the schematic component list and draw each component
                            foreach (Comp comp in ckt.comps)
                            {
                                if (hitTest(comp))
                                {
                                    tempComp = comp;
                                    offsetX = startX - comp.Loc.X;
                                    offsetY = startY - comp.Loc.Y;
                                }
                            }
                        }
                        if (lineDrawing)
                        {
                            // Snap the start point to the Grid
                            int x = e.X;
                            int y = e.Y;
                            SnapToGrid(ref x, ref y);
                            NewPt1 = new Point(x, y);
                            NewPt2 = new Point(x, y);
                            startX = x;
                            startY = y;

                            // Create a new wire and add it to the schematic wires list
                            NewWire = new Wire(); // Use the constructor with no parameters
                            NewWire.Pt1 = NewPt1;
                            NewWire.Pt2 = NewPt2;
                            NewWire.endcapsVisible = true;
                            //schematic.wires.Add(NewWire);

                            // Debugs to show the line points on the console
                            Debug.WriteLine("Line Start Points: (" + NewPt1.X + ", " + NewPt1.Y + " )");

                            // Iterate over the schematic component list and draw each component
                            foreach (Comp comp in ckt.comps)
                            {
                                if (hitTest(comp))
                                {
                                    Debug.WriteLine("Start Component hit!" + comp.ToString());
                                    NewWire.Cin = comp;
                                    //NewWire.Cout = Pout;
                                    comp.wires.Add(NewWire);
                                }
                            }
                        }
                        break;
                    }
                case MouseButtons.Right:
                    {
                        int x = e.X;
                        int y = e.Y;
                        SnapToGrid(ref x, ref y);
                        startX = x;
                        startY = y;
                        Comp hitComp = new Comp();

                        foreach (Comp comp in ckt.comps)
                        {

                            if (hitTest(comp))
                            {
                                hitComp = comp;
                            }                         
                        }

                        using (var form = new CompEditDialog(hitComp))
                        {
                            var result = form.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                hitComp.Name = form.ReturnName;
                                hitComp.Value = form.ReturnValue;
                                schematicCanvas.Refresh();
                            }
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private void schematicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (!lineDrawing)
                {
                    if (tempComp != null)
                    {
                        if (rotate)
                        {
                            if (tempComp.Orientation == "Series")
                                tempComp.Orientation = "Shunt";
                            else if (tempComp.Orientation == "Shunt")
                                tempComp.Orientation = "Series";
                            rotate = false;
                        }

                        int x = e.X;
                        int y = e.Y;
                        SnapToGrid(ref x, ref y);
                        SnapToGrid(ref x, ref y);
                        tempComp.Loc = new Point(x - offsetX, y - offsetY);
                        schematicCanvas.Invalidate();
                    }
                }
                if (lineDrawing)
                {
                    if (NewWire == null) return;
                    // Snap the end point to the Grid
                    int x = e.X;
                    int y = e.Y;
                    SnapToGrid(ref x, ref y);
                    SnapToGrid(ref x, ref y);
                    NewPt2 = new Point(x, y);
                    NewWire.Pt2 = NewPt2;

                    // Redraw, this creates the "rubber band" effect while drawing the wire
                    schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox
                }
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
                startX = x;
                startY = y;

                // Iterate over the schematic component list and draw each component
                foreach (Comp comp in ckt.comps)
                {
                    if (hitTest(comp))
                    {
                        Debug.WriteLine("End Component hit! " + comp.ToString());
                        NewWire.Cout = comp;
                        //if (startY == comp.Location.Y + 20 || startY == comp.Location.Y + 30)
                        //    NewWire.outCompPin = Pin1;
                        //else if (startY == comp.Location.Y + 40)
                        //    NewWire.outCompPin = Pin2;
                    }
                }

                // Update the new wire end points
                NewWire.Pt1 = NewPt1;
                NewWire.Pt2 = NewPt2;
                NewWire.endcapsVisible = false;

                // Terminate any further updates to the new wire, this makes it permanent in the schematic wires list
                NewWire = null;

                // Redraw.
                schematicCanvas.Invalidate(); // Refresh the drawing canvas pictureBox

                // Debugs to show the line points on the console
                Debug.WriteLine("Line End Points: (" + NewPt2.X + ", " + NewPt2.Y + " )");
            }
            //lineDrawing = false;
        }

        /* **************************** Menu Button Handlers **************************** */
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                //hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void hideSubMenu(Panel subMenu)
        {
            if(subMenu == panelLumpedSubmenu)
            {
                panelIdealSubmenu.Visible = false;
                panelMicrostripSubmenu.Visible = false;
                panelCPWSubmenu.Visible = false;
            }
            else if(subMenu == panelIdealSubmenu) 
            {
                panelLumpedSubmenu.Visible = false;
                panelMicrostripSubmenu.Visible = false;
                panelCPWSubmenu.Visible = false;
            }
            else if(subMenu == panelMicrostripSubmenu)
            {
                panelLumpedSubmenu.Visible = false;
                panelIdealSubmenu.Visible = false;
                panelCPWSubmenu.Visible = false;
            }
            else if(subMenu == panelCPWSubmenu)
            {
                panelLumpedSubmenu.Visible = false;
                panelIdealSubmenu.Visible = false;
                panelMicrostripSubmenu.Visible = false;
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCircuit_Click(object sender, EventArgs e)
        {
            showSubMenu(panelCircuitSubmenu);
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            showSubMenu(panelToolsSubmenu);
        }

        private void btnWire_Click(object sender, EventArgs e)
        {
            lineDrawing = !lineDrawing;
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        // Save the circuit.
            //        ckt.SaveCircuit(saveFileDialog1.FileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        // Load the network;
            //        Circuit circuit = Circuit.LoadCircuit(openFileDialog1.FileName);

            //        // Start using the new network.
            //        ckt = circuit;

            //        // Draw the new network.
            //        schematicCanvas.Refresh();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ckt = new Circuit();
            schematicCanvas.Refresh();
        }

        private void btnLumped_Click(object sender, EventArgs e)
        {
            showSubMenu(panelLumpedSubmenu);
            hideSubMenu(panelLumpedSubmenu);
        }

        private void btnIdeal_Click(object sender, EventArgs e)
        {
            showSubMenu(panelIdealSubmenu);
            hideSubMenu(panelIdealSubmenu);
        }

        private void btnMicrostrip_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMicrostripSubmenu);
            hideSubMenu(panelMicrostripSubmenu);
        }

        private void btnResistor_Click(object sender, EventArgs e)
        {
            Resistor res = new Resistor(75.0f, new Point(400, 300), new int[] { 0, 0 });
            ckt.comps.Add(res);
            schematicCanvas.Invalidate();
        }

        private void btnInductor_Click(object sender, EventArgs e)
        {
            Inductor ind = new Inductor(5.0f, new Point(500, 300), new int[] { 0, 0 });
            ckt.comps.Add(ind);
            schematicCanvas.Invalidate();
        }

        private void btnCapacitor_Click(object sender, EventArgs e)
        {
            Capacitor cap = new Capacitor(1.0f, new Point(600, 300), new int[] { 0, 0 });
            ckt.comps.Add(cap);
            schematicCanvas.Invalidate();
        }

        private void btnInPort_Click(object sender, EventArgs e)
        {
            InPort pin = new InPort(50.0f, new Point(300, 300), new int[] { 0, 0 });
            ckt.comps.Add(pin);
            schematicCanvas.Invalidate();
        }

        private void btnOutPort_Click(object sender, EventArgs e)
        {
            OutPort pout = new OutPort(50.0f, new Point(700, 300), new int[] { 0, 0 });
            ckt.comps.Add(pout);
            schematicCanvas.Invalidate();
        }

        private void btnNode_Click(object sender, EventArgs e)
        {
            Node node = new Node(0.0f, new Point(300, 300), new int[] { 0, 0 });
            ckt.comps.Add(node);
            schematicCanvas.Invalidate();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSettingsSubmenu);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 114 || e.KeyChar == 82)    // 114 = r, 82 = R
                rotate = !rotate;
        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            float fStart = Settings.Settings.fStart;
            float fStep = Settings.Settings.fStep;
            float fStop = Settings.Settings.fStop;
            float f;

            // Variables to store analysis results

            ResultRecord result = new ResultRecord();
            
            int numFreq = (int)((fStop - fStart) / fStep)+1;

            ckt.Traverse();

            Console.WriteLine("f\t" + "S11\t\t\t\t" + "S12\t\t\t\t" + "S21\t\t\t\t" + "S22");
            for (int i=0; i<numFreq; i++)
            {
                //f = 2.0f;
                f = (fStart + fStep * i);
                ckt.Y = Matrix<Complex32>.Build.Dense(0, 0);

                ckt.AnalyzeCircuit(f*1e9f);           

                if (ckt.S == null)
                    Console.WriteLine("Hit the Analyze button first!");

                if (Settings.Settings.dt == DataType.re_im)
                {
                    Console.WriteLine(f + "  \t" + String.Format("{0:0.###}", ckt.S[0, 0]) +
                                          "  \t" + String.Format("{0:0.###}", ckt.S[0, 1]) +
                                          "  \t" + String.Format("{0:0.###}", ckt.S[1, 0]) +
                                          "  \t" + String.Format("{0:0.###}", ckt.S[1, 1]));
                }
                if (Settings.Settings.dt == DataType.mag_ang)
                {
                    double S11_mag = ckt.S[0, 0].Magnitude;
                    double S11_ang = ckt.S[0, 0].Phase * 180.0 / Math.PI;
                    double S12_mag = ckt.S[0, 1].Magnitude;
                    double S12_ang = ckt.S[0, 1].Phase * 180.0 / Math.PI;
                    double S21_mag = ckt.S[1, 0].Magnitude;
                    double S21_ang = ckt.S[1, 0].Phase * 180.0 / Math.PI;
                    double S22_mag = ckt.S[1, 1].Magnitude;
                    double S22_ang = ckt.S[1, 1].Phase * 180.0 / Math.PI;
  
                    Console.WriteLine(f + "  \t" + String.Format("{0:0.###}", S11_mag) + "<" + String.Format("{0:0.###}", S11_ang) +
                                          "  \t" + String.Format("{0:0.###}", S12_mag) + "<" + String.Format("{0:0.###}", S12_ang) +
                                          "  \t" + String.Format("{0:0.###}", S21_mag) + "<" + String.Format("{0:0.###}", S21_ang) +
                                          "  \t" + String.Format("{0:0.###}", S22_mag) + "<" + String.Format("{0:0.###}", S22_ang));

                    result.f = f;
                    result.S11_1 = S11_mag;
                    result.S11_2 = S11_ang;
                    result.S12_1 = S12_mag;
                    result.S12_2 = S12_ang;
                    result.S21_1 = S21_mag;
                    result.S21_2 = S21_ang;
                    result.S22_1 = S22_mag;
                    result.S22_2 = S22_ang;
                    ckt.results.Add(result);
                }
                if (Settings.Settings.dt == DataType.magDB_ang)
                {
                    double S11_magDB = 20 * Math.Log10(ckt.S[0, 0].Magnitude);
                    double S11_ang = ckt.S[0, 0].Phase * 180.0 / Math.PI;
                    double S12_magDB = 20 * Math.Log10(ckt.S[0, 1].Magnitude);
                    double S12_ang = ckt.S[0, 1].Phase * 180.0 / Math.PI;
                    double S21_magDB = 20 * Math.Log10(ckt.S[1, 0].Magnitude);
                    double S21_ang = ckt.S[1, 0].Phase * 180.0 / Math.PI;
                    double S22_magDB = 20 * Math.Log10(ckt.S[1, 1].Magnitude);
                    double S22_ang = ckt.S[1, 1].Phase * 180.0 / Math.PI;

                    Console.WriteLine(f + "  \t" + String.Format("{0:0.###}", S11_magDB) + "<" + String.Format("{0:0.###}", S11_ang) +
                                          "  \t" + String.Format("{0:0.###}", S12_magDB) + "<" + String.Format("{0:0.###}", S12_ang) +
                                          "  \t" + String.Format("{0:0.###}", S21_magDB) + "<" + String.Format("{0:0.###}", S21_ang) +
                                          "  \t" + String.Format("{0:0.###}", S22_magDB) + "<" + String.Format("{0:0.###}", S22_ang));
                }
            } // end of frequency loop
            DataDisplayForm dataDisplayForm = new DataDisplayForm(ckt);
            dataDisplayForm.Show();
        }

        private void btnGnd_Click(object sender, EventArgs e)
        {
            Ground gnd = new Ground();
            ckt.comps.Add(gnd);
            schematicCanvas.Refresh();
        }

        private void btnCreateRLC_Click(object sender, EventArgs e)
        {
            int[] nodes = new int[] { 0, 0 };
            InPort pin = new InPort(50.0f, new Point(360, 230), nodes);
            ckt.comps.Add(pin);

            nodes = new int[] { 0, 0 };
            Resistor res = new Resistor(75.0f, new Point(400, 230), nodes);
            ckt.comps.Add(res);

            nodes = new int[] { 0, 0 };
            Inductor ind = new Inductor(5.0f, new Point(500, 230), nodes);
            ckt.comps.Add(ind);

            nodes = new int[] { 0, 0 };
            Capacitor cap = new Capacitor(1.0f, new Point(600, 230), nodes);
            ckt.comps.Add(cap);

            nodes = new int[] { 0, 0 };
            OutPort pout = new OutPort(50.0f, new Point(700, 230), nodes);
            ckt.comps.Add(pout);

            _ = new Wire(pin, res); // Use of discard _
            _ = new Wire(res, ind);
            _ = new Wire(ind, cap);
            _ = new Wire(cap, pout);

            schematicCanvas.Invalidate();
        }

        private void btnSRC_Click(object sender, EventArgs e)
        {
            SRC src = new SRC(75.0f, 1.0f, new Point(600, 300), new int[] { 0, 0 });
            ckt.comps.Add(src);
            schematicCanvas.Invalidate();
        }

        private void btnSRL_Click(object sender, EventArgs e)
        {
            SRL srl = new SRL(75.0f, 5.0f, new Point(600, 300), new int[] { 0, 0 });
            ckt.comps.Add(srl);
            schematicCanvas.Invalidate();
        }

        private void btnSLC_Click(object sender, EventArgs e)
        {
            SLC slc = new SLC(5.0f, 1.0f, new Point(600, 300), new int[] { 0, 0 });
            ckt.comps.Add(slc);
            schematicCanvas.Invalidate();
        }

        private void btnPRC_Click(object sender, EventArgs e)
        {
            PRC prc = new PRC(75.0f, 1.0f, new Point(600, 300), new int[] { 0, 0 });
            ckt.comps.Add(prc);
            schematicCanvas.Invalidate();
        }

        private void btnPRL_Click(object sender, EventArgs e)
        {
            PRL prl = new PRL(75.0f, 5.0f, new Point(600, 300), new int[] { 0, 0 });
            ckt.comps.Add(prl);
            schematicCanvas.Invalidate();
        }

        private void btnPLC_Click(object sender, EventArgs e)
        {
            PLC plc = new PLC(5.0f, 1.0f, new Point(600, 300), new int[] { 0, 0 });
            ckt.comps.Add(plc);
            schematicCanvas.Invalidate();
        }

        private void btnSRLC_Click(object sender, EventArgs e)
        {
            SRLC srlc = new SRLC(75.0f, 5.0f, 1.0f, new Point(600, 300), new int[] { 0, 0 });
            ckt.comps.Add(srlc);
            schematicCanvas.Invalidate();
        }

        private void btnPRLC_Click(object sender, EventArgs e)
        {
            PRLC prlc = new PRLC(75.0f, 5.0f, 1.0f, new Point(600, 300), new int[] { 0, 0 });
            ckt.comps.Add(prlc);
            schematicCanvas.Invalidate();
        }

        private void btnTLIN_Click(object sender, EventArgs e)
        {
            TLIN tlin = new TLIN(50, 100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(tlin);
            schematicCanvas.Invalidate();
        }

        private void btnTLOC_Click(object sender, EventArgs e)
        {
            TLOC tloc = new TLOC(50, 100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(tloc);
            schematicCanvas.Invalidate();
        }

        private void btnTLSC_Click(object sender, EventArgs e)
        {
            STLSC tlsc = new STLSC(50, 100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(tlsc);
            schematicCanvas.Invalidate();
        }

        private void btnMLIN_Click(object sender, EventArgs e)
        {
            MLIN mlin = new MLIN(100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mlin);
            schematicCanvas.Invalidate();
        }

        private void btnMCORNERL_Click(object sender, EventArgs e)
        {
            MCORNERL mcornl = new MCORNERL(100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mcornl);
            schematicCanvas.Invalidate();
        }

        private void btnMCORNERR_Click(object sender, EventArgs e)
        {
            MCORNERR mcornr = new MCORNERR(100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mcornr);
            schematicCanvas.Invalidate();
        }

        private void btnMMITERL_Click(object sender, EventArgs e)
        {
            MMITERL mmitl = new MMITERL(100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mmitl);
            schematicCanvas.Invalidate();
        }

        private void btnMMITERR_Click(object sender, EventArgs e)
        {
            MMITERR mmitr = new MMITERR(100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mmitr);
            schematicCanvas.Invalidate();
        }

        private void btnMCROSS_Click(object sender, EventArgs e)
        {
            MCROSS mcross = new MCROSS(100, 100, 100, 100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mcross);
            schematicCanvas.Invalidate();
        }

        private void btnMTEE_Click(object sender, EventArgs e)
        {
            MTEE mtee = new MTEE(100, 100, 100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mtee);
            schematicCanvas.Invalidate();
        }

        private void btnMOPEN_Click(object sender, EventArgs e)
        {
            MOPEN mopen = new MOPEN(100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mopen);
            schematicCanvas.Invalidate();
        }

        private void btnMGAP_Click(object sender, EventArgs e)
        {
            MGAP mgap = new MGAP(100, 100, 10, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mgap);
            schematicCanvas.Invalidate();
        }

        private void btnMSTEP_Click(object sender, EventArgs e)
        {
            MSTEP mstep = new MSTEP(100, 100, new Point(400, 300), new int[] { 1, 2 });
            ckt.comps.Add(mstep);
            schematicCanvas.Invalidate();
        }

        private void btnMVIA_Click(object sender, EventArgs e)
        {
            MVIA mvia = new MVIA(100, 100, new Point(600, 300), new int[] { 1, 2 });
            ckt.comps.Add(mvia);
            schematicCanvas.Invalidate();
        }

        private void btnMCPLIN_Click(object sender, EventArgs e)
        {
            MCPLIN mcplin = new MCPLIN(100, 100, 100, new Point(800, 500), new int[] { 1, 2 });
            ckt.comps.Add(mcplin);
            schematicCanvas.Invalidate();
        }

        private void btnCPWLIN_Click(object sender, EventArgs e)
        {
            CPWLIN cpwlin = new CPWLIN(100, 100, 100, 1, 0, new Point(800, 500), new int[] { 1, 2 });
            ckt.comps.Add(cpwlin);
            schematicCanvas.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showSubMenu(panelCPWSubmenu);
            hideSubMenu(panelCPWSubmenu);
        }

        private void btnCPWOPEN_Click(object sender, EventArgs e)
        {
            CPWOPEN copen = new CPWOPEN(100, 100, 100, 100, 1, 0, new Point(800, 500), new int[] { 1, 2 });
            ckt.comps.Add(copen);
            schematicCanvas.Invalidate();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            CPWSHORT csht = new CPWSHORT(100, 100, 100, 100, 1, 0, new Point(800, 500), new int[] { 1, 2 });
            ckt.comps.Add(csht);
            schematicCanvas.Invalidate();
        }

        private void btnCPWSTEP_Click(object sender, EventArgs e)
        {
            CPWSTEP cstp = new CPWSTEP(100, 100, 100, 100, 100, 1, 0, new Point(800, 500), new int[] { 1, 2 });
            ckt.comps.Add(cstp);
            schematicCanvas.Invalidate();
        }

        private void btnGAP_Click(object sender, EventArgs e)
        {
            CPWGAP cgap = new CPWGAP(100, 100, 100, 100, 1, 0, new Point(800, 500), new int[] { 1, 2 });
            ckt.comps.Add(cgap);
            schematicCanvas.Invalidate();
        }

        private void btnMicrostripCalc_Click(object sender, EventArgs e)
        {
            MicrostripCalcForm microstripCalcForm = new MicrostripCalcForm();
            microstripCalcForm.Show();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            showSubMenu(panelHelpSubmenu);
        }

        /* **************************** Canvas Change Handlers **************************** */
        private void schematicCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (Comp comp in ckt.comps)
            {
                comp.Draw(e.Graphics);
                schematicCanvas.Invalidate();

                foreach (Wire wire in comp.wires)
                {
                    wire.Draw(e.Graphics);
                }
                schematicCanvas.Invalidate();
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            DrawBackgroundGrid();
        }

        /* **************************** Helper Functions **************************** */
        private bool hitTest(Comp comp)
        {
            bool hit;

            hit = false;
            if ((startX >= comp.Loc.X && startX <= comp.Loc.X + comp.Width) &&
                (startY >= comp.Loc.Y && startY <= comp.Loc.Y + comp.Height))
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
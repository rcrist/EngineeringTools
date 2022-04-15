
namespace MicrowaveTools
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMainMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCircuit = new System.Windows.Forms.Button();
            this.panelCircuitSubmenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnStore = new System.Windows.Forms.Button();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnWireMode = new System.Windows.Forms.Button();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.panelCompMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLumped = new System.Windows.Forms.Button();
            this.btnIdeal = new System.Windows.Forms.Button();
            this.btnMicrostrip = new System.Windows.Forms.Button();
            this.btnRLC = new System.Windows.Forms.Button();
            this.schematicCanvas = new System.Windows.Forms.PictureBox();
            this.panelLumpedSubmenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRES = new System.Windows.Forms.Button();
            this.btnIND = new System.Windows.Forms.Button();
            this.btnCAP = new System.Windows.Forms.Button();
            this.panelIdealSubmenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnInPort = new System.Windows.Forms.Button();
            this.btnOutPort = new System.Windows.Forms.Button();
            this.btnGround = new System.Windows.Forms.Button();
            this.panelMicrostripSubmenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMLIN = new System.Windows.Forms.Button();
            this.btnMCROS = new System.Windows.Forms.Button();
            this.btnMTEE = new System.Windows.Forms.Button();
            this.panelMainMenu.SuspendLayout();
            this.panelCircuitSubmenu.SuspendLayout();
            this.panelCompMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).BeginInit();
            this.panelLumpedSubmenu.SuspendLayout();
            this.panelIdealSubmenu.SuspendLayout();
            this.panelMicrostripSubmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainMenu
            // 
            this.panelMainMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMainMenu.Controls.Add(this.label1);
            this.panelMainMenu.Controls.Add(this.btnCircuit);
            this.panelMainMenu.Controls.Add(this.panelCircuitSubmenu);
            this.panelMainMenu.Controls.Add(this.btnWireMode);
            this.panelMainMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMainMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelMainMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMainMenu.Name = "panelMainMenu";
            this.panelMainMenu.Size = new System.Drawing.Size(150, 753);
            this.panelMainMenu.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "Microwave Tools";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCircuit
            // 
            this.btnCircuit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCircuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnCircuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCircuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCircuit.ForeColor = System.Drawing.Color.LightGray;
            this.btnCircuit.Location = new System.Drawing.Point(3, 69);
            this.btnCircuit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCircuit.Name = "btnCircuit";
            this.btnCircuit.Size = new System.Drawing.Size(145, 33);
            this.btnCircuit.TabIndex = 10;
            this.btnCircuit.Text = "Circuit";
            this.btnCircuit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCircuit.UseVisualStyleBackColor = true;
            this.btnCircuit.Click += new System.EventHandler(this.btnCircuit_Click);
            // 
            // panelCircuitSubmenu
            // 
            this.panelCircuitSubmenu.Controls.Add(this.btnNew);
            this.panelCircuitSubmenu.Controls.Add(this.btnLoad);
            this.panelCircuitSubmenu.Controls.Add(this.btnStore);
            this.panelCircuitSubmenu.Controls.Add(this.btnAnalyze);
            this.panelCircuitSubmenu.Location = new System.Drawing.Point(3, 105);
            this.panelCircuitSubmenu.Name = "panelCircuitSubmenu";
            this.panelCircuitSubmenu.Size = new System.Drawing.Size(146, 134);
            this.panelCircuitSubmenu.TabIndex = 11;
            this.panelCircuitSubmenu.Visible = false;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnNew.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.LightGray;
            this.btnNew.Location = new System.Drawing.Point(0, 0);
            this.btnNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(143, 33);
            this.btnNew.TabIndex = 10;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnLoad.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.ForeColor = System.Drawing.Color.LightGray;
            this.btnLoad.Location = new System.Drawing.Point(0, 33);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(0);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(143, 33);
            this.btnLoad.TabIndex = 11;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = false;
            // 
            // btnStore
            // 
            this.btnStore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnStore.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnStore.FlatAppearance.BorderSize = 0;
            this.btnStore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStore.ForeColor = System.Drawing.Color.LightGray;
            this.btnStore.Location = new System.Drawing.Point(0, 66);
            this.btnStore.Margin = new System.Windows.Forms.Padding(0);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(143, 33);
            this.btnStore.TabIndex = 12;
            this.btnStore.Text = "Store";
            this.btnStore.UseVisualStyleBackColor = false;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnAnalyze.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAnalyze.FlatAppearance.BorderSize = 0;
            this.btnAnalyze.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnAnalyze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalyze.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalyze.ForeColor = System.Drawing.Color.LightGray;
            this.btnAnalyze.Location = new System.Drawing.Point(0, 99);
            this.btnAnalyze.Margin = new System.Windows.Forms.Padding(0);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(143, 33);
            this.btnAnalyze.TabIndex = 13;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = false;
            // 
            // btnWireMode
            // 
            this.btnWireMode.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnWireMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnWireMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWireMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWireMode.ForeColor = System.Drawing.Color.LightGray;
            this.btnWireMode.Location = new System.Drawing.Point(3, 242);
            this.btnWireMode.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnWireMode.Name = "btnWireMode";
            this.btnWireMode.Size = new System.Drawing.Size(145, 33);
            this.btnWireMode.TabIndex = 12;
            this.btnWireMode.Text = "Wire Mode";
            this.btnWireMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWireMode.UseVisualStyleBackColor = true;
            // 
            // propertyGrid
            // 
            this.propertyGrid.CategoryForeColor = System.Drawing.Color.White;
            this.propertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid.HelpBackColor = System.Drawing.Color.Black;
            this.propertyGrid.HelpBorderColor = System.Drawing.Color.DimGray;
            this.propertyGrid.HelpForeColor = System.Drawing.Color.White;
            this.propertyGrid.Location = new System.Drawing.Point(1052, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(130, 753);
            this.propertyGrid.TabIndex = 1;
            this.propertyGrid.ViewBackColor = System.Drawing.Color.Black;
            this.propertyGrid.ViewBorderColor = System.Drawing.Color.DimGray;
            this.propertyGrid.ViewForeColor = System.Drawing.Color.White;
            // 
            // panelCompMenu
            // 
            this.panelCompMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCompMenu.Controls.Add(this.btnLumped);
            this.panelCompMenu.Controls.Add(this.btnIdeal);
            this.panelCompMenu.Controls.Add(this.btnMicrostrip);
            this.panelCompMenu.Controls.Add(this.btnRLC);
            this.panelCompMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCompMenu.Location = new System.Drawing.Point(150, 0);
            this.panelCompMenu.Name = "panelCompMenu";
            this.panelCompMenu.Size = new System.Drawing.Size(902, 40);
            this.panelCompMenu.TabIndex = 2;
            // 
            // btnLumped
            // 
            this.btnLumped.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLumped.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnLumped.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLumped.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLumped.ForeColor = System.Drawing.Color.LightGray;
            this.btnLumped.Location = new System.Drawing.Point(3, 0);
            this.btnLumped.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLumped.Name = "btnLumped";
            this.btnLumped.Size = new System.Drawing.Size(145, 33);
            this.btnLumped.TabIndex = 11;
            this.btnLumped.Text = "Lumped";
            this.btnLumped.UseVisualStyleBackColor = true;
            this.btnLumped.Click += new System.EventHandler(this.btnLumped_Click);
            // 
            // btnIdeal
            // 
            this.btnIdeal.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnIdeal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnIdeal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIdeal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIdeal.ForeColor = System.Drawing.Color.LightGray;
            this.btnIdeal.Location = new System.Drawing.Point(154, 0);
            this.btnIdeal.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnIdeal.Name = "btnIdeal";
            this.btnIdeal.Size = new System.Drawing.Size(145, 33);
            this.btnIdeal.TabIndex = 12;
            this.btnIdeal.Text = "Ideal";
            this.btnIdeal.UseVisualStyleBackColor = true;
            this.btnIdeal.Click += new System.EventHandler(this.btnIdeal_Click);
            // 
            // btnMicrostrip
            // 
            this.btnMicrostrip.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMicrostrip.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnMicrostrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMicrostrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMicrostrip.ForeColor = System.Drawing.Color.LightGray;
            this.btnMicrostrip.Location = new System.Drawing.Point(305, 0);
            this.btnMicrostrip.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnMicrostrip.Name = "btnMicrostrip";
            this.btnMicrostrip.Size = new System.Drawing.Size(145, 33);
            this.btnMicrostrip.TabIndex = 13;
            this.btnMicrostrip.Text = "Microstrip";
            this.btnMicrostrip.UseVisualStyleBackColor = true;
            this.btnMicrostrip.Click += new System.EventHandler(this.btnMicrostrip_Click);
            // 
            // btnRLC
            // 
            this.btnRLC.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRLC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnRLC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRLC.ForeColor = System.Drawing.Color.LightGray;
            this.btnRLC.Location = new System.Drawing.Point(456, 0);
            this.btnRLC.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnRLC.Name = "btnRLC";
            this.btnRLC.Size = new System.Drawing.Size(145, 33);
            this.btnRLC.TabIndex = 14;
            this.btnRLC.Text = "RLC";
            this.btnRLC.UseVisualStyleBackColor = true;
            // 
            // schematicCanvas
            // 
            this.schematicCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicCanvas.Location = new System.Drawing.Point(150, 40);
            this.schematicCanvas.Margin = new System.Windows.Forms.Padding(0);
            this.schematicCanvas.Name = "schematicCanvas";
            this.schematicCanvas.Size = new System.Drawing.Size(902, 713);
            this.schematicCanvas.TabIndex = 3;
            this.schematicCanvas.TabStop = false;
            this.schematicCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.schematicCanvas_Paint);
            // 
            // panelLumpedSubmenu
            // 
            this.panelLumpedSubmenu.Controls.Add(this.btnRES);
            this.panelLumpedSubmenu.Controls.Add(this.btnIND);
            this.panelLumpedSubmenu.Controls.Add(this.btnCAP);
            this.panelLumpedSubmenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelLumpedSubmenu.Location = new System.Drawing.Point(153, 42);
            this.panelLumpedSubmenu.Name = "panelLumpedSubmenu";
            this.panelLumpedSubmenu.Size = new System.Drawing.Size(140, 165);
            this.panelLumpedSubmenu.TabIndex = 4;
            this.panelLumpedSubmenu.Visible = false;
            // 
            // btnRES
            // 
            this.btnRES.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnRES.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRES.FlatAppearance.BorderSize = 0;
            this.btnRES.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnRES.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRES.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRES.ForeColor = System.Drawing.Color.LightGray;
            this.btnRES.Location = new System.Drawing.Point(0, 0);
            this.btnRES.Margin = new System.Windows.Forms.Padding(0);
            this.btnRES.Name = "btnRES";
            this.btnRES.Size = new System.Drawing.Size(140, 33);
            this.btnRES.TabIndex = 12;
            this.btnRES.Text = "RES";
            this.btnRES.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRES.UseVisualStyleBackColor = false;
            this.btnRES.Click += new System.EventHandler(this.btnRES_Click);
            // 
            // btnIND
            // 
            this.btnIND.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnIND.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnIND.FlatAppearance.BorderSize = 0;
            this.btnIND.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnIND.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIND.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIND.ForeColor = System.Drawing.Color.LightGray;
            this.btnIND.Location = new System.Drawing.Point(0, 33);
            this.btnIND.Margin = new System.Windows.Forms.Padding(0);
            this.btnIND.Name = "btnIND";
            this.btnIND.Size = new System.Drawing.Size(140, 33);
            this.btnIND.TabIndex = 13;
            this.btnIND.Text = "IND";
            this.btnIND.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIND.UseVisualStyleBackColor = false;
            this.btnIND.Click += new System.EventHandler(this.btnIND_Click);
            // 
            // btnCAP
            // 
            this.btnCAP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnCAP.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCAP.FlatAppearance.BorderSize = 0;
            this.btnCAP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnCAP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCAP.ForeColor = System.Drawing.Color.LightGray;
            this.btnCAP.Location = new System.Drawing.Point(0, 66);
            this.btnCAP.Margin = new System.Windows.Forms.Padding(0);
            this.btnCAP.Name = "btnCAP";
            this.btnCAP.Size = new System.Drawing.Size(140, 33);
            this.btnCAP.TabIndex = 14;
            this.btnCAP.Text = "CAP";
            this.btnCAP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCAP.UseVisualStyleBackColor = false;
            this.btnCAP.Click += new System.EventHandler(this.btnCAP_Click);
            // 
            // panelIdealSubmenu
            // 
            this.panelIdealSubmenu.Controls.Add(this.btnInPort);
            this.panelIdealSubmenu.Controls.Add(this.btnOutPort);
            this.panelIdealSubmenu.Controls.Add(this.btnGround);
            this.panelIdealSubmenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelIdealSubmenu.Location = new System.Drawing.Point(299, 42);
            this.panelIdealSubmenu.Name = "panelIdealSubmenu";
            this.panelIdealSubmenu.Size = new System.Drawing.Size(140, 166);
            this.panelIdealSubmenu.TabIndex = 14;
            this.panelIdealSubmenu.Visible = false;
            // 
            // btnInPort
            // 
            this.btnInPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnInPort.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnInPort.FlatAppearance.BorderSize = 0;
            this.btnInPort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnInPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInPort.ForeColor = System.Drawing.Color.LightGray;
            this.btnInPort.Location = new System.Drawing.Point(0, 0);
            this.btnInPort.Margin = new System.Windows.Forms.Padding(0);
            this.btnInPort.Name = "btnInPort";
            this.btnInPort.Size = new System.Drawing.Size(140, 33);
            this.btnInPort.TabIndex = 9;
            this.btnInPort.Text = "InPort";
            this.btnInPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInPort.UseVisualStyleBackColor = false;
            this.btnInPort.Click += new System.EventHandler(this.btnInPort_Click);
            // 
            // btnOutPort
            // 
            this.btnOutPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnOutPort.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOutPort.FlatAppearance.BorderSize = 0;
            this.btnOutPort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnOutPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutPort.ForeColor = System.Drawing.Color.LightGray;
            this.btnOutPort.Location = new System.Drawing.Point(0, 33);
            this.btnOutPort.Margin = new System.Windows.Forms.Padding(0);
            this.btnOutPort.Name = "btnOutPort";
            this.btnOutPort.Size = new System.Drawing.Size(140, 33);
            this.btnOutPort.TabIndex = 10;
            this.btnOutPort.Text = "OutPort";
            this.btnOutPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOutPort.UseVisualStyleBackColor = false;
            this.btnOutPort.Click += new System.EventHandler(this.btnOutPort_Click);
            // 
            // btnGround
            // 
            this.btnGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnGround.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGround.FlatAppearance.BorderSize = 0;
            this.btnGround.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnGround.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGround.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGround.ForeColor = System.Drawing.Color.LightGray;
            this.btnGround.Location = new System.Drawing.Point(0, 66);
            this.btnGround.Margin = new System.Windows.Forms.Padding(0);
            this.btnGround.Name = "btnGround";
            this.btnGround.Size = new System.Drawing.Size(140, 33);
            this.btnGround.TabIndex = 11;
            this.btnGround.Text = "Ground";
            this.btnGround.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGround.UseVisualStyleBackColor = false;
            this.btnGround.Click += new System.EventHandler(this.btnGround_Click);
            // 
            // panelMicrostripSubmenu
            // 
            this.panelMicrostripSubmenu.Controls.Add(this.btnMLIN);
            this.panelMicrostripSubmenu.Controls.Add(this.btnMCROS);
            this.panelMicrostripSubmenu.Controls.Add(this.btnMTEE);
            this.panelMicrostripSubmenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelMicrostripSubmenu.Location = new System.Drawing.Point(445, 42);
            this.panelMicrostripSubmenu.Name = "panelMicrostripSubmenu";
            this.panelMicrostripSubmenu.Size = new System.Drawing.Size(140, 165);
            this.panelMicrostripSubmenu.TabIndex = 15;
            this.panelMicrostripSubmenu.Visible = false;
            // 
            // btnMLIN
            // 
            this.btnMLIN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnMLIN.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMLIN.FlatAppearance.BorderSize = 0;
            this.btnMLIN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnMLIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMLIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMLIN.ForeColor = System.Drawing.Color.LightGray;
            this.btnMLIN.Location = new System.Drawing.Point(0, 0);
            this.btnMLIN.Margin = new System.Windows.Forms.Padding(0);
            this.btnMLIN.Name = "btnMLIN";
            this.btnMLIN.Size = new System.Drawing.Size(140, 33);
            this.btnMLIN.TabIndex = 9;
            this.btnMLIN.Text = "MLIN";
            this.btnMLIN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMLIN.UseVisualStyleBackColor = false;
            // 
            // btnMCROS
            // 
            this.btnMCROS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnMCROS.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMCROS.FlatAppearance.BorderSize = 0;
            this.btnMCROS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnMCROS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMCROS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMCROS.ForeColor = System.Drawing.Color.LightGray;
            this.btnMCROS.Location = new System.Drawing.Point(0, 33);
            this.btnMCROS.Margin = new System.Windows.Forms.Padding(0);
            this.btnMCROS.Name = "btnMCROS";
            this.btnMCROS.Size = new System.Drawing.Size(140, 33);
            this.btnMCROS.TabIndex = 10;
            this.btnMCROS.Text = "MCROS";
            this.btnMCROS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMCROS.UseVisualStyleBackColor = false;
            // 
            // btnMTEE
            // 
            this.btnMTEE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnMTEE.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMTEE.FlatAppearance.BorderSize = 0;
            this.btnMTEE.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnMTEE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMTEE.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMTEE.ForeColor = System.Drawing.Color.LightGray;
            this.btnMTEE.Location = new System.Drawing.Point(0, 66);
            this.btnMTEE.Margin = new System.Windows.Forms.Padding(0);
            this.btnMTEE.Name = "btnMTEE";
            this.btnMTEE.Size = new System.Drawing.Size(140, 33);
            this.btnMTEE.TabIndex = 11;
            this.btnMTEE.Text = "MTEE";
            this.btnMTEE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMTEE.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.panelIdealSubmenu);
            this.Controls.Add(this.panelMicrostripSubmenu);
            this.Controls.Add(this.panelLumpedSubmenu);
            this.Controls.Add(this.schematicCanvas);
            this.Controls.Add(this.panelCompMenu);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.panelMainMenu);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Microwave Tools";
            this.panelMainMenu.ResumeLayout(false);
            this.panelCircuitSubmenu.ResumeLayout(false);
            this.panelCompMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).EndInit();
            this.panelLumpedSubmenu.ResumeLayout(false);
            this.panelIdealSubmenu.ResumeLayout(false);
            this.panelMicrostripSubmenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelMainMenu;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.FlowLayoutPanel panelCompMenu;
        private System.Windows.Forms.PictureBox schematicCanvas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCircuit;
        private System.Windows.Forms.FlowLayoutPanel panelCircuitSubmenu;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnWireMode;
        private System.Windows.Forms.Button btnLumped;
        private System.Windows.Forms.Button btnIdeal;
        private System.Windows.Forms.Button btnMicrostrip;
        private System.Windows.Forms.Button btnRLC;
        private System.Windows.Forms.FlowLayoutPanel panelLumpedSubmenu;
        private System.Windows.Forms.Button btnRES;
        private System.Windows.Forms.Button btnIND;
        private System.Windows.Forms.Button btnCAP;
        private System.Windows.Forms.FlowLayoutPanel panelIdealSubmenu;
        private System.Windows.Forms.Button btnInPort;
        private System.Windows.Forms.Button btnOutPort;
        private System.Windows.Forms.Button btnGround;
        private System.Windows.Forms.FlowLayoutPanel panelMicrostripSubmenu;
        private System.Windows.Forms.Button btnMLIN;
        private System.Windows.Forms.Button btnMCROS;
        private System.Windows.Forms.Button btnMTEE;
    }
}


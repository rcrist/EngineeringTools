
namespace DigitalSim
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelLogo = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCircuit = new System.Windows.Forms.Button();
            this.panelCircuitSubmenu = new System.Windows.Forms.Panel();
            this.btnStore = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnComponents = new System.Windows.Forms.Button();
            this.panelComponentsSubmenu = new System.Windows.Forms.Panel();
            this.btnLED = new System.Windows.Forms.Button();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.btnNOT = new System.Windows.Forms.Button();
            this.btnOR = new System.Windows.Forms.Button();
            this.btnAND = new System.Windows.Forms.Button();
            this.btnWire = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.schematicCanvas = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelCircuitSubmenu.SuspendLayout();
            this.panelComponentsSubmenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.flowLayoutPanel1.Controls.Add(this.panelLogo);
            this.flowLayoutPanel1.Controls.Add(this.btnCircuit);
            this.flowLayoutPanel1.Controls.Add(this.panelCircuitSubmenu);
            this.flowLayoutPanel1.Controls.Add(this.btnComponents);
            this.flowLayoutPanel1.Controls.Add(this.panelComponentsSubmenu);
            this.flowLayoutPanel1.Controls.Add(this.btnWire);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.btnExit);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(220, 798);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.label1);
            this.panelLogo.Location = new System.Drawing.Point(3, 3);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(197, 107);
            this.panelLogo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 107);
            this.label1.TabIndex = 0;
            this.label1.Text = "Digital Circuit Simulator";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCircuit
            // 
            this.btnCircuit.BackColor = System.Drawing.Color.Black;
            this.btnCircuit.FlatAppearance.BorderSize = 0;
            this.btnCircuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnCircuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCircuit.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnCircuit.ForeColor = System.Drawing.Color.White;
            this.btnCircuit.Location = new System.Drawing.Point(3, 116);
            this.btnCircuit.Name = "btnCircuit";
            this.btnCircuit.Size = new System.Drawing.Size(191, 41);
            this.btnCircuit.TabIndex = 1;
            this.btnCircuit.Text = "   Circuit";
            this.btnCircuit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCircuit.UseVisualStyleBackColor = false;
            this.btnCircuit.Click += new System.EventHandler(this.btnCircuit_Click);
            // 
            // panelCircuitSubmenu
            // 
            this.panelCircuitSubmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panelCircuitSubmenu.Controls.Add(this.btnStore);
            this.panelCircuitSubmenu.Controls.Add(this.btnLoad);
            this.panelCircuitSubmenu.Controls.Add(this.btnNew);
            this.panelCircuitSubmenu.Location = new System.Drawing.Point(3, 163);
            this.panelCircuitSubmenu.Name = "panelCircuitSubmenu";
            this.panelCircuitSubmenu.Size = new System.Drawing.Size(197, 149);
            this.panelCircuitSubmenu.TabIndex = 2;
            this.panelCircuitSubmenu.Visible = false;
            // 
            // btnStore
            // 
            this.btnStore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnStore.FlatAppearance.BorderSize = 0;
            this.btnStore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStore.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnStore.ForeColor = System.Drawing.Color.White;
            this.btnStore.Location = new System.Drawing.Point(0, 95);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(191, 41);
            this.btnStore.TabIndex = 4;
            this.btnStore.Text = "Store Circuit";
            this.btnStore.UseVisualStyleBackColor = false;
            this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Location = new System.Drawing.Point(3, 48);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(191, 41);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load Circuit";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(0, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(191, 41);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "New Circuit";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnComponents
            // 
            this.btnComponents.BackColor = System.Drawing.Color.Black;
            this.btnComponents.FlatAppearance.BorderSize = 0;
            this.btnComponents.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnComponents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComponents.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnComponents.ForeColor = System.Drawing.Color.White;
            this.btnComponents.Location = new System.Drawing.Point(3, 318);
            this.btnComponents.Name = "btnComponents";
            this.btnComponents.Size = new System.Drawing.Size(191, 41);
            this.btnComponents.TabIndex = 3;
            this.btnComponents.Text = "   Components";
            this.btnComponents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnComponents.UseVisualStyleBackColor = false;
            this.btnComponents.Click += new System.EventHandler(this.btnComponents_Click);
            // 
            // panelComponentsSubmenu
            // 
            this.panelComponentsSubmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panelComponentsSubmenu.Controls.Add(this.btnLED);
            this.panelComponentsSubmenu.Controls.Add(this.btnSwitch);
            this.panelComponentsSubmenu.Controls.Add(this.btnNOT);
            this.panelComponentsSubmenu.Controls.Add(this.btnOR);
            this.panelComponentsSubmenu.Controls.Add(this.btnAND);
            this.panelComponentsSubmenu.Location = new System.Drawing.Point(3, 365);
            this.panelComponentsSubmenu.Name = "panelComponentsSubmenu";
            this.panelComponentsSubmenu.Size = new System.Drawing.Size(197, 258);
            this.panelComponentsSubmenu.TabIndex = 4;
            this.panelComponentsSubmenu.Visible = false;
            // 
            // btnLED
            // 
            this.btnLED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnLED.FlatAppearance.BorderSize = 0;
            this.btnLED.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnLED.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLED.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnLED.ForeColor = System.Drawing.Color.White;
            this.btnLED.Image = ((System.Drawing.Image)(resources.GetObject("btnLED.Image")));
            this.btnLED.Location = new System.Drawing.Point(0, 210);
            this.btnLED.Name = "btnLED";
            this.btnLED.Size = new System.Drawing.Size(191, 45);
            this.btnLED.TabIndex = 6;
            this.btnLED.Text = "   LED";
            this.btnLED.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLED.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLED.UseVisualStyleBackColor = false;
            this.btnLED.Click += new System.EventHandler(this.btnLED_Click);
            // 
            // btnSwitch
            // 
            this.btnSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnSwitch.FlatAppearance.BorderSize = 0;
            this.btnSwitch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnSwitch.ForeColor = System.Drawing.Color.White;
            this.btnSwitch.Image = ((System.Drawing.Image)(resources.GetObject("btnSwitch.Image")));
            this.btnSwitch.Location = new System.Drawing.Point(-3, 159);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(191, 45);
            this.btnSwitch.TabIndex = 5;
            this.btnSwitch.Text = "   Switch";
            this.btnSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSwitch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSwitch.UseVisualStyleBackColor = false;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btnNOT
            // 
            this.btnNOT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnNOT.FlatAppearance.BorderSize = 0;
            this.btnNOT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnNOT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNOT.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnNOT.ForeColor = System.Drawing.Color.White;
            this.btnNOT.Image = ((System.Drawing.Image)(resources.GetObject("btnNOT.Image")));
            this.btnNOT.Location = new System.Drawing.Point(0, 108);
            this.btnNOT.Name = "btnNOT";
            this.btnNOT.Size = new System.Drawing.Size(191, 45);
            this.btnNOT.TabIndex = 4;
            this.btnNOT.Text = "   NOT gate";
            this.btnNOT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNOT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNOT.UseVisualStyleBackColor = false;
            this.btnNOT.Click += new System.EventHandler(this.btnNOT_Click);
            // 
            // btnOR
            // 
            this.btnOR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnOR.FlatAppearance.BorderSize = 0;
            this.btnOR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOR.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnOR.ForeColor = System.Drawing.Color.White;
            this.btnOR.Image = ((System.Drawing.Image)(resources.GetObject("btnOR.Image")));
            this.btnOR.Location = new System.Drawing.Point(-3, 57);
            this.btnOR.Name = "btnOR";
            this.btnOR.Size = new System.Drawing.Size(191, 45);
            this.btnOR.TabIndex = 3;
            this.btnOR.Text = "  OR gate";
            this.btnOR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOR.UseVisualStyleBackColor = false;
            this.btnOR.Click += new System.EventHandler(this.btnOR_Click);
            // 
            // btnAND
            // 
            this.btnAND.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnAND.FlatAppearance.BorderSize = 0;
            this.btnAND.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnAND.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAND.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnAND.ForeColor = System.Drawing.Color.White;
            this.btnAND.Image = ((System.Drawing.Image)(resources.GetObject("btnAND.Image")));
            this.btnAND.Location = new System.Drawing.Point(0, 3);
            this.btnAND.Name = "btnAND";
            this.btnAND.Size = new System.Drawing.Size(191, 48);
            this.btnAND.TabIndex = 2;
            this.btnAND.Text = " AND gate";
            this.btnAND.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAND.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAND.UseVisualStyleBackColor = false;
            this.btnAND.Click += new System.EventHandler(this.btnAND_Click);
            // 
            // btnWire
            // 
            this.btnWire.BackColor = System.Drawing.Color.Black;
            this.btnWire.FlatAppearance.BorderSize = 0;
            this.btnWire.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnWire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWire.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnWire.ForeColor = System.Drawing.Color.White;
            this.btnWire.Location = new System.Drawing.Point(3, 629);
            this.btnWire.Name = "btnWire";
            this.btnWire.Size = new System.Drawing.Size(191, 41);
            this.btnWire.TabIndex = 5;
            this.btnWire.Text = "   Wire Mode";
            this.btnWire.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWire.UseVisualStyleBackColor = false;
            this.btnWire.Click += new System.EventHandler(this.btnWire_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(3, 676);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(191, 41);
            this.button1.TabIndex = 6;
            this.button1.Text = "   Settings";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(3, 723);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(191, 41);
            this.button2.TabIndex = 7;
            this.button2.Text = "   Help";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Black;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(3, 770);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(191, 41);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "   Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // schematicCanvas
            // 
            this.schematicCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.schematicCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicCanvas.InitialImage = null;
            this.schematicCanvas.Location = new System.Drawing.Point(220, 0);
            this.schematicCanvas.Name = "schematicCanvas";
            this.schematicCanvas.Size = new System.Drawing.Size(894, 798);
            this.schematicCanvas.TabIndex = 1;
            this.schematicCanvas.TabStop = false;
            this.schematicCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.schematicCanvas_Paint);
            this.schematicCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseDown);
            this.schematicCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseMove);
            this.schematicCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseUp);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 798);
            this.Controls.Add(this.schematicCanvas);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.form1_Resize);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelCircuitSubmenu.ResumeLayout(false);
            this.panelComponentsSubmenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox schematicCanvas;
        private System.Windows.Forms.FlowLayoutPanel panelLogo;
        private System.Windows.Forms.Button btnCircuit;
        private System.Windows.Forms.Panel panelCircuitSubmenu;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnComponents;
        private System.Windows.Forms.Panel panelComponentsSubmenu;
        private System.Windows.Forms.Button btnAND;
        private System.Windows.Forms.Button btnLED;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnNOT;
        private System.Windows.Forms.Button btnOR;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnWire;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

namespace TestDigitalSimulator
{
    partial class DigitalSimulatorMainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnWIRE = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNOT = new System.Windows.Forms.Button();
            this.btnOR = new System.Windows.Forms.Button();
            this.btnAND = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLED = new System.Windows.Forms.Button();
            this.btnSWITCH = new System.Windows.Forms.Button();
            this.schematicCanvas = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.btnWIRE);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1066, 97);
            this.panel1.TabIndex = 0;
            // 
            // btnWIRE
            // 
            this.btnWIRE.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWIRE.ForeColor = System.Drawing.Color.Black;
            this.btnWIRE.Location = new System.Drawing.Point(845, 17);
            this.btnWIRE.Name = "btnWIRE";
            this.btnWIRE.Size = new System.Drawing.Size(113, 63);
            this.btnWIRE.TabIndex = 7;
            this.btnWIRE.Text = "Wire Mode";
            this.btnWIRE.UseVisualStyleBackColor = true;
            this.btnWIRE.Click += new System.EventHandler(this.btnWIRE_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnNOT);
            this.groupBox2.Controls.Add(this.btnOR);
            this.groupBox2.Controls.Add(this.btnAND);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(374, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gates";
            // 
            // btnNOT
            // 
            this.btnNOT.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNOT.ForeColor = System.Drawing.Color.Black;
            this.btnNOT.Location = new System.Drawing.Point(310, 27);
            this.btnNOT.Name = "btnNOT";
            this.btnNOT.Size = new System.Drawing.Size(113, 42);
            this.btnNOT.TabIndex = 6;
            this.btnNOT.Text = "NOT";
            this.btnNOT.UseVisualStyleBackColor = true;
            this.btnNOT.Click += new System.EventHandler(this.btnNOT_Click);
            // 
            // btnOR
            // 
            this.btnOR.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOR.ForeColor = System.Drawing.Color.Black;
            this.btnOR.Location = new System.Drawing.Point(173, 27);
            this.btnOR.Name = "btnOR";
            this.btnOR.Size = new System.Drawing.Size(113, 42);
            this.btnOR.TabIndex = 5;
            this.btnOR.Text = "OR";
            this.btnOR.UseVisualStyleBackColor = true;
            this.btnOR.Click += new System.EventHandler(this.btnOR_Click);
            // 
            // btnAND
            // 
            this.btnAND.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAND.ForeColor = System.Drawing.Color.Black;
            this.btnAND.Location = new System.Drawing.Point(34, 27);
            this.btnAND.Name = "btnAND";
            this.btnAND.Size = new System.Drawing.Size(113, 42);
            this.btnAND.TabIndex = 4;
            this.btnAND.Text = "AND";
            this.btnAND.UseVisualStyleBackColor = true;
            this.btnAND.Click += new System.EventHandler(this.btnAND_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLED);
            this.groupBox1.Controls.Add(this.btnSWITCH);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "I/O";
            // 
            // btnLED
            // 
            this.btnLED.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLED.ForeColor = System.Drawing.Color.Black;
            this.btnLED.Location = new System.Drawing.Point(183, 30);
            this.btnLED.Name = "btnLED";
            this.btnLED.Size = new System.Drawing.Size(113, 42);
            this.btnLED.TabIndex = 3;
            this.btnLED.Text = "LED";
            this.btnLED.UseVisualStyleBackColor = true;
            this.btnLED.Click += new System.EventHandler(this.btnLED_Click);
            // 
            // btnSWITCH
            // 
            this.btnSWITCH.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSWITCH.ForeColor = System.Drawing.Color.Black;
            this.btnSWITCH.Location = new System.Drawing.Point(44, 30);
            this.btnSWITCH.Name = "btnSWITCH";
            this.btnSWITCH.Size = new System.Drawing.Size(113, 42);
            this.btnSWITCH.TabIndex = 2;
            this.btnSWITCH.Text = "SWITCH";
            this.btnSWITCH.UseVisualStyleBackColor = true;
            this.btnSWITCH.Click += new System.EventHandler(this.btnSWITCH_Click);
            // 
            // schematicCanvas
            // 
            this.schematicCanvas.BackColor = System.Drawing.Color.LightSteelBlue;
            this.schematicCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicCanvas.Location = new System.Drawing.Point(0, 97);
            this.schematicCanvas.Name = "schematicCanvas";
            this.schematicCanvas.Size = new System.Drawing.Size(1066, 518);
            this.schematicCanvas.TabIndex = 1;
            this.schematicCanvas.TabStop = false;
            this.schematicCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.schematicCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.schematicCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.schematicCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.schematicCanvas.Resize += new System.EventHandler(this.pictureBox1_Resize);
            // 
            // DigitalSimulatorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 615);
            this.Controls.Add(this.schematicCanvas);
            this.Controls.Add(this.panel1);
            this.Name = "DigitalSimulatorMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Digital Simulator";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox schematicCanvas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnNOT;
        private System.Windows.Forms.Button btnOR;
        private System.Windows.Forms.Button btnAND;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLED;
        private System.Windows.Forms.Button btnSWITCH;
        private System.Windows.Forms.Button btnWIRE;
    }
}


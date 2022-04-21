
namespace TestWire
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
            this.schematicCanvas = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonSpline = new System.Windows.Forms.RadioButton();
            this.radioButtonRectilinear = new System.Windows.Forms.RadioButton();
            this.radioButtonStraight = new System.Windows.Forms.RadioButton();
            this.btnDisplay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // schematicCanvas
            // 
            this.schematicCanvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.schematicCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicCanvas.Location = new System.Drawing.Point(0, 0);
            this.schematicCanvas.Name = "schematicCanvas";
            this.schematicCanvas.Size = new System.Drawing.Size(1034, 596);
            this.schematicCanvas.TabIndex = 0;
            this.schematicCanvas.TabStop = false;
            this.schematicCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.schematicCanvas_Paint);
            this.schematicCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseDown);
            this.schematicCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseMove);
            this.schematicCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseUp);
            this.schematicCanvas.Resize += new System.EventHandler(this.schematicCanvas_Resize);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDisplay);
            this.groupBox1.Controls.Add(this.radioButtonSpline);
            this.groupBox1.Controls.Add(this.radioButtonRectilinear);
            this.groupBox1.Controls.Add(this.radioButtonStraight);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1034, 56);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Line Shape";
            // 
            // radioButtonSpline
            // 
            this.radioButtonSpline.AutoSize = true;
            this.radioButtonSpline.Location = new System.Drawing.Point(254, 29);
            this.radioButtonSpline.Name = "radioButtonSpline";
            this.radioButtonSpline.Size = new System.Drawing.Size(68, 21);
            this.radioButtonSpline.TabIndex = 2;
            this.radioButtonSpline.Text = "Spline";
            this.radioButtonSpline.UseVisualStyleBackColor = true;
            this.radioButtonSpline.CheckedChanged += new System.EventHandler(this.radioButtonSpline_CheckedChanged);
            // 
            // radioButtonRectilinear
            // 
            this.radioButtonRectilinear.AutoSize = true;
            this.radioButtonRectilinear.Location = new System.Drawing.Point(129, 29);
            this.radioButtonRectilinear.Name = "radioButtonRectilinear";
            this.radioButtonRectilinear.Size = new System.Drawing.Size(96, 21);
            this.radioButtonRectilinear.TabIndex = 1;
            this.radioButtonRectilinear.Text = "Rectilinear";
            this.radioButtonRectilinear.UseVisualStyleBackColor = true;
            this.radioButtonRectilinear.CheckedChanged += new System.EventHandler(this.radioButtonRectilinear_CheckedChanged);
            // 
            // radioButtonStraight
            // 
            this.radioButtonStraight.AutoSize = true;
            this.radioButtonStraight.Checked = true;
            this.radioButtonStraight.Location = new System.Drawing.Point(25, 29);
            this.radioButtonStraight.Name = "radioButtonStraight";
            this.radioButtonStraight.Size = new System.Drawing.Size(78, 21);
            this.radioButtonStraight.TabIndex = 0;
            this.radioButtonStraight.TabStop = true;
            this.radioButtonStraight.Text = "Straight";
            this.radioButtonStraight.UseVisualStyleBackColor = true;
            this.radioButtonStraight.CheckedChanged += new System.EventHandler(this.radioButtonStraignt_CheckedChanged);
            // 
            // btnDisplay
            // 
            this.btnDisplay.Location = new System.Drawing.Point(897, 21);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(114, 28);
            this.btnDisplay.TabIndex = 3;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 596);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.schematicCanvas);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Wire Component";
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox schematicCanvas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonRectilinear;
        private System.Windows.Forms.RadioButton radioButtonStraight;
        private System.Windows.Forms.RadioButton radioButtonSpline;
        private System.Windows.Forms.Button btnDisplay;
    }
}


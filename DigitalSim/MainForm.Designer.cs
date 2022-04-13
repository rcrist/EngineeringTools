
namespace DigitalSim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ANDBtn = new System.Windows.Forms.ToolStripButton();
            this.ORBtn = new System.Windows.Forms.ToolStripButton();
            this.NOTBtn = new System.Windows.Forms.ToolStripButton();
            this.SwitchBtn = new System.Windows.Forms.ToolStripButton();
            this.LEDBtn = new System.Windows.Forms.ToolStripButton();
            this.WireBtn = new System.Windows.Forms.ToolStripButton();
            this.formBtn = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.schematicCanvas = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ANDBtn,
            this.ORBtn,
            this.NOTBtn,
            this.SwitchBtn,
            this.LEDBtn,
            this.WireBtn,
            this.formBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1115, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ANDBtn
            // 
            this.ANDBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ANDBtn.Image = ((System.Drawing.Image)(resources.GetObject("ANDBtn.Image")));
            this.ANDBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ANDBtn.Name = "ANDBtn";
            this.ANDBtn.Size = new System.Drawing.Size(45, 24);
            this.ANDBtn.Text = "AND";
            this.ANDBtn.Click += new System.EventHandler(this.ANDBtn_Click);
            // 
            // ORBtn
            // 
            this.ORBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ORBtn.Image = ((System.Drawing.Image)(resources.GetObject("ORBtn.Image")));
            this.ORBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ORBtn.Name = "ORBtn";
            this.ORBtn.Size = new System.Drawing.Size(33, 24);
            this.ORBtn.Text = "OR";
            this.ORBtn.Click += new System.EventHandler(this.ORBtn_Click);
            // 
            // NOTBtn
            // 
            this.NOTBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NOTBtn.Image = ((System.Drawing.Image)(resources.GetObject("NOTBtn.Image")));
            this.NOTBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NOTBtn.Name = "NOTBtn";
            this.NOTBtn.Size = new System.Drawing.Size(42, 24);
            this.NOTBtn.Text = "NOT";
            this.NOTBtn.Click += new System.EventHandler(this.NOTBtn_Click);
            // 
            // SwitchBtn
            // 
            this.SwitchBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SwitchBtn.Image = ((System.Drawing.Image)(resources.GetObject("SwitchBtn.Image")));
            this.SwitchBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SwitchBtn.Name = "SwitchBtn";
            this.SwitchBtn.Size = new System.Drawing.Size(56, 24);
            this.SwitchBtn.Text = "Switch";
            this.SwitchBtn.Click += new System.EventHandler(this.SwitchBtn_Click);
            // 
            // LEDBtn
            // 
            this.LEDBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LEDBtn.Image = ((System.Drawing.Image)(resources.GetObject("LEDBtn.Image")));
            this.LEDBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LEDBtn.Name = "LEDBtn";
            this.LEDBtn.Size = new System.Drawing.Size(39, 24);
            this.LEDBtn.Text = "LED";
            this.LEDBtn.Click += new System.EventHandler(this.LEDBtn_Click);
            // 
            // WireBtn
            // 
            this.WireBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.WireBtn.Image = ((System.Drawing.Image)(resources.GetObject("WireBtn.Image")));
            this.WireBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WireBtn.Name = "WireBtn";
            this.WireBtn.Size = new System.Drawing.Size(44, 24);
            this.WireBtn.Text = "Wire";
            this.WireBtn.Click += new System.EventHandler(this.WireBtn_Click);
            // 
            // formBtn
            // 
            this.formBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.formBtn.Image = ((System.Drawing.Image)(resources.GetObject("formBtn.Image")));
            this.formBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.formBtn.Name = "formBtn";
            this.formBtn.Size = new System.Drawing.Size(83, 24);
            this.formBtn.Text = "Modern UI";
            this.formBtn.Click += new System.EventHandler(this.ModernUI_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // schematicCanvas
            // 
            this.schematicCanvas.BackColor = System.Drawing.Color.LightBlue;
            this.schematicCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicCanvas.Location = new System.Drawing.Point(0, 55);
            this.schematicCanvas.Name = "schematicCanvas";
            this.schematicCanvas.Size = new System.Drawing.Size(1115, 526);
            this.schematicCanvas.TabIndex = 3;
            this.schematicCanvas.TabStop = false;
            this.schematicCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.schematicCanvas_Paint);
            this.schematicCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseDown);
            this.schematicCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseMove);
            this.schematicCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseUp);
            this.schematicCanvas.Resize += new System.EventHandler(this.mainForm_Resize);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1115, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 581);
            this.Controls.Add(this.schematicCanvas);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Digital Simulator";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox schematicCanvas;
        private System.Windows.Forms.ToolStripButton ANDBtn;
        private System.Windows.Forms.ToolStripButton ORBtn;
        private System.Windows.Forms.ToolStripButton NOTBtn;
        private System.Windows.Forms.ToolStripButton SwitchBtn;
        private System.Windows.Forms.ToolStripButton LEDBtn;
        private System.Windows.Forms.ToolStripButton WireBtn;
        private System.Windows.Forms.ToolStripButton formBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}


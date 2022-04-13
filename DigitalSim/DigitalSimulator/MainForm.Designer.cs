
namespace DigitalSimulator
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ANDBtn = new System.Windows.Forms.ToolStripButton();
            this.ORBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.SwitchBtn = new System.Windows.Forms.ToolStripButton();
            this.LEDBtn = new System.Windows.Forms.ToolStripButton();
            this.RectBtn = new System.Windows.Forms.ToolStripButton();
            this.WireBtn = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.schematicCanvas = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1115, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.ANDBtn,
            this.ORBtn,
            this.toolStripButton3,
            this.SwitchBtn,
            this.LEDBtn,
            this.RectBtn,
            this.WireBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1115, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 27);
            this.toolStripButton1.Text = "AND";
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // ANDBtn
            // 
            this.ANDBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ANDBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ANDBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ANDBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ANDBtn.Name = "ANDBtn";
            this.ANDBtn.Size = new System.Drawing.Size(50, 27);
            this.ANDBtn.Text = "AND";
            this.ANDBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ANDBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // ORBtn
            // 
            this.ORBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ORBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ORBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ORBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ORBtn.Name = "ORBtn";
            this.ORBtn.Size = new System.Drawing.Size(37, 27);
            this.ORBtn.Text = "OR";
            this.ORBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ORBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(48, 27);
            this.toolStripButton3.Text = "NOT";
            this.toolStripButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // SwitchBtn
            // 
            this.SwitchBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SwitchBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SwitchBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SwitchBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SwitchBtn.Name = "SwitchBtn";
            this.SwitchBtn.Size = new System.Drawing.Size(63, 27);
            this.SwitchBtn.Text = "Switch";
            this.SwitchBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SwitchBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.SwitchBtn.Click += new System.EventHandler(this.SwitchBtn_Click);
            // 
            // LEDBtn
            // 
            this.LEDBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LEDBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LEDBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LEDBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LEDBtn.Name = "LEDBtn";
            this.LEDBtn.Size = new System.Drawing.Size(43, 27);
            this.LEDBtn.Text = "LED";
            this.LEDBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LEDBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.LEDBtn.Click += new System.EventHandler(this.LEDBtn_Click);
            // 
            // RectBtn
            // 
            this.RectBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RectBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RectBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RectBtn.Name = "RectBtn";
            this.RectBtn.Size = new System.Drawing.Size(47, 27);
            this.RectBtn.Text = "Rect";
            this.RectBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RectBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // WireBtn
            // 
            this.WireBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.WireBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WireBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.WireBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WireBtn.Name = "WireBtn";
            this.WireBtn.Size = new System.Drawing.Size(49, 27);
            this.WireBtn.Text = "Wire";
            this.WireBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WireBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.WireBtn.Click += new System.EventHandler(this.WireBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 589);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1115, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(58, 20);
            this.StatusLabel.Text = "Status...";
            // 
            // schematicCanvas
            // 
            this.schematicCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicCanvas.Location = new System.Drawing.Point(0, 58);
            this.schematicCanvas.Name = "schematicCanvas";
            this.schematicCanvas.Size = new System.Drawing.Size(1115, 531);
            this.schematicCanvas.TabIndex = 3;
            this.schematicCanvas.TabStop = false;
            this.schematicCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.schematicCanvas_Paint);
            this.schematicCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseDown);
            this.schematicCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseMove);
            this.schematicCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.schematicCanvas_MouseUp);
            this.schematicCanvas.Resize += new System.EventHandler(this.mainForm_Resize);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 615);
            this.Controls.Add(this.schematicCanvas);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Digital Simulator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton ANDBtn;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton ORBtn;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton SwitchBtn;
        private System.Windows.Forms.ToolStripButton LEDBtn;
        private System.Windows.Forms.ToolStripButton RectBtn;
        private System.Windows.Forms.ToolStripButton WireBtn;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.PictureBox schematicCanvas;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}


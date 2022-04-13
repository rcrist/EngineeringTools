
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.panelCompMenu = new System.Windows.Forms.Panel();
            this.schematicCanvas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 753);
            this.panel1.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.CategoryForeColor = System.Drawing.Color.White;
            this.propertyGrid.CategorySplitterColor = System.Drawing.Color.Black;
            this.propertyGrid.CommandsBorderColor = System.Drawing.Color.Black;
            this.propertyGrid.CommandsDisabledLinkColor = System.Drawing.Color.Black;
            this.propertyGrid.CommandsForeColor = System.Drawing.Color.White;
            this.propertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid.HelpBackColor = System.Drawing.Color.Black;
            this.propertyGrid.HelpBorderColor = System.Drawing.Color.DimGray;
            this.propertyGrid.HelpForeColor = System.Drawing.Color.White;
            this.propertyGrid.LineColor = System.Drawing.Color.Black;
            this.propertyGrid.Location = new System.Drawing.Point(1052, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.SelectedItemWithFocusForeColor = System.Drawing.Color.White;
            this.propertyGrid.Size = new System.Drawing.Size(130, 753);
            this.propertyGrid.TabIndex = 1;
            this.propertyGrid.ViewBackColor = System.Drawing.Color.Black;
            this.propertyGrid.ViewBorderColor = System.Drawing.Color.DimGray;
            this.propertyGrid.ViewForeColor = System.Drawing.Color.White;
            // 
            // panelCompMenu
            // 
            this.panelCompMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCompMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCompMenu.Location = new System.Drawing.Point(200, 0);
            this.panelCompMenu.Name = "panelCompMenu";
            this.panelCompMenu.Size = new System.Drawing.Size(852, 100);
            this.panelCompMenu.TabIndex = 2;
            // 
            // schematicCanvas
            // 
            this.schematicCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicCanvas.Location = new System.Drawing.Point(200, 100);
            this.schematicCanvas.Name = "schematicCanvas";
            this.schematicCanvas.Size = new System.Drawing.Size(852, 653);
            this.schematicCanvas.TabIndex = 3;
            this.schematicCanvas.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.schematicCanvas);
            this.Controls.Add(this.panelCompMenu);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Microwave Tools";
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Panel panelCompMenu;
        private System.Windows.Forms.PictureBox schematicCanvas;
    }
}


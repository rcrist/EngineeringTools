﻿
namespace TestDiagram
{
    partial class TestDigramMainForm
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
            this.panelMainMenu = new System.Windows.Forms.Panel();
            this.schematicCanvas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMainMenu
            // 
            this.panelMainMenu.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelMainMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMainMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMainMenu.Name = "panelMainMenu";
            this.panelMainMenu.Size = new System.Drawing.Size(800, 52);
            this.panelMainMenu.TabIndex = 0;
            // 
            // schematicCanvas
            // 
            this.schematicCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicCanvas.Location = new System.Drawing.Point(0, 52);
            this.schematicCanvas.Name = "schematicCanvas";
            this.schematicCanvas.Size = new System.Drawing.Size(800, 398);
            this.schematicCanvas.TabIndex = 1;
            this.schematicCanvas.TabStop = false;
            // 
            // TestDigramMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.schematicCanvas);
            this.Controls.Add(this.panelMainMenu);
            this.Name = "TestDigramMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Diagram";
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainMenu;
        private System.Windows.Forms.PictureBox schematicCanvas;
    }
}



namespace EngineeringTools
{
    partial class EngineeringToolsMainForm
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
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.schematicCanvas = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCircuit = new System.Windows.Forms.Button();
            this.panelCircuitSubmenu = new System.Windows.Forms.Panel();
            this.btnNewCircuit = new System.Windows.Forms.Button();
            this.btnLoadCircuit = new System.Windows.Forms.Button();
            this.btnStoreCircuit = new System.Windows.Forms.Button();
            this.btnAnalyzeCircuit = new System.Windows.Forms.Button();
            this.btnWireMode = new System.Windows.Forms.Button();
            this.panelSideMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelCompMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDigital = new System.Windows.Forms.Button();
            this.panelDigitalSubmenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAND = new System.Windows.Forms.Button();
            this.btnOR = new System.Windows.Forms.Button();
            this.btnNOT = new System.Windows.Forms.Button();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.btnLED = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelCircuitSubmenu.SuspendLayout();
            this.panelSideMenu.SuspendLayout();
            this.panelCompMenu.SuspendLayout();
            this.panelDigitalSubmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid
            // 
            this.propertyGrid.BackColor = System.Drawing.Color.Black;
            this.propertyGrid.CategoryForeColor = System.Drawing.Color.White;
            this.propertyGrid.CategorySplitterColor = System.Drawing.Color.DimGray;
            this.propertyGrid.CommandsBorderColor = System.Drawing.Color.DimGray;
            this.propertyGrid.CommandsDisabledLinkColor = System.Drawing.Color.Black;
            this.propertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid.HelpBackColor = System.Drawing.Color.Black;
            this.propertyGrid.HelpBorderColor = System.Drawing.Color.DimGray;
            this.propertyGrid.HelpForeColor = System.Drawing.Color.White;
            this.propertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.propertyGrid.Location = new System.Drawing.Point(1382, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(200, 753);
            this.propertyGrid.TabIndex = 1;
            this.propertyGrid.ViewBackColor = System.Drawing.Color.Black;
            this.propertyGrid.ViewBorderColor = System.Drawing.Color.DimGray;
            this.propertyGrid.ViewForeColor = System.Drawing.Color.WhiteSmoke;
            // 
            // schematicCanvas
            // 
            this.schematicCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicCanvas.Location = new System.Drawing.Point(0, 0);
            this.schematicCanvas.Name = "schematicCanvas";
            this.schematicCanvas.Size = new System.Drawing.Size(1382, 753);
            this.schematicCanvas.TabIndex = 0;
            this.schematicCanvas.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 100);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCircuit
            // 
            this.btnCircuit.FlatAppearance.BorderSize = 0;
            this.btnCircuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnCircuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCircuit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCircuit.Location = new System.Drawing.Point(3, 109);
            this.btnCircuit.Name = "btnCircuit";
            this.btnCircuit.Size = new System.Drawing.Size(200, 40);
            this.btnCircuit.TabIndex = 1;
            this.btnCircuit.Text = "Circuit";
            this.btnCircuit.UseVisualStyleBackColor = true;
            this.btnCircuit.Click += new System.EventHandler(this.btnCircuit_Click);
            // 
            // panelCircuitSubmenu
            // 
            this.panelCircuitSubmenu.Controls.Add(this.btnAnalyzeCircuit);
            this.panelCircuitSubmenu.Controls.Add(this.btnStoreCircuit);
            this.panelCircuitSubmenu.Controls.Add(this.btnLoadCircuit);
            this.panelCircuitSubmenu.Controls.Add(this.btnNewCircuit);
            this.panelCircuitSubmenu.Location = new System.Drawing.Point(3, 155);
            this.panelCircuitSubmenu.Name = "panelCircuitSubmenu";
            this.panelCircuitSubmenu.Size = new System.Drawing.Size(200, 165);
            this.panelCircuitSubmenu.TabIndex = 2;
            this.panelCircuitSubmenu.Visible = false;
            // 
            // btnNewCircuit
            // 
            this.btnNewCircuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnNewCircuit.FlatAppearance.BorderSize = 0;
            this.btnNewCircuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnNewCircuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewCircuit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewCircuit.Location = new System.Drawing.Point(-6, 1);
            this.btnNewCircuit.Name = "btnNewCircuit";
            this.btnNewCircuit.Size = new System.Drawing.Size(200, 40);
            this.btnNewCircuit.TabIndex = 2;
            this.btnNewCircuit.Text = "New Circuit";
            this.btnNewCircuit.UseVisualStyleBackColor = false;
            // 
            // btnLoadCircuit
            // 
            this.btnLoadCircuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnLoadCircuit.FlatAppearance.BorderSize = 0;
            this.btnLoadCircuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnLoadCircuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadCircuit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadCircuit.Location = new System.Drawing.Point(-6, 41);
            this.btnLoadCircuit.Name = "btnLoadCircuit";
            this.btnLoadCircuit.Size = new System.Drawing.Size(200, 40);
            this.btnLoadCircuit.TabIndex = 3;
            this.btnLoadCircuit.Text = "Load Circuit";
            this.btnLoadCircuit.UseVisualStyleBackColor = false;
            // 
            // btnStoreCircuit
            // 
            this.btnStoreCircuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnStoreCircuit.FlatAppearance.BorderSize = 0;
            this.btnStoreCircuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnStoreCircuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStoreCircuit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStoreCircuit.Location = new System.Drawing.Point(-6, 79);
            this.btnStoreCircuit.Name = "btnStoreCircuit";
            this.btnStoreCircuit.Size = new System.Drawing.Size(200, 40);
            this.btnStoreCircuit.TabIndex = 4;
            this.btnStoreCircuit.Text = "Store Circuit";
            this.btnStoreCircuit.UseVisualStyleBackColor = false;
            // 
            // btnAnalyzeCircuit
            // 
            this.btnAnalyzeCircuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnAnalyzeCircuit.FlatAppearance.BorderSize = 0;
            this.btnAnalyzeCircuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnAnalyzeCircuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalyzeCircuit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalyzeCircuit.Location = new System.Drawing.Point(-6, 119);
            this.btnAnalyzeCircuit.Name = "btnAnalyzeCircuit";
            this.btnAnalyzeCircuit.Size = new System.Drawing.Size(200, 40);
            this.btnAnalyzeCircuit.TabIndex = 5;
            this.btnAnalyzeCircuit.Text = "Analyze Circuit";
            this.btnAnalyzeCircuit.UseVisualStyleBackColor = false;
            // 
            // btnWireMode
            // 
            this.btnWireMode.FlatAppearance.BorderSize = 0;
            this.btnWireMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnWireMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWireMode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWireMode.Location = new System.Drawing.Point(3, 326);
            this.btnWireMode.Name = "btnWireMode";
            this.btnWireMode.Size = new System.Drawing.Size(200, 40);
            this.btnWireMode.TabIndex = 3;
            this.btnWireMode.Text = "Wire Mode";
            this.btnWireMode.UseVisualStyleBackColor = true;
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.Controls.Add(this.pictureBox1);
            this.panelSideMenu.Controls.Add(this.btnCircuit);
            this.panelSideMenu.Controls.Add(this.panelCircuitSubmenu);
            this.panelSideMenu.Controls.Add(this.btnWireMode);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(200, 753);
            this.panelSideMenu.TabIndex = 3;
            // 
            // panelCompMenu
            // 
            this.panelCompMenu.Controls.Add(this.btnDigital);
            this.panelCompMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCompMenu.Location = new System.Drawing.Point(200, 0);
            this.panelCompMenu.Name = "panelCompMenu";
            this.panelCompMenu.Size = new System.Drawing.Size(1182, 50);
            this.panelCompMenu.TabIndex = 4;
            // 
            // btnDigital
            // 
            this.btnDigital.FlatAppearance.BorderSize = 0;
            this.btnDigital.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnDigital.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDigital.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDigital.Location = new System.Drawing.Point(3, 3);
            this.btnDigital.Name = "btnDigital";
            this.btnDigital.Size = new System.Drawing.Size(200, 40);
            this.btnDigital.TabIndex = 2;
            this.btnDigital.Text = "Digital";
            this.btnDigital.UseVisualStyleBackColor = true;
            this.btnDigital.Click += new System.EventHandler(this.btnDigital_Click);
            // 
            // panelDigitalSubmenu
            // 
            this.panelDigitalSubmenu.Controls.Add(this.btnAND);
            this.panelDigitalSubmenu.Controls.Add(this.btnOR);
            this.panelDigitalSubmenu.Controls.Add(this.btnNOT);
            this.panelDigitalSubmenu.Controls.Add(this.btnSwitch);
            this.panelDigitalSubmenu.Controls.Add(this.btnLED);
            this.panelDigitalSubmenu.Location = new System.Drawing.Point(275, 49);
            this.panelDigitalSubmenu.Name = "panelDigitalSubmenu";
            this.panelDigitalSubmenu.Size = new System.Drawing.Size(200, 225);
            this.panelDigitalSubmenu.TabIndex = 5;
            this.panelDigitalSubmenu.Visible = false;
            // 
            // btnAND
            // 
            this.btnAND.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnAND.FlatAppearance.BorderSize = 0;
            this.btnAND.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnAND.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAND.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAND.Location = new System.Drawing.Point(3, 3);
            this.btnAND.Name = "btnAND";
            this.btnAND.Size = new System.Drawing.Size(200, 40);
            this.btnAND.TabIndex = 9;
            this.btnAND.Text = "AND";
            this.btnAND.UseVisualStyleBackColor = false;
            // 
            // btnOR
            // 
            this.btnOR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnOR.FlatAppearance.BorderSize = 0;
            this.btnOR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOR.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOR.Location = new System.Drawing.Point(3, 49);
            this.btnOR.Name = "btnOR";
            this.btnOR.Size = new System.Drawing.Size(200, 40);
            this.btnOR.TabIndex = 8;
            this.btnOR.Text = "OR";
            this.btnOR.UseVisualStyleBackColor = false;
            // 
            // btnNOT
            // 
            this.btnNOT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnNOT.FlatAppearance.BorderSize = 0;
            this.btnNOT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnNOT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNOT.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNOT.Location = new System.Drawing.Point(3, 95);
            this.btnNOT.Name = "btnNOT";
            this.btnNOT.Size = new System.Drawing.Size(200, 40);
            this.btnNOT.TabIndex = 7;
            this.btnNOT.Text = "NOT";
            this.btnNOT.UseVisualStyleBackColor = false;
            this.btnNOT.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnSwitch
            // 
            this.btnSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnSwitch.FlatAppearance.BorderSize = 0;
            this.btnSwitch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitch.Location = new System.Drawing.Point(3, 141);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(200, 40);
            this.btnSwitch.TabIndex = 6;
            this.btnSwitch.Text = "Switch";
            this.btnSwitch.UseVisualStyleBackColor = false;
            // 
            // btnLED
            // 
            this.btnLED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnLED.FlatAppearance.BorderSize = 0;
            this.btnLED.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btnLED.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLED.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLED.Location = new System.Drawing.Point(3, 187);
            this.btnLED.Name = "btnLED";
            this.btnLED.Size = new System.Drawing.Size(200, 40);
            this.btnLED.TabIndex = 10;
            this.btnLED.Text = "LED";
            this.btnLED.UseVisualStyleBackColor = false;
            // 
            // EngineeringToolsMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1582, 753);
            this.Controls.Add(this.panelDigitalSubmenu);
            this.Controls.Add(this.panelCompMenu);
            this.Controls.Add(this.panelSideMenu);
            this.Controls.Add(this.schematicCanvas);
            this.Controls.Add(this.propertyGrid);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "EngineeringToolsMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Engineering Tools";
            ((System.ComponentModel.ISupportInitialize)(this.schematicCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelCircuitSubmenu.ResumeLayout(false);
            this.panelSideMenu.ResumeLayout(false);
            this.panelCompMenu.ResumeLayout(false);
            this.panelDigitalSubmenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button btnCircuit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox schematicCanvas;
        private System.Windows.Forms.Panel panelCircuitSubmenu;
        private System.Windows.Forms.Button btnAnalyzeCircuit;
        private System.Windows.Forms.Button btnStoreCircuit;
        private System.Windows.Forms.Button btnLoadCircuit;
        private System.Windows.Forms.Button btnNewCircuit;
        private System.Windows.Forms.Button btnWireMode;
        private System.Windows.Forms.FlowLayoutPanel panelSideMenu;
        private System.Windows.Forms.FlowLayoutPanel panelCompMenu;
        private System.Windows.Forms.Button btnDigital;
        private System.Windows.Forms.FlowLayoutPanel panelDigitalSubmenu;
        private System.Windows.Forms.Button btnAND;
        private System.Windows.Forms.Button btnOR;
        private System.Windows.Forms.Button btnNOT;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnLED;
    }
}


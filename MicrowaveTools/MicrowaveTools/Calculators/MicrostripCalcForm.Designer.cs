
namespace MicrowaveTools.Calculators
{
    partial class MicrostripCalcForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MicrostripCalcForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbRough = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbRHO = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTand = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbH = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbEr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbL = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbW = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbFreq = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbDielLoss = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbCondLoss = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbErEff = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbSkinDepth = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbAngle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbZ0 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 243);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbRough);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbRHO);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbTand);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbT);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbH);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbEr);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 261);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 336);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Substrate Properties";
            // 
            // tbRough
            // 
            this.tbRough.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRough.Location = new System.Drawing.Point(126, 237);
            this.tbRough.Name = "tbRough";
            this.tbRough.Size = new System.Drawing.Size(231, 30);
            this.tbRough.TabIndex = 11;
            this.tbRough.Text = "0.15e-6";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(50, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Rough";
            // 
            // tbRHO
            // 
            this.tbRHO.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRHO.Location = new System.Drawing.Point(126, 201);
            this.tbRHO.Name = "tbRHO";
            this.tbRHO.Size = new System.Drawing.Size(231, 30);
            this.tbRHO.TabIndex = 9;
            this.tbRHO.Text = "0.022e-6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(74, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "rho";
            // 
            // tbTand
            // 
            this.tbTand.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTand.Location = new System.Drawing.Point(126, 165);
            this.tbTand.Name = "tbTand";
            this.tbTand.Size = new System.Drawing.Size(231, 30);
            this.tbTand.TabIndex = 7;
            this.tbTand.Text = "1e-3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tand";
            // 
            // tbT
            // 
            this.tbT.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbT.Location = new System.Drawing.Point(126, 129);
            this.tbT.Name = "tbT";
            this.tbT.Size = new System.Drawing.Size(231, 30);
            this.tbT.TabIndex = 5;
            this.tbT.Text = "35";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(91, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "T";
            // 
            // tbH
            // 
            this.tbH.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbH.Location = new System.Drawing.Point(126, 93);
            this.tbH.Name = "tbH";
            this.tbH.Size = new System.Drawing.Size(231, 30);
            this.tbH.TabIndex = 3;
            this.tbH.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(88, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "H";
            // 
            // tbEr
            // 
            this.tbEr.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEr.Location = new System.Drawing.Point(126, 57);
            this.tbEr.Name = "tbEr";
            this.tbEr.Size = new System.Drawing.Size(231, 30);
            this.tbEr.TabIndex = 1;
            this.tbEr.Text = "9.8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Er";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbL);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbW);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tbFreq);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(495, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 253);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Design Parameters";
            // 
            // tbL
            // 
            this.tbL.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbL.Location = new System.Drawing.Point(173, 116);
            this.tbL.Name = "tbL";
            this.tbL.Size = new System.Drawing.Size(231, 30);
            this.tbL.TabIndex = 7;
            this.tbL.Text = "1000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(146, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 23);
            this.label9.TabIndex = 6;
            this.label9.Text = "L";
            // 
            // tbW
            // 
            this.tbW.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbW.Location = new System.Drawing.Point(173, 80);
            this.tbW.Name = "tbW";
            this.tbW.Size = new System.Drawing.Size(231, 30);
            this.tbW.TabIndex = 5;
            this.tbW.Text = "37.2983";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(138, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 23);
            this.label8.TabIndex = 4;
            this.label8.Text = "W";
            // 
            // tbFreq
            // 
            this.tbFreq.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFreq.Location = new System.Drawing.Point(173, 44);
            this.tbFreq.Name = "tbFreq";
            this.tbFreq.Size = new System.Drawing.Size(231, 30);
            this.tbFreq.TabIndex = 3;
            this.tbFreq.Text = "2.0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(76, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 23);
            this.label7.TabIndex = 2;
            this.label7.Text = "Frequency";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbDielLoss);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.tbCondLoss);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.tbErEff);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.tbSkinDepth);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.tbAngle);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.tbZ0);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(495, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(634, 337);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Microstrip RF Properties";
            // 
            // tbDielLoss
            // 
            this.tbDielLoss.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDielLoss.Location = new System.Drawing.Point(173, 238);
            this.tbDielLoss.Name = "tbDielLoss";
            this.tbDielLoss.Size = new System.Drawing.Size(231, 30);
            this.tbDielLoss.TabIndex = 19;
            this.tbDielLoss.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(84, 241);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 23);
            this.label15.TabIndex = 18;
            this.label15.Text = "Diel Loss";
            // 
            // tbCondLoss
            // 
            this.tbCondLoss.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCondLoss.Location = new System.Drawing.Point(173, 202);
            this.tbCondLoss.Name = "tbCondLoss";
            this.tbCondLoss.Size = new System.Drawing.Size(231, 30);
            this.tbCondLoss.TabIndex = 17;
            this.tbCondLoss.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(72, 205);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 23);
            this.label14.TabIndex = 16;
            this.label14.Text = "Cond Loss";
            // 
            // tbErEff
            // 
            this.tbErEff.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbErEff.Location = new System.Drawing.Point(173, 166);
            this.tbErEff.Name = "tbErEff";
            this.tbErEff.Size = new System.Drawing.Size(231, 30);
            this.tbErEff.TabIndex = 15;
            this.tbErEff.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(116, 169);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 23);
            this.label13.TabIndex = 14;
            this.label13.Text = "ErEff";
            // 
            // tbSkinDepth
            // 
            this.tbSkinDepth.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSkinDepth.Location = new System.Drawing.Point(173, 130);
            this.tbSkinDepth.Name = "tbSkinDepth";
            this.tbSkinDepth.Size = new System.Drawing.Size(231, 30);
            this.tbSkinDepth.TabIndex = 13;
            this.tbSkinDepth.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(67, 133);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 23);
            this.label12.TabIndex = 12;
            this.label12.Text = "Skin Depth";
            // 
            // tbAngle
            // 
            this.tbAngle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAngle.Location = new System.Drawing.Point(173, 94);
            this.tbAngle.Name = "tbAngle";
            this.tbAngle.Size = new System.Drawing.Size(231, 30);
            this.tbAngle.TabIndex = 11;
            this.tbAngle.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(106, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 23);
            this.label11.TabIndex = 10;
            this.label11.Text = "Angle";
            // 
            // tbZ0
            // 
            this.tbZ0.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbZ0.Location = new System.Drawing.Point(173, 58);
            this.tbZ0.Name = "tbZ0";
            this.tbZ0.Size = new System.Drawing.Size(231, 30);
            this.tbZ0.TabIndex = 9;
            this.tbZ0.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(131, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 23);
            this.label10.TabIndex = 8;
            this.label10.Text = "Z0";
            // 
            // MicrostripCalcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1141, 609);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MicrostripCalcForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Microstrip Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbRough;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbRHO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTand;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbEr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbL;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbW;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbFreq;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbErEff;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbSkinDepth;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbAngle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbZ0;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbDielLoss;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbCondLoss;
        private System.Windows.Forms.Label label14;
    }
}
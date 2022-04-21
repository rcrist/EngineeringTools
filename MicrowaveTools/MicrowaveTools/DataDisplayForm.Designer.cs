
namespace MicrowaveTools
{
    partial class DataDisplayForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.rtbDataDisplay = new System.Windows.Forms.RichTextBox();
            this.btnData = new System.Windows.Forms.Button();
            this.chartDataDisplay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartDataDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbDataDisplay
            // 
            this.rtbDataDisplay.BackColor = System.Drawing.Color.Black;
            this.rtbDataDisplay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbDataDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDataDisplay.ForeColor = System.Drawing.Color.White;
            this.rtbDataDisplay.Location = new System.Drawing.Point(0, 601);
            this.rtbDataDisplay.Name = "rtbDataDisplay";
            this.rtbDataDisplay.Size = new System.Drawing.Size(1143, 150);
            this.rtbDataDisplay.TabIndex = 6;
            this.rtbDataDisplay.Text = "";
            // 
            // btnData
            // 
            this.btnData.BackColor = System.Drawing.Color.Black;
            this.btnData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData.ForeColor = System.Drawing.Color.White;
            this.btnData.Location = new System.Drawing.Point(0, 565);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(1143, 36);
            this.btnData.TabIndex = 8;
            this.btnData.Text = "Data";
            this.btnData.UseVisualStyleBackColor = false;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // chartDataDisplay
            // 
            this.chartDataDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.AxisX.Title = "Freq (GHz)";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.Title = "S[dB]";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.Name = "ChartArea1";
            this.chartDataDisplay.ChartAreas.Add(chartArea1);
            this.chartDataDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartDataDisplay.Legends.Add(legend1);
            this.chartDataDisplay.Location = new System.Drawing.Point(0, 0);
            this.chartDataDisplay.Name = "chartDataDisplay";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "S11";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "S21";
            this.chartDataDisplay.Series.Add(series1);
            this.chartDataDisplay.Series.Add(series2);
            this.chartDataDisplay.Size = new System.Drawing.Size(1143, 565);
            this.chartDataDisplay.TabIndex = 9;
            this.chartDataDisplay.Text = "chart1";
            // 
            // DataDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 751);
            this.Controls.Add(this.chartDataDisplay);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.rtbDataDisplay);
            this.Name = "DataDisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S-Parameter Results Data";
            ((System.ComponentModel.ISupportInitialize)(this.chartDataDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbDataDisplay;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDataDisplay;
    }
}
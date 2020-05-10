namespace TES
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.DisplayChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Alpha = new System.Windows.Forms.Label();
            this.Gamma = new System.Windows.Forms.Label();
            this.Delta = new System.Windows.Forms.Label();
            this.gvData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayChart
            // 
            chartArea1.Name = "ChartArea1";
            this.DisplayChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.DisplayChart.Legends.Add(legend1);
            this.DisplayChart.Location = new System.Drawing.Point(111, 96);
            this.DisplayChart.Name = "DisplayChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.DisplayChart.Series.Add(series1);
            this.DisplayChart.Size = new System.Drawing.Size(876, 311);
            this.DisplayChart.TabIndex = 0;
            this.DisplayChart.Text = "chart1";
            // 
            // Alpha
            // 
            this.Alpha.AutoSize = true;
            this.Alpha.Location = new System.Drawing.Point(21, 21);
            this.Alpha.Name = "Alpha";
            this.Alpha.Size = new System.Drawing.Size(35, 13);
            this.Alpha.TabIndex = 1;
            this.Alpha.Text = "label1";
            // 
            // Gamma
            // 
            this.Gamma.AutoSize = true;
            this.Gamma.Location = new System.Drawing.Point(21, 42);
            this.Gamma.Name = "Gamma";
            this.Gamma.Size = new System.Drawing.Size(35, 13);
            this.Gamma.TabIndex = 2;
            this.Gamma.Text = "label2";
            // 
            // Delta
            // 
            this.Delta.AutoSize = true;
            this.Delta.Location = new System.Drawing.Point(21, 67);
            this.Delta.Name = "Delta";
            this.Delta.Size = new System.Drawing.Size(35, 13);
            this.Delta.TabIndex = 3;
            this.Delta.Text = "label3";
            // 
            // gvData
            // 
            this.gvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvData.Location = new System.Drawing.Point(111, 422);
            this.gvData.Name = "gvData";
            this.gvData.Size = new System.Drawing.Size(876, 234);
            this.gvData.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 684);
            this.Controls.Add(this.gvData);
            this.Controls.Add(this.Delta);
            this.Controls.Add(this.Gamma);
            this.Controls.Add(this.Alpha);
            this.Controls.Add(this.DisplayChart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DisplayChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart DisplayChart;
        private System.Windows.Forms.Label Alpha;
        private System.Windows.Forms.Label Gamma;
        private System.Windows.Forms.Label Delta;
        private System.Windows.Forms.DataGridView gvData;
    }
}


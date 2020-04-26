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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            chartArea3.Name = "ChartArea1";
            this.DisplayChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.DisplayChart.Legends.Add(legend3);
            this.DisplayChart.Location = new System.Drawing.Point(111, 33);
            this.DisplayChart.Name = "DisplayChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.DisplayChart.Series.Add(series3);
            this.DisplayChart.Size = new System.Drawing.Size(876, 311);
            this.DisplayChart.TabIndex = 0;
            this.DisplayChart.Text = "chart1";
            // 
            // Alpha
            // 
            this.Alpha.AutoSize = true;
            this.Alpha.Location = new System.Drawing.Point(22, 33);
            this.Alpha.Name = "Alpha";
            this.Alpha.Size = new System.Drawing.Size(35, 13);
            this.Alpha.TabIndex = 1;
            this.Alpha.Text = "label1";
            // 
            // Gamma
            // 
            this.Gamma.AutoSize = true;
            this.Gamma.Location = new System.Drawing.Point(22, 54);
            this.Gamma.Name = "Gamma";
            this.Gamma.Size = new System.Drawing.Size(35, 13);
            this.Gamma.TabIndex = 2;
            this.Gamma.Text = "label2";
            // 
            // Delta
            // 
            this.Delta.AutoSize = true;
            this.Delta.Location = new System.Drawing.Point(22, 79);
            this.Delta.Name = "Delta";
            this.Delta.Size = new System.Drawing.Size(35, 13);
            this.Delta.TabIndex = 3;
            this.Delta.Text = "label3";
            // 
            // gvData
            // 
            this.gvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvData.Location = new System.Drawing.Point(111, 363);
            this.gvData.Name = "gvData";
            this.gvData.Size = new System.Drawing.Size(876, 150);
            this.gvData.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 603);
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


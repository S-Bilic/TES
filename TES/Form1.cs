using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Alpha.Text = $"Alpha: {Program._TesAlpha.ToString()}";
            Gamma.Text = $"Gamma: {Program._TesGamma.ToString()}";
            Delta.Text = $"Delta: {Program._TesDelta.ToString()}";

            gvData.DataSource = Program.TESResults;

            DisplayChart.Series.Clear();
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Tes",
                LegendText = "Tes",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Line
            };
            DisplayChart.ChartAreas[0].AxisX.Interval = 5;
            this.DisplayChart.Series.Add(series1);
            foreach (var point in Program.TESResults.Where(x => x.Time >= 1).ToList())
            {
                DataPoint p = new DataPoint();
                p.SetValueXY(point.Time, point.Demand);
                p.ToolTip = string.Format("{0}, {1}", point.Time, point.Demand);
                series1.Points.Add(p);
            }
            DisplayChart.Invalidate();
        }
    }
}

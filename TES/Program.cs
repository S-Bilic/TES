using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace TES
{
    static class Program
    {
        private static List<DataPoint> _dataList = new List<DataPoint>();
        private static double _Alpha = 0.3;
        private static double _Gamma = 0.22;
        private static SES ses = new SES(_Alpha);
        private static DES des;
        private static List<DataPoint> SESResults;
        private static List<DataPoint> DESResults;
        private static List<DataPoint> TESResults;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetData();

            CalculateResults();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void CalculateResults()
        {
            //SES part
            var items = _dataList.Where(x => x.Time < 1).ToList();
            SESResults = items;
            foreach (var point in _dataList.Where(x => x.Time >= 1).ToList())
            {
                var previousPoint = _dataList.FirstOrDefault(x => x.Time == point.Time - 1);
                SESResults.Add(ses.CalculateResult(point, previousPoint));
            }

            double stap = 0.1;
            double sse = 0;
            double Alpha = 0.1;
            double Gamma = 0.1;
            _Gamma = 0.1;
            _Alpha = 0.1;
            while(stap >= 0.001)
            { 
                for (double a = 0; a < 10; a ++)
                {
                    for (double b = 0; b < 10; b++)
                    {
                        des = new DES(_Alpha + (a * stap), _Gamma + (b * stap));
                        //Only calculate DES
                        DESResults = _dataList.Where(x => x.Time < 1).ToList();
                        foreach (var point in _dataList.Where(x => x.Time >= 1 && x.Time < 37).ToList())
                        {
                            var previousPoint = _dataList.FirstOrDefault(x => x.Time == point.Time - 1);
                            DESResults.Add(des.CalculateResult(point, previousPoint));
                        }
                        var lastPoint = DESResults.FirstOrDefault(x => x.Time == 36);
                        for (int t = 37; t < 49; t++)
                        {
                            var datapoint = new DataPoint
                            {
                                Time = t,
                                Demand = ((t - 16) * lastPoint.Trend) + lastPoint.Demand,
                                ForecastError = 0,
                                Trend =0,
                                Level = 0,
                                OneStepForecast = 0,
                                Seasonal = 0,
                                SquaredError = 0
                            };
                            DESResults.Add(datapoint);
                        }
                        double currSSE = DESResults.Sum(x => x.SquaredError);
                        if (sse == 0 || sse > currSSE)
                        {
                            sse = currSSE;
                            if (stap == 10)
                            {
                                Gamma = b * stap;
                                Alpha = a * stap;
                            }
                            else {
                                Alpha = _Alpha + (a * stap);
                                Gamma = _Gamma + (b * stap);
                            }
                        }
                    }
                }
                _Alpha = Alpha;
                _Gamma = Gamma;
                stap /= 10;
            }

            

        }

        private static void SetData()
        {
            var path = @" C: \Users\Stefan\source\repos\TES\TES\Data\SwordForecasting.xlsm";

            var excel = new ExcelPackage(new FileInfo(path));
            var worksheet = excel.Workbook.Worksheets.FirstOrDefault(x => x.Name == "HoltWintersSeasonal");

            var rows = worksheet.SelectedRange[5, 1, 64, 8];
            var data = ((object[,])rows.Value);
            for (int i = 0; i < rows.Rows; i++)
            {
                var point = new DataPoint();
                point.Time = int.Parse(data[i, 0].ToString());
                point.Demand = double.Parse(data[i, 1] == null ? "0" : data[i, 1].ToString());
                point.Level = double.Parse(data[i, 2] == null ? "0" : data[i, 2].ToString());
                point.Trend = double.Parse(data[i, 3] == null ? "0" : data[i, 3].ToString());
                point.Seasonal = double.Parse(data[i, 4] == null ? "0" : data[i, 4].ToString());
                point.OneStepForecast = double.Parse(data[i, 5] == null ? "0" : data[i, 5].ToString());
                point.ForecastError = double.Parse(data[i, 6] == null ? "0" : data[i, 6].ToString());
                point.SquaredError = double.Parse(data[i, 7] == null ? "0" : data[i, 7].ToString());
                _dataList.Add(point);
            }
        }
    }
}

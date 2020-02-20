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
        private static double Alpha = 0.3;
        private static double Gamma = 0.22;
        private static SES ses = new SES(Alpha);
        private static DES des = new DES(Alpha, Gamma);
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

            for (double a = 0.1; a < 1; a += 0.1)
            {
                //Only calculate SES
                for (double b = 0.1; b < 1; b += 0.1)
                {
                    //Only calculate DES
                    for (double c = 0.1; c < 1; c += 0.1)
                    {
                        //Calculate TES
                    }
                }
            }

            SESResults = _dataList.Where(x => x.Time < 37).ToList();
            DESResults = _dataList.Where(x => x.Time < 37).ToList();

            foreach (var point in _dataList.Where(x => x.Time >= 37).ToList())
            {
                var previousPoint = _dataList.FirstOrDefault(x => x.Time == point.Time - 1);
                SESResults.Add(ses.CalculateResult(point, previousPoint));
                DESResults.Add(des.CalculateResult(point, previousPoint));
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

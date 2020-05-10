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
        public static List<DataPointTes> _dataList = new List<DataPointTes>();
        public static double _Alpha = 0.3;
        public static double _Gamma = 0.22;
        public static double _TesGamma = 0.00;
        public static double _TesAlpha = 0.00;
        public static double _TesDelta = 0.00;
        public static SES ses = new SES(_Alpha);
        public static DES des;
        public static TES tes;
        public static List<DataPointTes> SESResults;
        public static List<DataPointTes> DESResults;
        public static List<DataPointTes> TESResults;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Init. data
            SetData();
            // Bereken resultaten
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

            // Init. variables
            int LastKnownPoint = 37;
            int increment = 12;
            double stap = 0.1;
            double sse = 0;
            double Alpha = 0.1;
            double Gamma = 0.1;
            _Gamma = 0.1;
            _Alpha = 0.1;

            double TesSSE = 0;
            double TesAlpha = 0.1;
            double TesGamma = 0.1;
            double TesDelta = 0.1;
            _TesGamma = 0.1;
            _TesAlpha = 0.1;
            _TesDelta = 0.1;

            while (stap >= 0.001)
            {
                for (double a = 0; a < 10; a++)
                {
                    for (double b = 0; b < 10; b++)
                    {
                        des = new DES(_Alpha + (a * stap), _Gamma + (b * stap));
                        // Alleen DES Berekenen
                        DESResults = _dataList.Where(x => x.Time < 1).ToList();
                        foreach (var point in _dataList.Where(x => x.Time >= 1 && x.Time < LastKnownPoint).ToList())
                        {
                            var previousPoint = _dataList.FirstOrDefault(x => x.Time == point.Time - 1);
                            DESResults.Add(des.CalculateResult(point, previousPoint));
                        }
                        var lastPoint = DESResults.FirstOrDefault(x => x.Time == LastKnownPoint - 1);
                        for (int t = LastKnownPoint; t < 49; t++)
                        {
                            var datapoint = new DataPointTes
                            {
                                Time = t,
                                Demand = ((t - 16) * lastPoint.Trend) + lastPoint.Demand,
                                ForecastError = 0,
                                Trend = 0,
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
                            else
                            {
                                Alpha = _Alpha + (a * stap);
                                Gamma = _Gamma + (b * stap);
                            }
                        }
                        for (double c = 0; c < 10; c++)
                        {
                            tes = new TES(_TesAlpha + (a * stap), _TesGamma + (b * stap), _TesDelta + (c * stap));
                            // Begin bij t = 1
                            TESResults = _dataList.Where(x => x.Time < 1).ToList();
                            // Tot t = 36
                            var tesLastPoint = TESResults.FirstOrDefault(x => x.Time == LastKnownPoint - 1);
                            foreach (var point in _dataList.Where(x => x.Time >= 1 && x.Time < LastKnownPoint).ToList())
                            {
                                var previousPoint = _dataList.FirstOrDefault(x => x.Time == point.Time - 1);
                                double SeasonalAdjustment = _dataList.FirstOrDefault(x => x.Time == point.Time - increment).Seasonal;
                                // Bereken alle resulaten (Level, Trend, Seasonality etc..) en voeg ze toe aan TESResults
                                TESResults.Add(tes.CalculateResult(point, previousPoint, SeasonalAdjustment));
                            }

                            double TescurrSSE = TESResults.Sum(x => x.SquaredError);
                            // Als 'SSE' value groter is dan 'huidige SSE' value
                            // -> Krijgt 'SSE' de value van 'huidige SSE'
                            if (TesSSE == 0 || TesSSE > TescurrSSE)
                            {
                                TesSSE = TescurrSSE;
                                if (stap == 10)
                                {
                                    TesGamma = b * stap;
                                    TesAlpha = a * stap;
                                    TesDelta = c * stap;
                                }
                                else
                                {
                                    TesAlpha = _TesAlpha + (a * stap);
                                    TesGamma = _TesGamma + (b * stap);
                                    TesDelta = _TesDelta + (c * stap);
                                }
                            }
                        }
                    }
                }
                _Alpha = Alpha;
                _Gamma = Gamma;

                // Uncomment dit om resultaten te testen volgens excel sheet.

                //_TesAlpha = 0.307003546945751;
                //_TesGamma = 0.228914336546831;
                //_TesDelta = 0;

                // Beste Alpha, Gamma, Delta
                _TesAlpha = TesAlpha;
                _TesGamma = TesGamma;
                _TesDelta = TesDelta;
                stap /= 10;
            }

            // Bereken TES nog keer met de definitieve Alpha, Gamma, Delta
            tes = new TES(_TesAlpha, _TesGamma, _TesDelta);
            var TesLastPoint = TESResults.FirstOrDefault(x => x.Time == LastKnownPoint - 1);
            TESResults = _dataList.Where(x => x.Time < 1).ToList();
            foreach (var point in _dataList.Where(x => x.Time >= 1 && x.Time < LastKnownPoint).ToList())
            {
                var previousPoint = _dataList.FirstOrDefault(x => x.Time == point.Time - 1);
                double SeasonalAdjustment = _dataList.FirstOrDefault(x => x.Time == point.Time - increment).Seasonal;
                TESResults.Add(tes.CalculateResult(point, previousPoint, SeasonalAdjustment));
            }
            // Van t = 37 tot t = 49
            for (int t = LastKnownPoint; t < 49; t++)
            {
                double SeasonalAdjustment = _dataList.FirstOrDefault(x => x.Time == t - increment).Seasonal;
                var datapoint = new DataPointTes
                {
                    Time = t,
                    // Bereken de predictions voor de demand
                    Demand = tes.PredictValues(TesLastPoint, SeasonalAdjustment, t),
                    ForecastError = 0,
                    Trend = 0,
                    Level = 0,
                    OneStepForecast = 0,
                    Seasonal = 0,
                    SquaredError = 0
                };
                TESResults.Add(datapoint);
            }
        }

        // Lees data uit excel file van 'HoltWinterSeasonal' Tab
        private static void SetData()
        {
            var path = @" C: \Users\Stefan\source\repos\TES\TES\Data\SwordForecasting.xlsm";

            var excel = new ExcelPackage(new FileInfo(path));
            var worksheet = excel.Workbook.Worksheets.FirstOrDefault(x => x.Name == "HoltWintersSeasonal");

            var rows = worksheet.SelectedRange[5, 1, 64, 8];
            var data = ((object[,])rows.Value);
            for (int i = 0; i < rows.Rows; i++)
            {
                var point = new DataPointTes();
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

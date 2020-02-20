using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES
{
    public class DataPoint
    {
        public int Time { get; set; }
        public double Demand { get; set; }
        public double  Level { get; set; }
        public double Trend { get; set; }
        public double Seasonal { get; set; }
        public double OneStepForecast { get; set; }
        public double ForecastError { get; set; }
        public double SquaredError { get; set; }
    }
}

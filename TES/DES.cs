using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES
{
    public class DES
    {
        public double Alpha;
        public double Gamma;
        public DES(double alpha, double gamma)
        {
            this.Alpha = alpha;
            this.Gamma = gamma;
        }

        public DataPoint CalculateResult(DataPoint CurrentPoint, DataPoint PreviousPoint)
        {
            CurrentPoint.OneStepForecast = PreviousPoint.Level + PreviousPoint.Trend;
            CurrentPoint.ForecastError = CurrentPoint.Demand - CurrentPoint.OneStepForecast;
            CurrentPoint.Level = (PreviousPoint.Level + PreviousPoint.Trend) + (Alpha * CurrentPoint.ForecastError);
            CurrentPoint.Trend = PreviousPoint.Trend + (Gamma * Alpha * CurrentPoint.ForecastError);
            CurrentPoint.SquaredError = Math.Pow(CurrentPoint.ForecastError, 2);
            return CurrentPoint;
        }
    }
}

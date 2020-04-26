using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES
{
    public class SES
    {
        public double Alpha;
        public SES(double Alpha)
        {
            this.Alpha = Alpha;
        }

        public DataPointTes CalculateResult(DataPointTes point, DataPointTes previousPoint)
        {
            point.Level = previousPoint.Level + Alpha * (point.Demand - previousPoint.Level);
            point.OneStepForecast = previousPoint.Level;
            point.ForecastError = point.Demand - point.OneStepForecast;
            point.SquaredError = Math.Pow(point.ForecastError, 2);
            return point;
        }
    }
}

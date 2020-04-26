using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES
{
    public class TES
    {
        public double Alpha;
        public double Gamma;
        public double Delta;
        public TES(double alpha, double gamma, double delta)
        {
            this.Alpha = alpha;
            this.Gamma = gamma;
            this.Delta = delta;
        }


        public DataPointTes CalculateResult(DataPointTes CurrentPoint, DataPointTes PreviousPoint, double SeasonalAdjustment)
        {
            CurrentPoint.Level = (PreviousPoint.Level + PreviousPoint.Trend) + 
                (Alpha * (CurrentPoint.Demand - (PreviousPoint.Level + PreviousPoint.Trend) * SeasonalAdjustment) / SeasonalAdjustment);
            CurrentPoint.Trend = PreviousPoint.Trend + Gamma * Alpha * 
                (CurrentPoint.Demand - (PreviousPoint.Level + PreviousPoint.Trend) * SeasonalAdjustment) / SeasonalAdjustment;
            CurrentPoint.Seasonal = SeasonalAdjustment + Delta * (1 - Alpha) *
                (CurrentPoint.Demand - (PreviousPoint.Level + PreviousPoint.Trend) * SeasonalAdjustment) / (PreviousPoint.Level + PreviousPoint.Trend);
            CurrentPoint.OneStepForecast = (PreviousPoint.Level + PreviousPoint.Trend) * SeasonalAdjustment;
            CurrentPoint.ForecastError = CurrentPoint.Demand - CurrentPoint.OneStepForecast;
            CurrentPoint.SquaredError = Math.Pow(CurrentPoint.ForecastError, 2);
            return CurrentPoint;
        }

        public double PredictValues(DataPointTes LastKnown, double SeasonalAdjustment, int Time)
        {
            return (LastKnown.Level + (Time - LastKnown.Time) * LastKnown.Trend) * SeasonalAdjustment;
        }
    }
}

using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class InterchangingAtPeakHour : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public InterchangingAtPeakHour(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            return (cnn.IsInterchange(station) && inputOption.JourneyTime.IsPeak() ? CostCalculationConfigs.InterchangingAtPeakHourCost : 0) 
                   + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
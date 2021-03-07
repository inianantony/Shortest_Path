using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class PeakHourInNeNs : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public PeakHourInNeNs(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            return (cnn.IsInNeOrNs(station) && inputOption.JourneyTime.IsPeak() && !cnn.IsInterchanged(station) ? CostCalculationConfigs.PeakHourNeNsCost : 0)
                   + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
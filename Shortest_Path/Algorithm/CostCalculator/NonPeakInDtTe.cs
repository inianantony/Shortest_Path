using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class NonPeakInDtTe : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public NonPeakInDtTe(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            return (inputOption.JourneyTime.IsNonPeakBeforeNight() && cnn.IsInDtTe(station) && !cnn.IsInterchange(station) ? CostCalculationConfigs.NonPeakInDtTeCost : 0)
                   + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
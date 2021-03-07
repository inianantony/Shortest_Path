using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class InterchangingAtNonPeak : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public InterchangingAtNonPeak(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            return (inputOption.JourneyTime.IsNonPeakBeforeNight() && cnn.IsInterchange(station) ? CostCalculationConfigs.InterchangingAtNonPeakCost : 0) 
                   + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
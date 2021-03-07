using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class InterchangingAtNight : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public InterchangingAtNight(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            return (inputOption.JourneyTime.IsNight() && cnn.IsInterchange(station) ? CostCalculationConfigs.InterchangingAtNightCost : 0)
                   + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
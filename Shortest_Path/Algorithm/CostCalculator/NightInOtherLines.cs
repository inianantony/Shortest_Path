using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class NightInOtherLines : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public NightInOtherLines(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            return (inputOption.JourneyTime.IsNight() && !cnn.IsInTe(station) && !cnn.IsInterchanged(station) ? CostCalculationConfigs.NightInOtherLinesCost : 0)
                   + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
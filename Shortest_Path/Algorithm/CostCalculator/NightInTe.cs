using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class NightInTe : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public NightInTe(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            return (inputOption.JourneyTime.IsNight() && cnn.IsInTe(station) && !cnn.IsInterchanged(station) ? CostCalculationConfigs.NightInTeCost : 0) 
                   + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
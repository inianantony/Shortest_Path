using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class DisabledJourneyTime : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public DisabledJourneyTime(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            return inputOption.JourneyTime.IsDisabled() ? new BaseCostCalculator().GetCost(inputOption, cnn, station) : _inner.GetCost(inputOption, cnn, station);
        }
    }
}
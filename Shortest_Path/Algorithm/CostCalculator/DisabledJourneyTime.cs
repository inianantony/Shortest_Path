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

        public decimal GetCost(Options option, Edge cnn, Station station)
        {
            return option.JourneyTime.IsDisabled() ? new BaseCostCalculator().GetCost(option, cnn, station) : _inner.GetCost(option, cnn, station);
        }
    }
}
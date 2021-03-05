using System.Collections.Generic;
using System.Linq;
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

        public double GetCost(Options option, Edge cnn, Station station)
        {
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var isInNeOrNs = commonStations.Intersect(new List<string> { "NE", "NS" }).Any();
            var isPeakHour = option.JourneyTime.IsPeak();
            return (isInNeOrNs && isPeakHour && !option.JourneyTime.IsDisabled ? 12 : 0) + _inner.GetCost(option, cnn, station);
        }
    }
}
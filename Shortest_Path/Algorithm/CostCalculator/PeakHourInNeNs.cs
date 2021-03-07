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

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var isInNeOrNs = commonStations.Intersect(new List<string> { "NE", "NS" }).Any();
            var isPeakHour = inputOption.JourneyTime.IsPeak();
            var interchange = !commonStations.Any();
            return (isInNeOrNs && isPeakHour && !interchange ? 12 : 0) + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
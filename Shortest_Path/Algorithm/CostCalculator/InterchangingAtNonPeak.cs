using System.Linq;
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

        public decimal GetCost(Options option, Edge cnn, Station station)
        {
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var isNight = option.JourneyTime.IsNight();
            var isNonPeak = option.JourneyTime.IsNonPeak();
            var interchange = !commonStations.Any();
            return (isNonPeak && !isNight && interchange ? 10 : 0) + _inner.GetCost(option, cnn, station);
        }
    }
}
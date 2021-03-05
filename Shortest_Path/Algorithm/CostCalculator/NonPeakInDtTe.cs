using System.Collections.Generic;
using System.Linq;
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

        public double GetCost(Options option, Edge cnn, Station station)
        {
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var getLies = cnn.ConnectedStation.Lines.Union(station.Lines).ToList();
            var isInDtTe = getLies.Intersect(new List<string> { "DT", "TE" }).Any();
            var isNight = option.JourneyTime.IsNight();
            var isNonPeak = option.JourneyTime.IsNonPeak();
            return (isNonPeak && !isNight && isInDtTe && !option.JourneyTime.IsDisabled() ? 8 : 0) + _inner.GetCost(option, cnn, station);
        }
    }
}
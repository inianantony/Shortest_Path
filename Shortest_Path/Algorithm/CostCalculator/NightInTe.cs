using System.Collections.Generic;
using System.Linq;
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

        public double GetCost(Options option, Edge cnn, Station station)
        {
            var getLies = cnn.ConnectedStation.Lines.Union(station.Lines).ToList();
            var isInTe = getLies.Intersect(new List<string> { "TE" }).Any();
            var isNight = option.JourneyTime.IsNight();
            return (isNight && isInTe && !option.JourneyTime.IsDisabled() ? 8 : 0) + _inner.GetCost(option, cnn, station);
        }
    }
}
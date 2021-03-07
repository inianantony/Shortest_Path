using System.Collections.Generic;
using System.Linq;
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
            var getLies = cnn.ConnectedStation.Lines.Union(station.Lines).ToList();
            var isInTe = getLies.Intersect(new List<string> { "TE" }).Any();
            var isNight = inputOption.JourneyTime.IsNight();
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var interchange = !commonStations.Any();
            return (isNight && !isInTe && !interchange ? 10 : 0) + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
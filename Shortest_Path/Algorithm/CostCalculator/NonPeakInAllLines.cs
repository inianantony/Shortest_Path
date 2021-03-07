using System.Collections.Generic;
using System.Linq;
using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class NonPeakInAllLines : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public NonPeakInAllLines(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var getLies = cnn.ConnectedStation.Lines.Union(station.Lines).ToList();
            var isInDtTe = getLies.Intersect(new List<string> { "DT", "TE" }).Any();
            var isNight = inputOption.JourneyTime.IsNight();
            var isNonPeak = inputOption.JourneyTime.IsNonPeak();
            var interchange = !commonStations.Any();
            return (isNonPeak && !interchange && !isNight && !isInDtTe ? 10 : 0) + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
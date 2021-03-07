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

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var isNight = inputOption.JourneyTime.IsNight();
            var isNonPeak = inputOption.JourneyTime.IsNonPeak();
            var interchange = !commonStations.Any();
            return (isNonPeak && !isNight && interchange ? 10 : 0) + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
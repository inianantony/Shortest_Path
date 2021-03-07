using System.Linq;
using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class InterchangingAtNight : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public InterchangingAtNight(ICostCalculator inner)
        {
            _inner = inner;
        }

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            var isNight = inputOption.JourneyTime.IsNight();
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var interchange = !commonStations.Any();
            return (isNight && interchange ? 10 : 0) + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
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

        public double GetCost(Options option, Edge cnn, Station station)
        {
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var isNight = option.JourneyTime.IsNight();
            var interchange = !commonStations.Any();
            return (isNight && interchange && !option.JourneyTime.IsDisabled() ? 10 : 0) + _inner.GetCost(option, cnn, station);
        }
    }
}
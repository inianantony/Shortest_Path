using System.Linq;
using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class InterchangingAtPeakHour : ICostCalculator
    {
        private readonly ICostCalculator _inner;

        public InterchangingAtPeakHour(ICostCalculator inner)
        {
            _inner = inner;
        }

        public double GetCost(Options option, Edge cnn, Station station)
        {
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var isPeakHour = option.JourneyTime.IsPeak();
            var interchange = !commonStations.Any();
            return (interchange && isPeakHour && !option.JourneyTime.IsDisabled() ? 15 : 0) + _inner.GetCost(option, cnn, station);
        }
    }
}
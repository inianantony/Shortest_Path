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

        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            var commonStations = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var isPeakHour = inputOption.JourneyTime.IsPeak();
            var interchange = !commonStations.Any();
            return (interchange && isPeakHour ? 15 : 0) + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
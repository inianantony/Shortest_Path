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
            return (inputOption.JourneyTime.IsNonPeakBeforeNight() && !cnn.IsInterchange(station)  && !cnn.IsInDtTe(station) ? CostCalculationConfigs.NonPeakInAllLinesCost : 0) 
                   + _inner.GetCost(inputOption, cnn, station);
        }
    }
}
using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class BaseCostCalculator : ICostCalculator
    {
        public double GetCost(Options option, Edge cnn, Station station)
        {
            return cnn.Cost;
        }
    }
}
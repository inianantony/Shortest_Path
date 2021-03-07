using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public class BaseCostCalculator : ICostCalculator
    {
        public decimal GetCost(InputOption inputOption, Edge cnn, Station station)
        {
            return cnn.Cost;
        }
    }
}
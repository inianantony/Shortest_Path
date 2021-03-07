using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public interface ICostCalculator
    {
        decimal GetCost(Options option, Edge cnn, Station station);
    }
}
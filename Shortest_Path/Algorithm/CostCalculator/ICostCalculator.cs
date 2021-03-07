using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public interface ICostCalculator
    {
        decimal GetCost(InputOption inputOption, Edge cnn, Station station);
    }
}
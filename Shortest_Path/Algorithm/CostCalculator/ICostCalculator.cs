using Shortest_Path.Models;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public interface ICostCalculator
    {
        double GetCost(Options option, Edge cnn, Station station);
    }
}
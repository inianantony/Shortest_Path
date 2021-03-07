using System.Collections.Generic;
using Shortest_Path.Models;

namespace Shortest_Path.Algorithm
{
    public interface ISearchAlgorithm
    {
        List<Station> FillShortestPath(List<Station> stations, InputOption inputOption);
    }
}
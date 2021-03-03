using System.Collections.Generic;
using Shortest_Path.Models;

namespace Shortest_Path
{
    public interface ISearchAlgorithm
    {
        List<Station> FillShortestPath(List<Station> stations, Station start, Station end);
    }
}
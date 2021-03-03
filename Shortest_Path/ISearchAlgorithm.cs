using System.Collections.Generic;

namespace Shortest_Path
{
    public interface ISearchAlgorithm
    {
        List<Station> FillShortestPath(List<Station> stations, Station start, Station end);
    }
}
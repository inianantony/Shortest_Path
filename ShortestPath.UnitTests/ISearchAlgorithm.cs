using System.Collections.Generic;

namespace ShortestPath.UnitTests
{
    public interface ISearchAlgorithm
    {
        List<Station> FillShortestPath(List<Station> stations, Station start, Station end);
    }
}
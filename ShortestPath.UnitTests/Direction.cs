using System.Collections.Generic;
using System.Linq;

namespace ShortestPath.UnitTests
{
    public class Direction
    {
        private readonly ISearchAlgorithm _searchAlgorithm;
        private readonly Station _start;
        private readonly Station _end;
        public double ShortestPathCost { get; set; }

        public double ShortestPathLength { get; set; }

        public Direction(ISearchAlgorithm searchAlgorithm, Station start, Station end)
        {
            _searchAlgorithm = searchAlgorithm;
            _start = start;
            _end = end;
        }

        public RouteInfo PrepareRouteInfoFrom(Map map)
        {
            var mappedStations = _searchAlgorithm.FillShortestPath(map.Stations, _start, _end);

            var shortestPath = new List<Station>();
            var end = mappedStations.First(a => a == _end);
            shortestPath.Add(end);
            BuildShortestPath(shortestPath, end);
            shortestPath.Reverse();

            ShortestPathLength += end.MinimumCost ?? 0;
            ShortestPathCost += end.MinimumCost ?? 0;

            return new RouteInfo(shortestPath, _start, end);
        }

        private void BuildShortestPath(List<Station> stations, Station station)
        {
            if (station.NearestToStart == null)
                return;
            stations.Add(station.NearestToStart);
            BuildShortestPath(stations, station.NearestToStart);
        }
    }
}
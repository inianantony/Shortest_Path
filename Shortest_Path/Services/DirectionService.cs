 using System.Collections.Generic;
using System.Linq;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;

namespace Shortest_Path.Services
{
    public class DirectionService
    {
        private readonly ISearchAlgorithm _searchAlgorithm;
        private readonly Station _start;
        private readonly Station _end;

        public DirectionService(ISearchAlgorithm searchAlgorithm, Options options)
        {
            _searchAlgorithm = searchAlgorithm;
            _start = options.StartStation;
            _end = options.EndStation;
        }

        public RouteInfo PrepareRouteInfoFrom(Map map)
        {
            var options = new Options{Start = _start.StationName, End = _end.StationName};
            var mappedStations = _searchAlgorithm.FillShortestPath(map.Stations, options);

            var shortestPath = new List<Station>();
            var end = mappedStations.First(a => a.IsSameAs(_end));
            shortestPath.Add(end);
            BuildShortestPath(shortestPath, end);
            shortestPath.Reverse();

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
 using System.Collections.Generic;
using System.Linq;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;

namespace Shortest_Path.Services
{
    public class DirectionService
    {
        private readonly ISearchAlgorithm _searchAlgorithm;
        private readonly Options _options;

        public DirectionService(ISearchAlgorithm searchAlgorithm, Options options)
        {
            _searchAlgorithm = searchAlgorithm;
            _options = options;
        }

        public RouteInfo PrepareRouteInfoFrom(Map map)
        {
            var mappedStations = _searchAlgorithm.FillShortestPath(map.Stations, _options);

            var shortestPath = new List<Station>();
            var end = mappedStations.First(a => a.IsSameAs(_options.EndStation.StationName));
            shortestPath.Add(end);
            BuildShortestPath(shortestPath, end);
            shortestPath.Reverse();

            var start = mappedStations.First(a => a.IsSameAs(_options.StartStation.StationName));
            return new RouteInfo(shortestPath, start, end);
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
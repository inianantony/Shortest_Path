using System.Collections.Generic;
using System.Linq;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;

namespace Shortest_Path.Services
{
    public class DirectionService
    {
        private readonly ISearchAlgorithm _searchAlgorithm;
        private readonly InputOption _inputOption;

        public DirectionService(ISearchAlgorithm searchAlgorithm, InputOption inputOption)
        {
            _searchAlgorithm = searchAlgorithm;
            _inputOption = inputOption;
        }

        public RouteInfo PrepareRouteInfoFrom(Map map)
        {
            var mappedStations = _searchAlgorithm.FillShortestPath(map.Stations, _inputOption);

            var shortestPath = new List<Station>();
            var end = mappedStations.First(a => a.IsSameAs(_inputOption.EndStation.StationName));
            shortestPath.Add(end);
            BuildShortestPath(shortestPath, end);
            shortestPath.Reverse();

            var start = mappedStations.First(a => a.IsSameAs(_inputOption.StartStation.StationName));
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
using System.Collections.Generic;
using Shortest_Path.Algorithm;
using Shortest_Path.Mapper;
using Shortest_Path.Models;

namespace Shortest_Path
{
    public class RouteService
    {
        public RouteInfo GetARoute(List<RawStationData> rawRecords, Options option)
        {
            var rawStationConvertor = new RawStationConvertor();
            var stations = rawStationConvertor.Convert(rawRecords);
            var mrtLines = rawStationConvertor.GroupStationsByLines(rawRecords, stations);

            var map = new Map().LinkStations(stations, mrtLines);

            var start = new Station(option.Start);
            var end = new Station(option.End);

            ISearchAlgorithm algorithm = new DijkstraSearch();
            Direction direction = new Direction(algorithm, start, end);
            var routeInfo = direction.PrepareRouteInfoFrom(map);
            return routeInfo;
        }
    }
}
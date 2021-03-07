using System.Collections.Generic;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;
using Shortest_Path.Printer;
using Shortest_Path.Reader;
using Shortest_Path.Services;

namespace Shortest_Path
{
    public class Program
    {
        private static IPrinter _printer;
        private static IStationDataReader _reader;

        public static void Main(string[] args)
        {
            _printer = new ConsolePrinter();
            _reader = new CsvStationDataReader();

            var option = Options.GetInputOptions(args);

            var rawRecords = ReadRawStationData(option);

            var map = GetMap(rawRecords);

            option.ValidateStations(map);

            var routeInfo = GetRoute(map, option);

            PrintTheJourney(routeInfo);
        }

        private static RouteInfo GetRoute(Map map, Options option)
        {
            ISearchAlgorithm algorithm = new DijkstraSearch();
            var start = option.StartStation;
            var end = option.EndStation;
            var directionService = new DirectionService(algorithm, new Options
            {
                Start = start.StationName,
                End = end.StationName
            });
            return directionService.PrepareRouteInfoFrom(map);
        }

        private static Map GetMap(List<RawStationData> rawRecords)
        {
            var map = new Map(rawRecords).LinkStations();
            return map;
        }

        private static List<RawStationData> ReadRawStationData(Options options)
        {
            return _reader.GetRawStationRecords(options.CsvPath);
        }

        private static void PrintTheJourney(RouteInfo routeInfo)
        {
            _printer.With(routeInfo).DisplayRoutes();
        }
    }
}

using System.Collections.Generic;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;
using Shortest_Path.Reader;
using Shortest_Path.Services;
using Shortest_Path.Writer;

namespace Shortest_Path
{
    public class Program
    {
        private static IPrinter _printer;
        public static void Main(string[] args)
        {
            _printer = new ConsolePrinter();

            var option = Options.GetInputOptions(args);

            var rawRecords = ReadRawStationData(option);

            var map = GetMap(rawRecords);

            var routeInfo = GetRoute(map, option);

            PrintTheJourney(routeInfo);
        }

        private static RouteInfo GetRoute(Map map, Options option)
        {
            ISearchAlgorithm algorithm = new DijkstraSearch();
            var directionService = new DirectionService(algorithm, option.StartStation, option.EndStation);
            return directionService.PrepareRouteInfoFrom(map);
        }

        private static Map GetMap(List<RawStationData> rawRecords)
        {
            var map = new Map(rawRecords).LinkStations();
            return map;
        }

        private static List<RawStationData> ReadRawStationData(Options options)
        {
            IStationDataReader reader = new CsvStationDataReader(options.CsvPath);
            return reader.GetRawStaionRecords();
        }

        private static void PrintTheJourney(RouteInfo routeInfo)
        {
            _printer.With(routeInfo).DisplayRoutes();
        }
    }
}

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
        public static IPrinter Printer = new ConsolePrinter();
        public static IStationDataReader Reader = new CsvStationDataReader();

        public static void Main(string[] args)
        {
            var option = InputOption.Get(args);

            var rawRecords = ReadRawStationData(option);

            var map = GetMap(rawRecords);

            option.ValidateStations(map);

            var routeInfo = GetRoute(map, option);

            PrintTheJourney(routeInfo);
        }

        private static RouteInfo GetRoute(Map map, InputOption inputOption)
        {
            ISearchAlgorithm algorithm = new DijkstraSearch();
            var directionService = new DirectionService(algorithm, inputOption);
            return directionService.PrepareRouteInfoFrom(map);
        }

        private static Map GetMap(List<RawStationData> rawRecords)
        {
            var map = new Map(rawRecords).LinkStations();
            return map;
        }

        private static List<RawStationData> ReadRawStationData(InputOption inputOption)
        {
            return Reader.GetRawStationRecords(inputOption.CsvPath);
        }

        private static void PrintTheJourney(RouteInfo routeInfo)
        {
            var printer = Printer.With(routeInfo);
            printer.DisplayRoutes();
        }
    }
}

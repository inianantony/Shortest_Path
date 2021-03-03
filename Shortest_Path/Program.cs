using System.Collections.Generic;
using Shortest_Path.Models;

namespace Shortest_Path
{
    public class Program
    {
        static void Main(string[] args)
        {
            var option = Options.GetOptions(args);

            var rawRecords = ReadRawStationData();

            var routeInfo = new RouteService().GetARoute(rawRecords, option);

            PrintTheJourney(routeInfo);
        }

        private static List<RawStationData> ReadRawStationData()
        {
            IStationDataReader reader = new CsvStationDataReader(@"StationMap.csv");
            var rawRecords = reader.GetRawStaionRecords();
            return rawRecords;
        }

        private static void PrintTheJourney(RouteInfo routeInfo)
        {
            IPrinter printer = new ConsolePrinter(routeInfo);
            printer.PrintJourneyTitle();
            printer.PrintStations();
            printer.PrintRoute();
            printer.PrintJourney();
        }
    }
}

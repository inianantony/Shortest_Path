﻿using System.Collections.Generic;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;
using Shortest_Path.Reader;
using Shortest_Path.Services;
using Shortest_Path.Writer;

namespace Shortest_Path
{
    public class Program
    {
        static void Main(string[] args)
        {
            var option = Options.GetOptions(args);

            var rawRecords = ReadRawStationData(option);

            var routeInfo = GetRoute(rawRecords, option);

            PrintTheJourney(routeInfo);
        }

        private static RouteInfo GetRoute(List<RawStationData> rawRecords, Options option)
        {
            var map = new Map(rawRecords).LinkStations();

            ISearchAlgorithm algorithm = new DijkstraSearch();
            var directionService = new DirectionService(algorithm, option.StartStation, option.EndStation);
            return directionService.PrepareRouteInfoFrom(map);
        }

        private static List<RawStationData> ReadRawStationData(Options options)
        {
            IStationDataReader reader = new CsvStationDataReader(options.CsvPath);
            return reader.GetRawStaionRecords();
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

using System.Collections.Generic;
using NUnit.Framework;
using Shortest_Path;

namespace ShortestPath.UnitTests
{
    [TestFixture]
    public class CoverageTest
    {
        [Test]
        public void FullCoverageTest()
        {
            IStationDataReader reader = new TestStationDataReader();
            var rawRecords = reader.GetRawStaionRecords();

            var rawStationConvertor = new RawStationConvertor();
            var stations = rawStationConvertor.Convert(rawRecords);
            var  mrtLines = rawStationConvertor.GroupStationsByLines(rawRecords, stations);

            var stationRecords = new StationRecords(stations);
            var linkedStations = stationRecords.LinkStations(stations,mrtLines);
            var map = new Map(linkedStations);

            var start = new Station("SengKang");
            var end = new Station("Bishan");
            var route = map.GetRouteFor(start, end);

            Direction direction = new Direction(route);
            IPrinter printer = new ConsolePrinter(direction);
            printer.PrintJourneyTitle();
            printer.PrintStations();
            printer.PrintRoute();
            printer.PrintJourney();
        }
    }
}
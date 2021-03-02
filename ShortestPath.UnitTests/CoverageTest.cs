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



            var stationRecords = new StationRecords(new RawStationToStationConvertor().Convert(rawRecords));
            var map = stationRecords.GetMap();

            var start = new Station { StationName = "SengKang" };
            var end = new Station { StationName = "Bishan" };
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
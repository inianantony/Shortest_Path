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
            var mrtLines = rawStationConvertor.GroupStationsByLines(rawRecords, stations);

            var map = new Map().LinkStations(stations, mrtLines);

            var start = new Station("SengKang");
            var end = new Station("Bishan");

            ISearchAlgorithm algorithm = new DijkstraSearch();
            Direction direction = new Direction(algorithm, start, end);
            var routeInfo = direction.PrepareRouteInfoFrom(map);

            Assert.IsNotEmpty(routeInfo.JourneyTitle);
            Assert.IsNotEmpty(routeInfo.Route);
            Assert.IsNotEmpty(routeInfo.StationsTravelled);
            Assert.IsNotEmpty(routeInfo.Journey);
        }
    }
}
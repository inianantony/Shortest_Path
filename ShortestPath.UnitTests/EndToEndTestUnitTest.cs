using System.Collections.Generic;
using NUnit.Framework;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;
using Shortest_Path.Services;

namespace ShortestPath.UnitTests
{
    [TestFixture]
    public class EndToEndTestUnitTest
    {
        [Test]
        public void GetRoute_End_To_End_Test()
        {
            var rawRecords = new List<RawStationData>
            {
                new RawStationData {StationCode = "NE1", StationName = "SengKang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE2", StationName = "Kovan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE3", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE4", StationName = "BoonKeng", OpeningDate = string.Empty},
                new RawStationData {StationCode = "CC1", StationName = "Lorang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "CC2", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "CC3", StationName = "Bishan", OpeningDate = string.Empty},
            };

            var map = new Map(rawRecords).LinkStations();

            var start = new Station("SengKang");
            var end = new Station("Bishan");

            ISearchAlgorithm algorithm = new DijkstraSearch();
            var directionService = new DirectionService(algorithm, start, end);
            var routeInfo = directionService.PrepareRouteInfoFrom(map);

            Assert.IsTrue(routeInfo.JourneyTitle.Contains(start.StationName), $"Was : {start.StationName}");
            Assert.IsNotEmpty(routeInfo.Route);
            Assert.IsTrue(routeInfo.StationsTraveled.Contains("4"), $"Was : {routeInfo.StationsTraveled}");
            Assert.IsNotEmpty(routeInfo.Journey);
        }
    }
}
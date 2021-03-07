using System;
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
        public void GetRoute_WithOutTimeFactor()
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
            var directionService = new DirectionService(algorithm, new InputOption
            {
                Start = start.StationName,
                End = end.StationName
            });
            var routeInfo = directionService.PrepareRouteInfoFrom(map);

            Assert.IsTrue(routeInfo.JourneyTitle.Contains(start.StationName), $"Was : {start.StationName}");
            Assert.IsNotEmpty(routeInfo.Route);
            Assert.IsTrue(routeInfo.StationsTraveled.Contains("4"), $"Was : {routeInfo.StationsTraveled}");
            Assert.IsNotEmpty(routeInfo.Journey);
        }

        [Test]
        public void GetRoute_NightTime_In_DT()
        {
            var rawRecords = new List<RawStationData>
            {
                new RawStationData {StationCode = "NE1", StationName = "SengKang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE2", StationName = "Kovan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE3", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE4", StationName = "BoonKeng", OpeningDate = string.Empty},
                new RawStationData {StationCode = "DT1", StationName = "Lorang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "DT2", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "DT3", StationName = "Bishan", OpeningDate = string.Empty},
            };

            var map = new Map(rawRecords).LinkStations();

            var start = new Station("SengKang");
            var end = new Station("Bishan");

            ISearchAlgorithm algorithm = new DijkstraSearch();
            var directionService = new DirectionService(algorithm, new InputOption
            {
                Start = start.StationName,
                End = end.StationName,
                StartTime = new DateTime(2021, 3, 5, 23, 40, 00)
            });
            var routeInfo = directionService.PrepareRouteInfoFrom(map);

            Assert.IsTrue(routeInfo.JourneyTitle.Contains(start.StationName), $"Was : {start.StationName}");
            Assert.IsNotEmpty(routeInfo.Route);
            Assert.IsTrue(routeInfo.StationsTraveled.Contains("1"), $"Was : {routeInfo.StationsTraveled}");
            Assert.IsEmpty(routeInfo.Journey);
        }

        [Test]
        public void GetRoute_NightTime_Involving_DT_In_Middle()
        {
            var rawRecords = new List<RawStationData>
            {
                new RawStationData {StationCode = "NE1", StationName = "SengKang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE2", StationName = "Kovan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE3", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE4", StationName = "BoonKeng", OpeningDate = string.Empty},
                new RawStationData {StationCode = "DT1", StationName = "Lorang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "DT2", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "DT3", StationName = "Bishan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "EW1", StationName = "Bugis", OpeningDate = string.Empty},
                new RawStationData {StationCode = "EW2", StationName = "Bishan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "EW3", StationName = "Katib", OpeningDate = string.Empty},
                new RawStationData {StationCode = "EW4", StationName = "Lavender", OpeningDate = string.Empty},
            };

            var map = new Map(rawRecords).LinkStations();

            var start = new Station("Lavender");
            var end = new Station("SengKang");

            ISearchAlgorithm algorithm = new DijkstraSearch();
            var directionService = new DirectionService(algorithm, new InputOption
            {
                Start = start.StationName,
                End = end.StationName,
                StartTime = new DateTime(2021, 3, 5, 23, 40, 00)
            });
            var routeInfo = directionService.PrepareRouteInfoFrom(map);

            Assert.IsTrue(routeInfo.JourneyTitle.Contains(start.StationName), $"Was : {start.StationName}");
            Assert.IsNotEmpty(routeInfo.Route);
            Assert.IsTrue(routeInfo.StationsTraveled.Contains("1"), $"Was : {routeInfo.StationsTraveled}");
            Assert.IsEmpty(routeInfo.Journey);
        }

        [Test]
        public void GetRoute_NightTime_Involving_DT_In_Middle_But_Algorithm_Choose_Alternate_Route()
        {
            var rawRecords = new List<RawStationData>
            {
                new RawStationData {StationCode = "NE1", StationName = "SengKang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE2", StationName = "Kovan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE3", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE4", StationName = "BoonKeng", OpeningDate = string.Empty},
                new RawStationData {StationCode = "DT1", StationName = "Lorang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "DT2", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "DT3", StationName = "Bishan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "EW1", StationName = "Bugis", OpeningDate = string.Empty},
                new RawStationData {StationCode = "EW2", StationName = "Bishan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "EW3", StationName = "Kovan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "EW4", StationName = "Lavender", OpeningDate = string.Empty},
                new RawStationData {StationCode = "KK1", StationName = "Bugis", OpeningDate = string.Empty},
                new RawStationData {StationCode = "KK2", StationName = "Lorang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "KK3", StationName = "Caldecot", OpeningDate = string.Empty},
            };

            var map = new Map(rawRecords).LinkStations();

            var start = new Station("Caldecot");
            var end = new Station("SengKang");

            ISearchAlgorithm algorithm = new DijkstraSearch();
            var directionService = new DirectionService(algorithm, new InputOption
            {
                Start = start.StationName,
                End = end.StationName,
                StartTime = new DateTime(2021, 3, 5, 23, 40, 00)
            });
            var routeInfo = directionService.PrepareRouteInfoFrom(map);

            Assert.IsTrue(routeInfo.JourneyTitle.Contains(start.StationName), $"Was : {start.StationName}");
            Assert.IsNotEmpty(routeInfo.Route);
            Assert.IsTrue(routeInfo.StationsTraveled.Contains("6"), $"Was : {routeInfo.StationsTraveled}");
            Assert.IsNotEmpty(routeInfo.Journey, string.Join(", ", routeInfo.Journey));
        }
    }
}
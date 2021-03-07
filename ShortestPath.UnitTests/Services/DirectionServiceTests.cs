using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;
using Shortest_Path.Services;

namespace ShortestPath.UnitTests.Services
{
    public class DirectionServiceTests
    {
        private Station _sengkangStation;
        private Station _kovanStation;
        private Station _harborStation;
        private Station _bishanStation;

        private Mock<ISearchAlgorithm> _algorithm;


        [SetUp]
        public void Init()
        {
            _sengkangStation = new Station("Sengkang");
            _kovanStation = new Station("Kovan");
            _harborStation = new Station("Harbor");
            _bishanStation = new Station("Bishan");

            _algorithm = new Mock<ISearchAlgorithm>();
            _algorithm.Setup(a => a.FillShortestPath(
                It.IsAny<List<Station>>(),
                It.IsAny<InputOption>())).Returns(new List<Station>
            {
                _kovanStation,
                _sengkangStation
            });
        }

        [Test]
        public void PrepareRouteInfo_ShouldReturn_SingleStation_In_Plan_For_SameStations_As_StartAndEnd()
        {
            _kovanStation.NearestToStart = _sengkangStation;

            var direction = new DirectionService(_algorithm.Object, new InputOption
            {
                Start = _sengkangStation.StationName,
                End = _sengkangStation.StationName
            });
            var routeInfo = direction.PrepareRouteInfoFrom(new Map());

            routeInfo.Journey.Should().BeEmpty()
                .And.HaveCount(0);
        }

        [Test]
        public void PrepareRouteInfo_ShouldReturn_RoutePlan_For_TwoStations()
        {
            _kovanStation.NearestToStart = _sengkangStation;
            _kovanStation.AddLine("NE");
            _sengkangStation.AddLine("NE");

            var direction = new DirectionService(_algorithm.Object, new InputOption
            {
                Start = _sengkangStation.StationName,
                End = _kovanStation.StationName
            });
            var routeInfo = direction.PrepareRouteInfoFrom(new Map());

            routeInfo.Journey.Should().NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainMatch($"*{_sengkangStation.StationName}*")
                .And.ContainMatch($"*{_kovanStation.StationName}*");
        }

        [Test]
        public void PrepareRouteInfo_ShouldReturn_RoutePlan_For_FourStations_In_DiamondShape()
        {
            _harborStation.NearestToStart = _bishanStation;
            _bishanStation.NearestToStart = _sengkangStation;
            _kovanStation.NearestToStart = _sengkangStation;

            _sengkangStation.AddLine("NE");
            _sengkangStation.AddLine("CC");
            _bishanStation.AddLine("CC");
            _kovanStation.AddLine("NE");
            _harborStation.AddLine("NE");
            _harborStation.AddLine("CC");

            _algorithm.Setup(a => a.FillShortestPath(It.IsAny<List<Station>>(), It.IsAny<InputOption>()))
                .Returns(new List<Station>
            {
                _harborStation,
                _kovanStation,
                _bishanStation,
                _sengkangStation,
            });

            var direction = new DirectionService(_algorithm.Object, new InputOption
            {
                Start = _sengkangStation.StationName,
                End = _harborStation.StationName
            });
            var routeInfo = direction.PrepareRouteInfoFrom(new Map());

            routeInfo.Journey.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.ContainMatch($"*{_sengkangStation.StationName}*")
                .And.ContainMatch($"*{_bishanStation.StationName}*")
                .And.ContainMatch($"*{_harborStation.StationName}*");
        }
    }
}
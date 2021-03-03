using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shortest_Path;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests
{
    public class DirectionTests
    {
        private Station _sengkangStation;
        private Station _kovanStation;
        private Station _HarborStation;
        private Station _BishanStation;

        private Mock<ISearchAlgorithm> _algorithm;


        [SetUp]
        public void Init()
        {
            _sengkangStation = new Station("Sengkang");
            _kovanStation = new Station("Kovan");
            _HarborStation = new Station("Harbor");
            _BishanStation = new Station("Bishan");

            _algorithm = new Mock<ISearchAlgorithm>();
        }

        [Test]
        public void PrepareRouteInfo_ShouldReturn_SingleStation_In_Plan_For_SameStations_As_StartAndEnd()
        {
            _kovanStation.NearestToStart = _sengkangStation;

            _algorithm.Setup(a => a.FillShortestPath(It.IsAny<List<Station>>(), It.IsAny<Station>(), It.IsAny<Station>())).Returns(new List<Station>
            {
                _kovanStation,
                _sengkangStation
            });

            var direction = new Direction(_algorithm.Object, _sengkangStation, _sengkangStation);
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

            _algorithm.Setup(a => a.FillShortestPath(It.IsAny<List<Station>>(), It.IsAny<Station>(), It.IsAny<Station>())).Returns(new List<Station>
            {
                _kovanStation,
                _sengkangStation
            });

            var direction = new Direction(_algorithm.Object, _sengkangStation, _kovanStation);
            var routeInfo = direction.PrepareRouteInfoFrom(new Map());

            routeInfo.Journey.Should().NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainMatch($"*{_sengkangStation.StationName}*")
                .And.ContainMatch($"*{_kovanStation.StationName}*");
        }

        [Test]
        public void PrepareRouteInfo_ShouldReturn_RoutePlan_For_FourStations_In_DiamondShape()
        {
            _HarborStation.NearestToStart = _BishanStation;
            _BishanStation.NearestToStart = _sengkangStation;
            _kovanStation.NearestToStart = _sengkangStation;

            _sengkangStation.AddLine("NE");
            _sengkangStation.AddLine("CC");
            _BishanStation.AddLine("CC");
            _kovanStation.AddLine("NE");
            _HarborStation.AddLine("NE");
            _HarborStation.AddLine("CC");

            _algorithm.Setup(a => a.FillShortestPath(It.IsAny<List<Station>>(), It.IsAny<Station>(), It.IsAny<Station>())).Returns(new List<Station>
            {
                _HarborStation,
                _kovanStation,
                _BishanStation,
                _sengkangStation,
            });

            var direction = new Direction(_algorithm.Object, _sengkangStation, _HarborStation);
            var routeInfo = direction.PrepareRouteInfoFrom(new Map());

            routeInfo.Journey.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.ContainMatch($"*{_sengkangStation.StationName}*")
                .And.ContainMatch($"*{_BishanStation.StationName}*")
                .And.ContainMatch($"*{_HarborStation.StationName}*");
        }
    }
}
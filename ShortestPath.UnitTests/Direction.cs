using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class Direction
    {
        private readonly ISearchAlgorithm _searchAlgorithm;
        private readonly Station _start;
        private readonly Station _end;
        public double ShortestPathCost { get; set; }

        public double ShortestPathLength { get; set; }

        public Direction(ISearchAlgorithm searchAlgorithm, Station start, Station end)
        {
            _searchAlgorithm = searchAlgorithm;
            _start = start;
            _end = end;
        }

        public RouteInfo PrepareRouteInfoFrom(Map map)
        {
            var mappedStations = _searchAlgorithm.FillShortestPath(map.Stations, _start, _end);

            var shortestPath = new List<Station>();
            var end = mappedStations.First(a => a == _end);
            shortestPath.Add(end);
            BuildShortestPath(shortestPath, end);
            shortestPath.Reverse();
            return new RouteInfo(shortestPath);
        }

        private void BuildShortestPath(List<Station> stations, Station station)
        {
            if (station.NearestToStart == null)
                return;
            stations.Add(station.NearestToStart);
            var single = station.Connections.Single(x => x.ConnectedStation == station.NearestToStart);

            ShortestPathLength += single.Length;
            ShortestPathCost += single.Cost;

            BuildShortestPath(stations, station.NearestToStart);
        }
    }

    public interface ISearchAlgorithm
    {
        List<Station> FillShortestPath(List<Station> stations, Station start, Station end);
    }

    public class RouteInfo
    {
        public string JourneyTitle;
        public string TravelledStations;
        public string Route;
        public List<string> Journey;

        public RouteInfo(List<Station> shortestPath)
        {
            Journey = shortestPath.Select(a => a.StationName).ToList();
        }
    }

    public class DirectionTests
    {
        private Station _sengkangStation;
        private Station _kovanStation;
        private Station _HarborStation;
        private Station _SerangoonStation;
        private Station _BishanStation;
        private List<Station> _stations;

        private Mock<ISearchAlgorithm> _algorithm;


        [SetUp]
        public void Init()
        {
            _sengkangStation = new Station("Sengkang");
            _kovanStation = new Station("Kovan");
            _HarborStation = new Station("Harbor");
            _SerangoonStation = new Station("Serangoon");
            _BishanStation = new Station("Bishan");
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation,
                _SerangoonStation,
                _HarborStation,
                _BishanStation
            };
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

            Assert.IsTrue(routeInfo.Journey.Contains(_sengkangStation.StationName));
        }

        [Test]
        public void PrepareRouteInfo_ShouldReturn_RoutePlan_For_TwoStations()
        {
            _kovanStation.NearestToStart = _sengkangStation;

            _algorithm.Setup(a => a.FillShortestPath(It.IsAny<List<Station>>(), It.IsAny<Station>(), It.IsAny<Station>())).Returns(new List<Station>
            {
                _kovanStation,
                _sengkangStation
            });

            var direction = new Direction(_algorithm.Object, _sengkangStation, _kovanStation);
            var routeInfo = direction.PrepareRouteInfoFrom(new Map());

            Assert.IsTrue(routeInfo.Journey.Contains(_sengkangStation.StationName), _sengkangStation.StationName);
            Assert.IsTrue(routeInfo.Journey.Contains(_kovanStation.StationName), _kovanStation.StationName);
        }

        [Test]
        public void PrepareRouteInfo_ShouldReturn_RoutePlan_For_FourStations()
        {
            _HarborStation.NearestToStart = _BishanStation;
            _BishanStation.NearestToStart = _sengkangStation;
            _kovanStation.NearestToStart = _sengkangStation;

            _algorithm.Setup(a => a.FillShortestPath(It.IsAny<List<Station>>(), It.IsAny<Station>(), It.IsAny<Station>())).Returns(new List<Station>
            {
                _HarborStation,
                _kovanStation,
                _BishanStation,
                _sengkangStation,
            });

            Direction direction = new Direction(_algorithm.Object, _sengkangStation, _HarborStation);
            var routeInfo = direction.PrepareRouteInfoFrom(new Map());

            Assert.IsTrue(routeInfo.Journey.Contains(_HarborStation.StationName), _HarborStation.StationName);
            Assert.IsTrue(routeInfo.Journey.Contains(_BishanStation.StationName), _BishanStation.StationName);
            Assert.IsTrue(routeInfo.Journey.Contains(_sengkangStation.StationName), _kovanStation.StationName);
        }
    }
}
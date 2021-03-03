using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class Direction
    {
        private readonly Station _start;
        private readonly Station _end;
        public double ShortestPathCost { get; set; }

        public double ShortestPathLength { get; set; }

        public Direction(Station start, Station end)
        {
            _start = start;
            _end = end;
        }

        public RouteInfo PrepareRouteInfoFrom(Map map)
        {
            var mappedStations = new DijkstraSearch().FillShortestPath(map.Stations, _start, _end);

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
        }

        [Test]
        public void PrepareRouteInfo_ShouldReturn_SingleStation_In_Plan_For_SameStations_As_StartAndEnd()
        {
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });

            Direction direction = new Direction(_sengkangStation, _sengkangStation);
            var mrtLines = new Dictionary<string, List<Station>>
            {
                {"NE", new List<Station> { _sengkangStation, _kovanStation}},
            };
            Map map = new Map().LinkStations(_stations, mrtLines);
            var routeInfo = direction.PrepareRouteInfoFrom(map);

            Assert.AreEqual(1, routeInfo.Journey.Count);
        }

        [Test]
        public void PrepareRouteInfo_ShouldReturn_RoutePlan_For_TwoStations()
        {
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });

            Direction direction = new Direction(_sengkangStation, _kovanStation);
            var mrtLines = new Dictionary<string, List<Station>>
            {
                {"NE", new List<Station> { _sengkangStation, _kovanStation}},
            };
            Map map = new Map().LinkStations(_stations, mrtLines);
            var routeInfo = direction.PrepareRouteInfoFrom(map);

            Assert.AreEqual(2, routeInfo.Journey.Count);
        }
    }
}
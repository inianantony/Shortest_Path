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

            BuildShortestPath(stations, station.NearestToStart);
        }
    }

    public class DijkstraSearch
    {
        public List<Station> FillShortestPath(List<Station> stations, Station startStation, Station endStation)
        {
            var start = stations.First(a => a == startStation);
            start.MinCostToStart = 0;
            var priorityQueue = new List<Station> { start };
            do
            {
                priorityQueue = priorityQueue.OrderBy(x => x.MinCostToStart ?? 0).ToList();
                var node = priorityQueue.First();
                priorityQueue.Remove(node);
                foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
                {
                    var childNode = cnn.ConnectedStation;
                    if (childNode.Visited)
                        continue;
                    if (childNode.MinCostToStart == null || node.MinCostToStart + cnn.Cost < childNode.MinCostToStart)
                    {
                        childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
                        childNode.NearestToStart = node;
                        if (!priorityQueue.Contains(childNode))
                            priorityQueue.Add(childNode);
                    }
                }
                node.Visited = true;
                if (node == endStation)
                    return stations;
            } while (priorityQueue.Any());

            return stations;
        }
    }

    public class DijkstraSearchTests
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
        }

        [Test]
        public void Given_2_Stations_NearestToStart_From_EndStation_ShouldBe_Begining_Station()
        {
            _sengkangStation.Connections.Add(new Edge{ConnectedStation = _kovanStation,Cost = 1, Length = 1});
            _kovanStation.Connections.Add(new Edge{ConnectedStation = _sengkangStation,Cost = 1, Length = 1 });
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var path = dijkstraSearch.FillShortestPath(_stations, _sengkangStation, _kovanStation);
            Assert.AreEqual(_sengkangStation, _kovanStation.NearestToStart);
            Assert.AreEqual(null, _sengkangStation.NearestToStart);
        }

        [Test]
        public void Given_3_Stations_We_Can_Trace_To_BeginingStation_FromEndStation()
        {
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _HarborStation, Cost = 1, Length = 1 });
            _HarborStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });

            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var path = dijkstraSearch.FillShortestPath(_stations, _sengkangStation, _kovanStation);
            Assert.AreEqual(_kovanStation, _HarborStation.NearestToStart);
            Assert.AreEqual(_sengkangStation, _kovanStation.NearestToStart);
            Assert.AreEqual(null, _sengkangStation.NearestToStart);
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
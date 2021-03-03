using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class RouteInfo
    {
        private readonly List<Station> _shortestPath;
        private readonly Station _start;
        private readonly Station _end;

        public string JourneyTitle => $"Travel from {_start.StationName} to {_end.StationName}";
        public string StationsTravelled => $"Stations travelled: {_shortestPath.Count}";

        public string Route
        {
            get
            {
                var preIntersect = string.Empty;
                var routes = new List<string>();
                for (int i = 0; i < _shortestPath.Count - 1; i++)
                {
                    var current = _shortestPath[i];
                    var next = _shortestPath[i + 1];
                    var getIntersectingStation = current.Lines.Intersect(next.Lines).First();

                    var switchingLines = preIntersect != getIntersectingStation && preIntersect != string.Empty;
                    if (switchingLines)
                    {
                        routes.Add(current.StationCodes.Find(a => a.StartsWith(preIntersect)));
                    }

                    routes.Add(current.StationCodes.Find(a => a.StartsWith(getIntersectingStation)));

                    var closeToLastStation = i == _shortestPath.Count - 2;
                    if (closeToLastStation)
                    {
                        routes.Add(next.StationCodes.Find(a => a.StartsWith(getIntersectingStation)));
                    }

                    preIntersect = getIntersectingStation;
                }

                return $"Route : ('{string.Join("', '", routes) }')";
            }
        }

        public List<string> Journey
        {
            get
            {
                var preIntersect = string.Empty;
                List<string> routes = new List<string>();
                for (int i = 0; i < _shortestPath.Count - 1; i++)
                {
                    var current = _shortestPath[i];
                    var next = _shortestPath[i + 1];
                    var getIntersectingStation = current.Lines.Intersect(next.Lines).First();
                    if (preIntersect != getIntersectingStation && preIntersect != string.Empty)
                    {
                        routes.Add($"Change from {preIntersect} line to {getIntersectingStation} line");
                    }

                    preIntersect = getIntersectingStation;
                    routes.Add($"Take {getIntersectingStation} line from {current.StationName} to {next.StationName}");
                }

                return routes;
            }
        }

        public RouteInfo(List<Station> shortestPath, Station start, Station end)
        {
            _shortestPath = shortestPath;
            _start = start;
            _end = end;
        }

    }

    public class RouteInfoTests
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
            _sengkangStation.AddStationCode("NE1");
            _sengkangStation.AddLine("NE");

            _kovanStation = new Station("Kovan");
            _kovanStation.AddStationCode("NE2");
            _kovanStation.AddLine("NE");

            _HarborStation = new Station("Harbor");

            _SerangoonStation = new Station("Serangoon");
            _SerangoonStation.AddStationCode("NE3");
            _SerangoonStation.AddLine("NE");
            _SerangoonStation.AddStationCode("CC1");
            _SerangoonStation.AddLine("CC");

            _BishanStation = new Station("Bishan");
            _BishanStation.AddStationCode("CC2");
            _BishanStation.AddLine("CC");
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
        }

        [Test]
        public void JourneyTitle_Shows_Start_And_End()
        {
            var routeInfo = new RouteInfo(_stations, _sengkangStation, _kovanStation);
            Assert.AreEqual($"Travel from {_sengkangStation.StationName} to {_kovanStation.StationName}", routeInfo.JourneyTitle);
        }

        [Test]
        public void StationsTravelled_Shows_Hop_Count()
        {
            var routeInfo = new RouteInfo(_stations, _sengkangStation, _kovanStation);
            Assert.AreEqual("Stations travelled: 2", routeInfo.StationsTravelled);
        }

        [Test]
        public void Route_Shows_StationCodes_InOneLine()
        {
            var routeInfo = new RouteInfo(_stations, _sengkangStation, _kovanStation);
            Assert.AreEqual("Route : ('NE1', 'NE2')", routeInfo.Route);
        }

        [Test]
        public void Route_Shows_All_StationCodes_Of_InterChanges()
        {
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation,
                _SerangoonStation,
                _BishanStation
            };
            var routeInfo = new RouteInfo(_stations, _sengkangStation, _BishanStation);
            Assert.AreEqual("Route : ('NE1', 'NE2', 'NE3', 'CC1', 'CC2')", routeInfo.Route);
        }

        [Test]
        public void Journey_Shows_Description_When_JourneyIs_From_Single_Line()
        {
            var routeInfo = new RouteInfo(_stations, _sengkangStation, _kovanStation);
            var expected = new List<string>
            {
                $"Take NE line from {_sengkangStation.StationName} to {_kovanStation.StationName}"
            };
            routeInfo.Journey.Should().NotBeEmpty()
                .And.HaveCount(1)
                .And.BeEquivalentTo(expected);
        }

        [Test]
        public void Journey_Shows_Description_WithInterChange_Info_When_JourneyIs_From_Multiple_Lines()
        {
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation,
                _SerangoonStation,
                _BishanStation
            };
            var routeInfo = new RouteInfo(_stations, _sengkangStation, _BishanStation);

            var expected = new List<string>
            {
                $"Take NE line from {_sengkangStation.StationName} to {_kovanStation.StationName}",
                $"Take NE line from {_kovanStation.StationName} to {_SerangoonStation.StationName}",
                "Change from NE line to CC line",
                $"Take CC line from {_SerangoonStation.StationName} to {_BishanStation.StationName}",
            };
            routeInfo.Journey.Should().NotBeEmpty()
                .And.HaveCount(4)
                .And.BeEquivalentTo(expected);
        }
    }
}
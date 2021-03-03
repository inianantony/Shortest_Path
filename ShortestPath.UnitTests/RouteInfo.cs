using System.Collections.Generic;
using System.Linq;
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
        public string Route;
        public List<string> Journey;

        public RouteInfo(List<Station> shortestPath, Station start, Station end)
        {
            _shortestPath = shortestPath;
            _start = start;
            _end = end;
            Journey = shortestPath.Select(a => a.StationName).ToList();
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
            _kovanStation = new Station("Kovan");
            _HarborStation = new Station("Harbor");
            _SerangoonStation = new Station("Serangoon");
            _BishanStation = new Station("Bishan");
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
            Assert.AreEqual($"Stations travelled: 2", routeInfo.StationsTravelled);
        }
    }
}
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Models
{
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
        public void StationsTraveled_Shows_Hop_Count()
        {
            var routeInfo = new RouteInfo(_stations, _sengkangStation, _kovanStation);
            Assert.AreEqual("Stations traveled: 2", routeInfo.StationsTraveled);
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
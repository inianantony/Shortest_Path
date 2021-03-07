using System.Collections.Generic;
using ExpectedObjects;
using NUnit.Framework;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Models
{
    public class StationTests
    {
        private Station _sengkangStation;
        private Station _kovanStation;
        private Station _serangoonStation;
        private Station _bishanStation;

        [SetUp]
        public void Init()
        {
            _sengkangStation = new Station("Sengkang");
            _kovanStation = new Station("Kovan");
            _serangoonStation = new Station("Serangoon");
            _bishanStation = new Station("Bishan");
        }

        [Test]
        public void Test_Succesfull_Creation_Of_Station()
        {
            Assert.IsNotNull(_sengkangStation);
        }

        [Test]
        public void Add_Empty_StationCode_ShouldNotHaveAnyImpact()
        {
            _sengkangStation.AddStationCode(null);
            Assert.IsEmpty(_sengkangStation.StationCodes);
        }

        [Test]
        public void Add_LineName_NE_Should_Add_NE_To_Lines()
        {
            _sengkangStation.AddLine("NE");
            var lines = new List<string> { "NE" };
            lines.ToExpectedObject().ShouldMatch(_sengkangStation.Lines);
        }

        [Test]
        public void Add_LineName_NE_Twice_Should_NOT_Add_NE_To_Lines()
        {
            _sengkangStation.AddLine("NE");
            _sengkangStation.AddLine("NE");
            var lines = new List<string> { "NE" };
            lines.ToExpectedObject().ShouldMatch(_sengkangStation.Lines);
        }

        [Test]
        public void Add_StationCode_NE1_Should_Add_NE1_To_StationCodes()
        {
            var station = new Station("Sengkang");
            station.AddStationCode("NE1");
            var stationCodes = new List<string> { "NE1" };
            stationCodes.ToExpectedObject().ShouldMatch(station.StationCodes);
        }

        [Test]
        public void Add_StationCode_NE1_Twice_Should_NOT_Add_NE1_To_StationCodes()
        {
            _sengkangStation.AddStationCode("NE1");
            _sengkangStation.AddStationCode("NE1");
            var stationCodes = new List<string> { "NE1" };
            stationCodes.ToExpectedObject().ShouldMatch(_sengkangStation.StationCodes);
        }

        [Test]
        public void Add_LineName_NE_And_CC_Should_Add_NE_And_CC_To_Lines()
        {
            _sengkangStation.AddLine("NE");
            _sengkangStation.AddLine("CC");
            var lines = new List<string> { "NE", "CC" };
            lines.ToExpectedObject().ShouldMatch(_sengkangStation.Lines);
        }

        [Test]
        public void Add_StationCode_NE1_And_CC1_Should_Add_NE1_And_CC1_To_StationCodes()
        {
            _sengkangStation.AddStationCode("NE1");
            _sengkangStation.AddStationCode("CC1");
            var stationCodes = new List<string> { "NE1", "CC1" };
            stationCodes.ToExpectedObject().ShouldMatch(_sengkangStation.StationCodes);
        }

        [Test]
        public void ConnectNearByStations_Links_First_Station_With_NextStation_On_Same_Line()
        {
            var neLine = new Dictionary<string, List<Station>>
            {
                {"NE", new List<Station> {_sengkangStation, _kovanStation}}
            };
            var connections = _sengkangStation.ConnectNearByStations(neLine).Connections;
            var expectedConnection = new List<Edge>
            {
                new Edge {ConnectedStation = _kovanStation, Cost = 1}
            };
            expectedConnection.ToExpectedObject().ShouldMatch(connections);
        }

        [Test]
        public void ConnectNearByStations_Links_Last_Station_With_PreviousStation_On_Same_Line()
        {
            var neLine = new Dictionary<string, List<Station>>
            {
                {"NE", new List<Station> {_sengkangStation, _kovanStation}}
            };
            var connections = _kovanStation.ConnectNearByStations(neLine).Connections;
            var expectedConnection = new List<Edge>
            {
                new Edge {ConnectedStation = _sengkangStation, Cost = 1}
            };
            expectedConnection.ToExpectedObject().ShouldMatch(connections);
        }

        [Test]
        public void ConnectNearByStations_Links_Middle_Station_With_PreviousStation_And_NextStation_On_Same_Line()
        {
            var neLine = new Dictionary<string, List<Station>>
            {
                {"NE", new List<Station> {_sengkangStation, _serangoonStation, _kovanStation}}
            };
            var connections = _serangoonStation.ConnectNearByStations(neLine).Connections;
            var expectedConnection = new List<Edge>
            {
                new Edge {ConnectedStation = _sengkangStation, Cost = 1},
                new Edge {ConnectedStation = _kovanStation, Cost = 1}
            };
            expectedConnection.ToExpectedObject().ShouldMatch(connections);
        }

        [Test]
        public void ConnectNearByStations_Links_All_Interchanges()
        {
            var mrtLines = new Dictionary<string, List<Station>>
            {
                {"NE", new List<Station> {_sengkangStation, _serangoonStation, _kovanStation}},
                {"CC", new List<Station> {_bishanStation, _serangoonStation}},
            };
            var connections = _serangoonStation.ConnectNearByStations(mrtLines).Connections;
            var expectedConnection = new List<Edge>
            {
                new Edge{ConnectedStation = _sengkangStation,Cost = 1},
                new Edge{ConnectedStation = _kovanStation,Cost = 1},
                new Edge{ConnectedStation = _bishanStation,Cost = 1},
            };
            expectedConnection.ToExpectedObject().ShouldMatch(connections);
        }

        [Test]
        public void IsSameAs_Should_ReturnTrue_IfName_Is_Same()
        {
            Assert.IsTrue(new Station("Kovan").IsSameAs("Kovan"));
            Assert.IsTrue(new Station("Kovan").IsSameAs(new Station("Kovan")));
        }

        [Test]
        public void IsSameAs_Should_ReturnFalse_IfName_Is_Not_Same()
        {
            Assert.IsFalse(new Station("Kovan").IsSameAs("Ubi"));
            Assert.IsFalse(new Station("Kovan").IsSameAs(new Station("Ubi")));
        }
    }
}
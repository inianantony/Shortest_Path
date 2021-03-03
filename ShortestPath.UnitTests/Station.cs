using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class Station : IEqualityComparer<Station>, IComparable<Station>
    {
        public Station(string stationName)
        {
            StationName = stationName;
            Lines = new List<string>();
            StationCodes = new List<string>();
            Connections = new List<Edge>();
        }

        public string StationName { get; }
        public List<string> Lines { get; }
        public List<string> StationCodes { get; }
        public List<Edge> Connections { get; private set; }
        public Station NearestToStart { get; set; }
        public double? MinimumCost { get; set; }
        public bool Visited { get; set; }

        public Station AddStationCode(string stationCode)
        {
            if (string.IsNullOrEmpty(stationCode)) return this;

            if (!StationCodes.Contains(stationCode))
                StationCodes.Add(stationCode);
            return this;
        }

        public Station AddLine(string lineName)
        {
            if (string.IsNullOrEmpty(lineName)) return this;

            if (!Lines.Contains(lineName))
                Lines.Add(lineName);

            return this;
        }

        public Station ConnectNearByStations(List<Station> stations, Dictionary<string, List<Station>> mrtLines)
        {
            var nearbyStations = new List<Station>();
            foreach (var line in mrtLines)
            {
                var nearbyIndices = GetNearbyStationIndices(line);
                nearbyStations.AddRange(nearbyIndices.Select(a => stations.First(b => b.Equals(line.Value[a]))).ToList());
            }

            Connections = nearbyStations.Distinct().Select(a => new Edge { ConnectedStation = a, Cost = 1, Length = 1 }).ToList();

            return this;
        }

        private List<int> GetNearbyStationIndices(KeyValuePair<string, List<Station>> line)
        {
            var nearbyIndices = new List<int>();
            for (var i = 0; i < line.Value.Count; i++)
            {
                if (!StationName.Equals(line.Value[i].StationName)) continue;

                var firstStation = i == 0;
                var lastStation = i == (line.Value.Count - 1);
                if (firstStation)
                {
                    nearbyIndices.Add(i + 1);
                }
                else if (lastStation)
                {
                    nearbyIndices.Add(i - 1);
                }
                else
                {
                    nearbyIndices.Add(i + 1);
                    nearbyIndices.Add(i - 1);
                }
            }

            return nearbyIndices;
        }

        public override int GetHashCode()
        {
            return StationName.GetHashCode();
        }

        public override bool Equals(object? y)
        {
            return Equals(this, y as Station);
        }

        public override string ToString()
        {
            return StationName;
        }

        public int CompareTo(Station other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(StationName, other.StationName, StringComparison.Ordinal);
        }

        public bool Equals(Station x, Station y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.StationName == y.StationName;
        }

        public int GetHashCode(Station obj)
        {
            return GetHashCode();
        }
    }

    public class StationTests
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
            var connections = _sengkangStation.ConnectNearByStations(_stations, neLine).Connections;
            var expectedConnection = new List<Edge>
                    {
                        new Edge{ConnectedStation = _kovanStation,Cost = 1,Length = 1}
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
            var connections = _kovanStation.ConnectNearByStations(_stations, neLine).Connections;
            var expectedConnection = new List<Edge>
            {
                new Edge{ConnectedStation = _sengkangStation,Cost = 1,Length = 1}
            };
            expectedConnection.ToExpectedObject().ShouldMatch(connections);
        }

        [Test]
        public void ConnectNearByStations_Links_Third_Station_With_PreviousStation_And_NextStation_On_Same_Line()
        {
            var neLine = new Dictionary<string, List<Station>>
            {
                {"NE", new List<Station> {_sengkangStation, _SerangoonStation, _kovanStation}}
            };
            var connections = _SerangoonStation.ConnectNearByStations(_stations, neLine).Connections;
            var expectedConnection = new List<Edge>
            {
                new Edge{ConnectedStation = _sengkangStation,Cost = 1,Length = 1},
                new Edge{ConnectedStation = _kovanStation,Cost = 1,Length = 1}
            };
            expectedConnection.ToExpectedObject().ShouldMatch(connections);
        }

        [Test]
        public void ConnectNearByStations_Links_All_Interchanges()
        {
            var neLine = new Dictionary<string, List<Station>>
            {
                {"NE", new List<Station> {_sengkangStation, _SerangoonStation, _kovanStation}},
                {"CC", new List<Station> {_BishanStation, _SerangoonStation}},
            };
            var connections = _SerangoonStation.ConnectNearByStations(_stations, neLine).Connections;
            var expectedConnection = new List<Edge>
            {
                new Edge{ConnectedStation = _sengkangStation,Cost = 1,Length = 1},
                new Edge{ConnectedStation = _kovanStation,Cost = 1,Length = 1},
                new Edge{ConnectedStation = _BishanStation,Cost = 1,Length = 1},
            };
            expectedConnection.ToExpectedObject().ShouldMatch(connections);
        }
    }
}
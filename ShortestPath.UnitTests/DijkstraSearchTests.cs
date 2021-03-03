using System.Collections.Generic;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
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
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
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
            var path = dijkstraSearch.FillShortestPath(_stations, _sengkangStation, _HarborStation);
            Assert.AreEqual(_kovanStation, _HarborStation.NearestToStart);
            Assert.AreEqual(_sengkangStation, _kovanStation.NearestToStart);
            Assert.AreEqual(null, _sengkangStation.NearestToStart);
        }

        [Test]
        public void Scenario_4_Stations_Where_Start_And_End_Is_Same_Then_FirstRouteReached_WillBe_Returned()
        {
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _BishanStation, Cost = 1, Length = 1 });
            _BishanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _BishanStation.Connections.Add(new Edge { ConnectedStation = _HarborStation, Cost = 1, Length = 1 });
            _HarborStation.Connections.Add(new Edge { ConnectedStation = _BishanStation, Cost = 1, Length = 1 });

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
            var path = dijkstraSearch.FillShortestPath(_stations, _sengkangStation, _HarborStation);
            Assert.AreEqual(_BishanStation, _HarborStation.NearestToStart);
            Assert.AreEqual(_sengkangStation, _BishanStation.NearestToStart);
            Assert.AreEqual(null, _sengkangStation.NearestToStart);
        }

        [Test]
        public void Scenario_4_Stations_Where_Start_And_End_Is_Same_Then_LowestCostRouteReached_WillBe_Returned()
        {
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _BishanStation, Cost = 1, Length = 1 });
            _BishanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _BishanStation.Connections.Add(new Edge { ConnectedStation = _HarborStation, Cost = 1, Length = 1 });
            _HarborStation.Connections.Add(new Edge { ConnectedStation = _BishanStation, Cost = 1, Length = 1 });

            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _HarborStation, Cost = 0.5, Length = 1 });
            _HarborStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 0.5, Length = 1 });

            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var path = dijkstraSearch.FillShortestPath(_stations, _sengkangStation, _HarborStation);
            Assert.AreEqual(_kovanStation, _HarborStation.NearestToStart);
            Assert.AreEqual(_sengkangStation, _kovanStation.NearestToStart);
            Assert.AreEqual(null, _sengkangStation.NearestToStart);
        }
    }
}
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Shortest_Path.Algorithm;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Algorithm
{
    public class DijkstraSearchTests
    {
        private Station _sengkangStation;
        private Station _kovanStation;
        private Station _harborStation;
        private Station _bishanStation;
        private List<Station> _stations;

        [SetUp]
        public void Init()
        {
            _sengkangStation = new Station("Sengkang");
            _kovanStation = new Station("Kovan");
            _harborStation = new Station("Harbor");
            _bishanStation = new Station("Bishan");
        }

        [Test]
        public void Given_2_Stations_The_Begining_Station_Should_Be_The_Nearest_Station_For_End_Station()
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

            var expected = new List<Station>
            {
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = 1},
                new Station("Sengkang") {NearestToStart = null, MinimumCost = 0},
            };

            path.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.MinimumCost)
                    .Including(a => a.NearestToStart));
        }

        [Test]
        public void Given_3_Stations_We_Can_Trace_To_BeginingStation_FromEndStation()
        {
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _harborStation, Cost = 1, Length = 1 });
            _harborStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });

            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation,
                _harborStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var path = dijkstraSearch.FillShortestPath(_stations, _sengkangStation, _harborStation);

            var expected = new List<Station>
            {
                new Station("Harbor") {NearestToStart = _kovanStation, MinimumCost = 2},
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = 1},
                new Station("Sengkang") {NearestToStart = null, MinimumCost = 0},
            };

            path.Should().NotBeEmpty()
                .And.HaveCount(3)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.MinimumCost)
                    .Including(a => a.NearestToStart));
        }

        [Test]
        public void Scenario_4_Stations_Where_Start_And_End_Is_Same_Then_FirstRouteReached_WillBe_Returned()
        {
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _bishanStation, Cost = 1, Length = 1 });
            _bishanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _bishanStation.Connections.Add(new Edge { ConnectedStation = _harborStation, Cost = 1, Length = 1 });
            _harborStation.Connections.Add(new Edge { ConnectedStation = _bishanStation, Cost = 1, Length = 1 });

            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _harborStation, Cost = 1, Length = 1 });
            _harborStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });

            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation,
                _bishanStation,
                _harborStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var path = dijkstraSearch.FillShortestPath(_stations, _sengkangStation, _harborStation);

            var expected = new List<Station>
            {
                new Station("Harbor") {NearestToStart = _bishanStation, MinimumCost = 2},
                new Station("Bishan") {NearestToStart = _sengkangStation, MinimumCost = 1},
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = 1},
                new Station("Sengkang") {NearestToStart = null, MinimumCost = 0},
            };

            path.Should().NotBeEmpty()
                .And.HaveCount(4)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.MinimumCost)
                    .Including(a => a.NearestToStart));
        }

        [Test]
        public void Scenario_4_Stations_Where_Start_And_End_Is_Same_Then_LowestCostRouteReached_WillBe_Returned()
        {
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _bishanStation, Cost = 1, Length = 1 });
            _bishanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _bishanStation.Connections.Add(new Edge { ConnectedStation = _harborStation, Cost = 1, Length = 1 });
            _harborStation.Connections.Add(new Edge { ConnectedStation = _bishanStation, Cost = 1, Length = 1 });

            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _harborStation, Cost = 0.5, Length = 1 });
            _harborStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 0.5, Length = 1 });

            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation,
                _bishanStation,
                _harborStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var path = dijkstraSearch.FillShortestPath(_stations, _sengkangStation, _harborStation);

            var expected = new List<Station>
            {
                new Station("Harbor") {NearestToStart = _kovanStation, MinimumCost = 1.5},
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = 1},
                new Station("Bishan") {NearestToStart = _sengkangStation, MinimumCost = 1},
                new Station("Sengkang") {NearestToStart = null, MinimumCost = 0},
            };

            path.Should().NotBeEmpty()
                .And.HaveCount(4)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.MinimumCost)
                    .Including(a => a.NearestToStart));
        }
    }
}
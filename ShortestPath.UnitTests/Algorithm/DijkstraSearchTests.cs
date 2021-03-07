using System;
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
        private Station _ubiStation;
        private Station _tuasStation;
        private List<Station> _stations;

        [SetUp]
        public void Init()
        {
            _sengkangStation = new Station("Sengkang");
            _kovanStation = new Station("Kovan");
            _harborStation = new Station("Harbor");
            _bishanStation = new Station("Bishan");
            _ubiStation = new Station("Ubi");
            _tuasStation = new Station("Tuas");
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
            var option = new Options { Start = _sengkangStation.StationName, End = _kovanStation.StationName };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

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
            var option = new Options { Start = _sengkangStation.StationName, End = _harborStation.StationName };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

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
            var option = new Options { Start = _sengkangStation.StationName, End = _harborStation.StationName };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

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
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _harborStation, Cost = 0.5m, Length = 1 });
            _harborStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 0.5m, Length = 1 });

            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation,
                _bishanStation,
                _harborStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var option = new Options { Start = _sengkangStation.StationName, End = _harborStation.StationName };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

            var expected = new List<Station>
            {
                new Station("Harbor") {NearestToStart = _kovanStation, MinimumCost = 1.5m},
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

        [TestCase("NE", 13)]
        [TestCase("NS", 13)]
        [TestCase("CC", 11)]
        public void Scenario_User_Journey_Falls_In_Line_During_PeakTime_WeekDays_Costs_More(string line, int cost)
        {
            _sengkangStation.AddLine(line);
            _kovanStation.AddLine(line);
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var option = new Options
            {
                Start = _sengkangStation.StationName,
                End = _kovanStation.StationName,
                StartTime = new DateTime(2019, 01, 31, 8, 00, 00)
            };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

            var expected = new List<Station>
            {
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = cost},
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
        public void Scenario_User_Journey_Needs_Interchange_Costs_15_More()
        {
            _sengkangStation.AddLine("NE");
            _kovanStation.AddLine("CC");
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var option = new Options
            {
                Start = _sengkangStation.StationName,
                End = _kovanStation.StationName,
                StartTime = new DateTime(2019, 01, 31, 8, 00, 00)
            };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

            var expected = new List<Station>
            {
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = 16},
                new Station("Sengkang") {NearestToStart = null, MinimumCost = 0},
            };

            path.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.MinimumCost)
                    .Including(a => a.NearestToStart));
        }

        [TestCase("DT")]
        [TestCase("CG")]
        [TestCase("CE")]
        public void Scenario_User_Cant_Take_DT_CG_CE_Lines_At_Night(string line)
        {
            _sengkangStation.AddLine(line);
            _kovanStation.AddLine(line);
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var option = new Options
            {
                Start = _sengkangStation.StationName,
                End = _kovanStation.StationName,
                StartTime = new DateTime(2019, 01, 31, 22, 00, 00)
            };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

            var expected = new List<Station>
            {
                new Station("Sengkang") {NearestToStart = null, MinimumCost = 0},
                new Station("Kovan") {NearestToStart = null, MinimumCost = null},
            };

            path.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.MinimumCost)
                    .Including(a => a.NearestToStart));
        }

        [TestCase("DT")]
        [TestCase("CG")]
        [TestCase("CE")]
        public void Scenario_User_Cant_Journey_When_DT_CG_CE_Comes_As_Interchanges_At_Night(string line)
        {
            _sengkangStation.AddLine("NE");
            _bishanStation.AddLine("NE");
            _bishanStation.AddLine(line);
            _kovanStation.AddLine(line);
            _tuasStation.AddLine(line);
            _tuasStation.AddLine("CC");
            _ubiStation.AddLine("CC");
            _harborStation.AddLine("CC");
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _bishanStation, Cost = 1, Length = 1 });
            _bishanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _bishanStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _bishanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _tuasStation, Cost = 1, Length = 1 });
            _tuasStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _tuasStation.Connections.Add(new Edge { ConnectedStation = _ubiStation, Cost = 1, Length = 1 });
            _ubiStation.Connections.Add(new Edge { ConnectedStation = _tuasStation, Cost = 1, Length = 1 });
            _ubiStation.Connections.Add(new Edge { ConnectedStation = _harborStation, Cost = 1, Length = 1 });
            _harborStation.Connections.Add(new Edge { ConnectedStation = _ubiStation, Cost = 1, Length = 1 });
            _stations = new List<Station>
            {
                _sengkangStation,
                _bishanStation,
                _kovanStation,
                _tuasStation,
                _ubiStation,
                _harborStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var option = new Options
            {
                Start = _sengkangStation.StationName,
                End = _harborStation.StationName,
                StartTime = new DateTime(2019, 01, 31, 22, 00, 00)
            };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

            var expected = new List<Station>
            {
                new Station("Sengkang") {NearestToStart = null, MinimumCost = 0},
                new Station("Bishan") {NearestToStart = _sengkangStation, MinimumCost = 11},
                new Station("Kovan") {NearestToStart = null, MinimumCost = null},
                new Station("Tuas") {NearestToStart = null, MinimumCost = null},
                new Station("Ubi") {NearestToStart = null, MinimumCost = null},
                new Station("Harbor") {NearestToStart = null, MinimumCost = null},
            };

            path.Should().NotBeEmpty()
                .And.HaveCount(6)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.MinimumCost)
                    .Including(a => a.NearestToStart)
                );
        }

        [TestCase("TE", 9)]
        [TestCase("CC", 11)]
        public void Scenario_User_Journey_On_TE_Line_At_Night_Costs_8_More(string line, int cost)
        {
            _sengkangStation.AddLine(line);
            _kovanStation.AddLine(line);
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var option = new Options
            {
                Start = _sengkangStation.StationName,
                End = _kovanStation.StationName,
                StartTime = new DateTime(2019, 01, 31, 16, 00, 00)
            };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

            var expected = new List<Station>
            {
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = cost},
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
        public void Scenario_User_Journey_Needs_Interchange_At_Night_Costs_10_More()
        {
            _sengkangStation.AddLine("NE");
            _kovanStation.AddLine("CC");
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var option = new Options
            {
                Start = _sengkangStation.StationName,
                End = _kovanStation.StationName,
                StartTime = new DateTime(2019, 01, 31, 22, 00, 00)
            };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

            var expected = new List<Station>
            {
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = 11},
                new Station("Sengkang") {NearestToStart = null, MinimumCost = 0},
            };

            path.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.MinimumCost)
                    .Including(a => a.NearestToStart));
        }

        [TestCase("DT", 9)]
        [TestCase("TE", 9)]
        [TestCase("NE", 11)]
        public void Scenario_User_Journey_On_DT_And_TE_Line_At_Other_Times_Costs_8_More(string line, int cost)
        {
            _sengkangStation.AddLine(line);
            _kovanStation.AddLine(line);
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var option = new Options
            {
                Start = _sengkangStation.StationName,
                End = _kovanStation.StationName,
                StartTime = new DateTime(2019, 01, 31, 21, 30, 00)
            };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

            var expected = new List<Station>
            {
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = cost},
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
        public void Scenario_User_Journey_Needs_Interchange_At_Other_Times_Costs_10_More()
        {
            _sengkangStation.AddLine("NE");
            _kovanStation.AddLine("CC");
            _sengkangStation.Connections.Add(new Edge { ConnectedStation = _kovanStation, Cost = 1, Length = 1 });
            _kovanStation.Connections.Add(new Edge { ConnectedStation = _sengkangStation, Cost = 1, Length = 1 });
            _stations = new List<Station>
            {
                _sengkangStation,
                _kovanStation
            };
            var dijkstraSearch = new DijkstraSearch();
            var option = new Options
            {
                Start = _sengkangStation.StationName,
                End = _kovanStation.StationName,
                StartTime = new DateTime(2019, 01, 31, 21, 30, 00)
            };
            var path = dijkstraSearch.FillShortestPath(_stations, option);

            var expected = new List<Station>
            {
                new Station("Kovan") {NearestToStart = _sengkangStation, MinimumCost = 11},
                new Station("Sengkang") {NearestToStart = null, MinimumCost = 0},
            };

            path.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.MinimumCost)
                    .Including(a => a.NearestToStart));
        }

    }
}
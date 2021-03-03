using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using ExpectedObjects;
using FluentAssertions;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class RawStationConvertor
    {
        public List<Station> Convert(List<RawStationData> rawRecords)
        {
            var stations = new List<Station>();
            foreach (var rawStationData in rawRecords)
            {
                var station = new Station(rawStationData.StationName);
                if (stations.Exists(a => a.StationName == rawStationData.StationName))
                {
                    station = stations.First(a => a.StationName == rawStationData.StationName);
                }
                else
                {
                    stations.Add(station);
                }

                station.AddStationCode(rawStationData.StationCode);
                station.AddLine(rawStationData.Line);
            }
            return stations;
        }

        public Dictionary<string, List<Station>> GroupStationsByLines(List<RawStationData> rawRecords,
            List<Station> stations)
        {
            return rawRecords.GroupBy(
                    a => a.Line,
                    b => stations.First(c => c.StationName.Equals(b.StationName)))
                .ToDictionary(
                    a => a.Key,
                    b => b.ToList());
        }
    }

    public class RawStationToStationConvertorTests
    {
        private RawStationConvertor _rawStationConvertor;

        [SetUp]
        public void Init()
        {
            _rawStationConvertor = new RawStationConvertor();
        }

        [Test]
        public void Convert_Returns_Empty_Stations_When_Empty_Raw_Data_Is_Passed()
        {

            var stations = _rawStationConvertor.Convert(new List<RawStationData>());
            Assert.IsEmpty(stations);
        }

        [Test]
        public void Convert_Returns_Correct_Station_When_One_Raw_Data_Is_Passed()
        {
            //Arrange
            var stationName = "Sengkang";
            var stationCode = "NE1";

            //Act
            var stations = _rawStationConvertor.Convert(new List<RawStationData>
            {
                new RawStationData
                {
                    StationCode = stationCode, StationName = stationName
                }
            });

            //Assert
            Assert.AreEqual(1, stations.Count);

            var station = stations.First();
            Assert.AreEqual(stationName, station.StationName);

            var stationCodes = new List<string> { "NE1" };
            stationCodes.ToExpectedObject().ShouldMatch(station.StationCodes);

            var lines = new List<string> { "NE" };
            lines.ToExpectedObject().ShouldMatch(station.Lines);
        }

        [Test]
        public void Convert_AddsLine_And_StationCode_To_ExistingStation()
        {
            //Arrange
            var stationName = "Sengkang";

            //Act
            var stations = _rawStationConvertor.Convert(new List<RawStationData>
            {
                new RawStationData
                {
                    StationCode = "NE1", StationName = stationName
                },
                new RawStationData
                {
                    StationCode = "CC1", StationName = stationName
                }
            });

            var expected = new List<Station>
            {
                new Station(stationName)
                {
                    Lines = {"NE", "CC"},
                    StationCodes = {"NE1", "CC1"}
                }
            };

            stations.Should().NotBeEmpty()
                .And.HaveCount(1)
                .And.BeEquivalentTo(expected, options => options
                    .Including(o => o.StationName)
                    .Including(o => o.Lines)
                    .Including(a => a.StationCodes));
        }

        [Test]
        public void GroupStationsByLines_Returns_EmptyGroup_When_EmptyData_Is_Passed()
        {
            var mrtLines = _rawStationConvertor.GroupStationsByLines(new List<RawStationData>(), new List<Station>());
            Assert.IsEmpty(mrtLines);
        }

        [Test]
        public void GroupStationsByLines_Returns_Mrt_Lines_When_StationInfo_Is_Passed()
        {
            var sengkang = new Station("Sengkang");
            var serangoon = new Station("Serangoon");
            var mrtLines = _rawStationConvertor.GroupStationsByLines(new List<RawStationData>
            {
                new RawStationData {StationName = "Sengkang", StationCode = "NE1"},
                new RawStationData {StationName = "Serangoon", StationCode = "NE21"},
                new RawStationData {StationName = "Serangoon", StationCode = "CC1"},
            }, new List<Station>
            {
                sengkang, serangoon
            });
            var expected = new Dictionary<string, List<Station>>
            {
                {"NE",new List<Station>{sengkang,serangoon}},
                {"CC",new List<Station>{serangoon}}
            };
            expected.ToExpectedObject().ShouldMatch(mrtLines);
        }

    }
}

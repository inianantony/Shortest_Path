using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Shortest_Path.Mapper;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Mapper
{
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
                new RawStationData {StationCode = stationCode, StationName = stationName}
            });

            var expected = new List<Station>
            {
                new Station(stationName) {StationCodes = {"NE1"}, Lines = {"NE"}}
            };

            expected.Should().NotBeEmpty()
                .And.HaveCount(1)
                .And.BeEquivalentTo(stations);
        }

        [Test]
        public void Convert_AddsLine_And_StationCode_To_ExistingStation()
        {
            //Arrange
            var stationName = "Sengkang";

            //Act
            var stations = _rawStationConvertor.Convert(new List<RawStationData>
            {
                new RawStationData {StationCode = "NE1", StationName = stationName},
                new RawStationData {StationCode = "CC1", StationName = stationName}
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
                {"NE", new List<Station> {sengkang, serangoon}},
                {"CC", new List<Station> {serangoon}}
            };

            mrtLines.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(expected);
        }

    }
}
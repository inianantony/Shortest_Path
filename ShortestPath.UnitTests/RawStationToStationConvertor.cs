using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class RawStationToStationConvertor
    {
        public List<Station> Convert(List<RawStationData> rawRecords)
        {
            return new List<Station>();
        }
    }

    public class RawStationToStationConvertorTests
    {
        private RawStationToStationConvertor _rawStationToStationConvertor;

        [SetUp]
        public void Init()
        {
            _rawStationToStationConvertor = new RawStationToStationConvertor();
        }

        [Test]
        public void Returns_Empty_Stations_When_Empty_Raw_Data_Is_Passed()
        {

            var stations = _rawStationToStationConvertor.Convert(new List<RawStationData>());
            Assert.IsEmpty(stations);
        }

        [Test]
        public void Returns_Correct_Station_When_One_Raw_Data_Is_Passed()
        {
            var stationName = "Sengkang";
            var stationCode = "NE1";
            var stations = _rawStationToStationConvertor.Convert(new List<RawStationData>
            {
                new RawStationData
                {
                    StationCode = stationCode, StationName = stationName
                }
            });

            var stationCodes = new List<string> { "NE1" };
            var lines = new List<string> { "NE" };
            Assert.AreEqual(1, stations.Count);
            var station = stations.First();
            Assert.AreEqual(stationName, station.StationName);
            stationCodes.ToExpectedObject().ShouldMatch(station.StationCodes);
            lines.ToExpectedObject().ShouldMatch(station.Lines);
        }
    }
}

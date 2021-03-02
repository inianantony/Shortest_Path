using System.Collections.Generic;
using ExpectedObjects;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class Station
    {
        public Station(string stationName)
        {
            StationName = stationName;
            Lines = new List<string>();
            StationCodes = new List<string>();
        }

        public string StationName { get; set; }
        public List<string> Lines { get; }
        public List<string> StationCodes { get; }

        public void AddStationCode(string stationCode)
        {
            var lineName = stationCode.Substring(0, 2);
            if (!Lines.Contains(lineName))
                Lines.Add(lineName);

            if(!StationCodes.Contains(stationCode))
                StationCodes.Add(stationCode);
        }
    }

    public class StationTests
    {
        [Test]
        public void Test_Succesfull_Creation_Of_Station()
        {
            var station = new Station("Sengkang");
            Assert.IsNotNull(station);
        }

        [Test]
        public void Add_StationCode_NE1_Should_Add__NE_To_Lines()
        {
            var station = new Station("Sengkang");
            station.AddStationCode("NE1");
            var lines = new List<string> { "NE" };
            lines.ToExpectedObject().ShouldMatch(station.Lines);
        }

        [Test]
        public void Add_StationCode_NE1_Twice_Should_NOT_Add__NE_To_Lines()
        {
            var station = new Station("Sengkang");
            station.AddStationCode("NE1");
            station.AddStationCode("NE1");
            var lines = new List<string> { "NE" };
            lines.ToExpectedObject().ShouldMatch(station.Lines);
        }

        [Test]
        public void Add_StationCode_NE1_Should_Add__NE1_To_StationCodes()
        {
            var station = new Station("Sengkang");
            station.AddStationCode("NE1");
            var lines = new List<string> { "NE" };
            lines.ToExpectedObject().ShouldMatch(station.Lines);
        }

        [Test]
        public void Add_StationCode_NE1_Twice_Should_NOT_Add__NE1_To_StationCodes()
        {
            var station = new Station("Sengkang");
            station.AddStationCode("NE1");
            station.AddStationCode("NE1");
            var lines = new List<string> { "NE" };
            lines.ToExpectedObject().ShouldMatch(station.Lines);
        }
    }
}
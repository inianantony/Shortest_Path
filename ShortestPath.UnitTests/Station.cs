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
        public List<Edge> Connections { get; set; }

        public Station AddStationCode(string stationCode)
        {
            if(string.IsNullOrEmpty(stationCode)) return this;

            if(!StationCodes.Contains(stationCode))
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
        public void Add_Empty_StationCode_ShouldNotHaveAnyImpact()
        {
            var station = new Station("Sengkang");
            station.AddStationCode(null);
            Assert.IsEmpty(station.StationCodes);
        }

        [Test]
        public void Add_LineName_NE_Should_Add_NE_To_Lines()
        {
            var station = new Station("Sengkang");
            station.AddLine("NE");
            var lines = new List<string> { "NE" };
            lines.ToExpectedObject().ShouldMatch(station.Lines);
        }

        [Test]
        public void Add_LineName_NE_Twice_Should_NOT_Add_NE_To_Lines()
        {
            var station = new Station("Sengkang");
            station.AddLine("NE");
            station.AddLine("NE");
            var lines = new List<string> { "NE" };
            lines.ToExpectedObject().ShouldMatch(station.Lines);
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
            var station = new Station("Sengkang");
            station.AddStationCode("NE1");
            station.AddStationCode("NE1");
            var stationCodes = new List<string> { "NE1" };
            stationCodes.ToExpectedObject().ShouldMatch(station.StationCodes);
        }

        [Test]
        public void Add_LineName_NE_And_CC_Should_Add_NE_And_CC_To_Lines()
        {
            var station = new Station("Sengkang");
            station.AddLine("NE");
            station.AddLine("CC");
            var lines = new List<string> { "NE", "CC" };
            lines.ToExpectedObject().ShouldMatch(station.Lines);
        }

        [Test]
        public void Add_StationCode_NE1_And_CC1_Should_Add_NE1_And_CC1_To_StationCodes()
        {
            var station = new Station("Sengkang");
            station.AddStationCode("NE1");
            station.AddStationCode("CC1");
            var stationCodes = new List<string> { "NE1", "CC1" };
            stationCodes.ToExpectedObject().ShouldMatch(station.StationCodes);
        }
    }
}
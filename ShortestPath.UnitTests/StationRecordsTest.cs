using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    [TestFixture]
    public class StationRecordsTest
    {
        [Test]
        public void GetMap_Should_Return_Map_Linking_Two_Stations()
        {
            var stations = new List<Station>();
            var sengKangStation = new Station("Sengkang");
            sengKangStation.AddStationCode("NE1");

            var kovanStation = new Station("Sengkang");
            kovanStation.AddStationCode("NE2");

            stations.Add(sengKangStation);
            stations.Add(kovanStation);

            StationRecords records = new StationRecords(stations);
            var linkedStations = records.LinkStations(new List<Station>(), new Dictionary<string, List<Station>>());

            var sengkangConnection = linkedStations.Find(a => a.StationName == sengKangStation.StationName).Connections;
            var kovanConnection = linkedStations.Find(a => a.StationName == kovanStation.StationName).Connections;

            Assert.AreEqual(1, sengkangConnection.Count);
            Assert.AreEqual(kovanStation.StationName, sengkangConnection.First().ConnectedStation.StationName);

            Assert.AreEqual(1, kovanConnection.Count);
            Assert.AreEqual(sengKangStation.StationName, kovanConnection.First().ConnectedStation.StationName);
        }
    }

    public class Edge
    {
        public double Length { get; set; }
        public double Cost { get; set; }
        public Station ConnectedStation { get; set; }

        public override string ToString()
        {
            return "-> " + ConnectedStation;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using NUnit.Framework;
using Shortest_Path;

namespace ShortestPath.UnitTests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void LinkStations_Should_Linking_Two_Stations()
        {
            var stations = new List<Station>();
            var sengKangStation = new Station("Sengkang");

            var kovanStation = new Station("Kovan");

            stations.Add(sengKangStation);
            stations.Add(kovanStation);

            var neLine = new Dictionary<string, List<Station>>
            {
                {"NE", new List<Station> { sengKangStation, kovanStation}},
            };
            var expectedConnection = new List<List<Edge>>
            {
                new List<Edge>
                {
                    new Edge {ConnectedStation = kovanStation, Cost = 1, Length = 1}
                },
                new List<Edge>
                {
                    new Edge {ConnectedStation = sengKangStation, Cost = 1, Length = 1}
                }
            };

            //Act
            var map = new Map().LinkStations(stations, neLine);

            //Assert
            var actualConnections = map.Stations.Select(a => a.Connections).ToList();
            expectedConnection.ToExpectedObject().ShouldMatch(actualConnections);
        }
    }
}
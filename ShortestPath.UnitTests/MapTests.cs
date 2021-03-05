using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using FluentAssertions;
using NUnit.Framework;
using Shortest_Path;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void LinkStations_Should_Linking_Two_Stations()
        {
            //Arrange
            var sengkang = "Sengkang";
            var kovan = "Kovan";
            var rawRecords = new List<RawStationData>
            {
                new RawStationData {StationCode = "NE1", StationName = sengkang, OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE2", StationName = kovan, OpeningDate = string.Empty},
            };

            //Act
            var map = new Map(rawRecords).LinkStations();

            //Assert
            var expectedConnection = new List<List<Edge>>
            {
                new List<Edge> {new Edge {ConnectedStation = map.Stations.First(a=>a.IsSameAs(kovan)), Cost = 1, Length = 1}},
                new List<Edge> {new Edge {ConnectedStation = map.Stations.First(a=>a.IsSameAs(sengkang)), Cost = 1, Length = 1}}
            };
            var actualConnections = map.Stations.Select(a => a.Connections).ToList();
            actualConnections.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(expectedConnection, options => options
                    .WithStrictOrdering()
                    .IgnoringCyclicReferences()
                    .ExcludingMissingMembers()
                    .ExcludingNestedObjects());
        }
    }
}
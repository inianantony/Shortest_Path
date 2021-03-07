using System;
using NUnit.Framework;
using Shortest_Path.Algorithm.CostCalculator;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Algorithm.CostCalculator
{
    class PeakHourInNeNsTests
    {
        [TestCase("NE")]
        [TestCase("NS")]
        public void GetCost_Should_Return_12_Plus_Base_Edge_Cost(string line)
        {
            var options = new InputOption { StartTime = new DateTime(2021, 3, 5, 20, 00, 0) };
            var connectedStation = new Station("A");
            connectedStation.AddLine(line);

            var currentStation = new Station("B");
            currentStation.AddLine(line);

            var edge = new Edge { Cost = 1, ConnectedStation = connectedStation };
            var costCalculator = new PeakHourInNeNs(new BaseCostCalculator());
            Assert.AreEqual(13, costCalculator.GetCost(options, edge, currentStation));
        }
    }
}
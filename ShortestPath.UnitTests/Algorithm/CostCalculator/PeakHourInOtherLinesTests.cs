using System;
using NUnit.Framework;
using Shortest_Path.Algorithm.CostCalculator;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Algorithm.CostCalculator
{
    class PeakHourInOtherLinesTests
    {
        [TestCase("CC")]
        public void GetCost_Should_Return_10_Plus_Base_Edge_Cost(string line)
        {
            var options = new Options { StartTime = new DateTime(2021, 3, 5, 20, 00, 0) };
            var connectedStation = new Station("A");
            connectedStation.AddLine(line);

            var currentStation = new Station("B");
            currentStation.AddLine(line);

            var edge = new Edge { Cost = 1, ConnectedStation = connectedStation };
            var costCalculator = new PeakHourInOtherLines(new BaseCostCalculator());
            Assert.AreEqual(11, costCalculator.GetCost(options, edge, currentStation));
        }
    }
}
using System;
using NUnit.Framework;
using Shortest_Path.Algorithm.CostCalculator;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Algorithm.CostCalculator
{
    class NonPeakInDtTeTests
    {
        [TestCase("DT")]
        [TestCase("TE")]
        public void GetCost_Should_Return_8_Plus_Base_Edge_Cost(string line)
        {
            var options = new Options { StartTime = new DateTime(2021, 3, 5, 21, 30, 0) };
            var connectedStation = new Station("A");
            connectedStation.AddLine(line);

            var currentStation = new Station("B");
            currentStation.AddLine(line);

            var edge = new Edge { Cost = 1, ConnectedStation = connectedStation };
            var costCalculator = new NonPeakInDtTe(new BaseCostCalculator());
            Assert.AreEqual(9, costCalculator.GetCost(options, edge, currentStation));
        }
    }
}
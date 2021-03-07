using System;
using NUnit.Framework;
using Shortest_Path.Algorithm.CostCalculator;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Algorithm.CostCalculator
{
    class InterchangingAtPeakHourTests
    {
        [Test]
        public void GetCost_Should_Return_15_Plus_Base_Edge_Cost()
        {
            var options = new InputOption { StartTime = new DateTime(2021, 3, 5, 19, 30, 0) };
            var connectedStation = new Station("A");
            connectedStation.AddLine("NE");

            var currentStation = new Station("B");
            currentStation.AddLine("CC");

            var edge = new Edge { Cost = 1, ConnectedStation = connectedStation };
            var costCalculator = new InterchangingAtPeakHour(new BaseCostCalculator());
            Assert.AreEqual(16, costCalculator.GetCost(options, edge, currentStation));
        }
    }
}
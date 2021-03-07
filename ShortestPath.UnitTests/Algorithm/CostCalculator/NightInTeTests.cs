using System;
using NUnit.Framework;
using Shortest_Path.Algorithm.CostCalculator;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Algorithm.CostCalculator
{
    class NightInTeTests
    {
        [Test]
        public void GetCost_Should_Return_8_Plus_Base_Edge_Cost()
        {
            var options = new InputOption { StartTime = new DateTime(2021, 3, 5, 23, 30, 0) };
            var connectedStation = new Station("A");
            connectedStation.AddLine("TE");

            var currentStation = new Station("B");
            currentStation.AddLine("TE");

            var edge = new Edge { Cost = 1, ConnectedStation = connectedStation };
            var costCalculator = new NightInTe(new BaseCostCalculator());
            Assert.AreEqual(9, costCalculator.GetCost(options, edge, currentStation));
        }
    }
}
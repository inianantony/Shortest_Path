﻿using System;
using NUnit.Framework;
using Shortest_Path.Algorithm.CostCalculator;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Algorithm.CostCalculator
{
    class NonPeakInAllLinesTests
    {
        [Test]
        public void GetCost_Should_Return_10_Plus_Base_Edge_Cost()
        {
            var options = new InputOption { StartTime = new DateTime(2021, 3, 5, 21, 30, 0) };
            var connectedStation = new Station("A");
            connectedStation.AddLine("NE");

            var currentStation = new Station("B");
            currentStation.AddLine("NE");

            var edge = new Edge { Cost = 1, ConnectedStation = connectedStation };
            var costCalculator = new NonPeakInAllLines(new BaseCostCalculator());
            Assert.AreEqual(11, costCalculator.GetCost(options, edge, currentStation));
        }
    }
}
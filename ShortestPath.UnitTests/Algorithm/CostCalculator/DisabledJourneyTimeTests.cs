using NUnit.Framework;
using Shortest_Path.Algorithm.CostCalculator;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Algorithm.CostCalculator
{
    class DisabledJourneyTimeTests
    {
        [Test]
        public void GetCost_Should_Return_Edge_Cost()
        {
            var cost = 1;
            Assert.AreEqual(cost, new DisabledJourneyTime(null).GetCost(new InputOption(), new Edge { Cost = cost }, null));
        }
    }
}

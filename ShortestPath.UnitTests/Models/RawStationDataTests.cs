using NUnit.Framework;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Models
{
    public class RawStationDataTests
    {
        [Test]
        public void Line_ShouldReturn_First_Two_Characters_Of_StationCode()
        {
            Assert.AreEqual("NE", new RawStationData { StationCode = "NE1" }.Line);
        }
    }
}
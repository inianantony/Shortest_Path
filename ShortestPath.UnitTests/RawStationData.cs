using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class RawStationData
    {
        public string StationCode { get; set; }
        public string StationName { get; set; }
        public object OpeningDate { get; set; }

        public string Line => StationCode.Substring(0, 2);
    }

    public class RawStationDataTest
    {
        [Test]
        public void Line_ShouldReturn_First_Two_Characters_Of_StationCode()
        {
            Assert.AreEqual("NE", new RawStationData { StationCode = "NE1" }.Line);
        }
    }
}
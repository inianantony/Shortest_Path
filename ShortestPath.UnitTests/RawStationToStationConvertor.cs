using System.Collections.Generic;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class RawStationToStationConvertor
    {
        public List<Station> Convert(List<RawStationData> rawRecords)
        {
            return new List<Station>();
        }
    }

    public class RawStationToStationConvertorTests
    {
        [Test]
        public void Returns_Empty_Stations_When_Empty_Raw_Data_Is_Passed()
        {
            var r = new RawStationToStationConvertor().Convert(new List<RawStationData>());
        }
    }
}
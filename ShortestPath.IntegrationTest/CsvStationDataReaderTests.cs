using NUnit.Framework;
using Shortest_Path;

namespace ShortestPath.IntegrationTest
{
    public class CsvStationDataReaderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetRawStaionRecords_Able_To_Read_StationMap_Csv()
        {
            CsvStationDataReader reader = new CsvStationDataReader(@"StationMap.csv");
            var stations = reader.GetRawStaionRecords();

            Assert.Greater(stations.Count, 1);
        }
    }
}
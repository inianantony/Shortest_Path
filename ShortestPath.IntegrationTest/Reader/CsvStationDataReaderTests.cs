using NUnit.Framework;
using Shortest_Path.Reader;

namespace ShortestPath.IntegrationTest.Reader
{
    public class CsvStationDataReaderTests
    {
        [Test]
        public void GetRawStationRecords_Able_To_Read_StationMap_Csv()
        {
            var reader = new CsvStationDataReader();
            var stations = reader.GetRawStationRecords(@"StationMap.csv");

            Assert.Greater(stations.Count, 1);
        }
    }
}
using NUnit.Framework;
using Shortest_Path.Reader;

namespace ShortestPath.IntegrationTest.Reader
{
    public class CsvStationDataReaderTests
    {
        [Test]
        public void GetRawStationRecords_Able_To_Read_StationMap_Csv()
        {
            CsvStationDataReader reader = new CsvStationDataReader(@"StationMap.csv");
            var stations = reader.GetRawStationRecords();

            Assert.Greater(stations.Count, 1);
        }
    }
}
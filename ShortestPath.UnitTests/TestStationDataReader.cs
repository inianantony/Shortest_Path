using System.Collections.Generic;
using Shortest_Path;

namespace ShortestPath.UnitTests
{
    public class TestStationDataReader : IStationDataReader
    {
        public List<RawStationData> GetRawStaionRecords()
        {
            return new List<RawStationData>
            {
                new RawStationData {StationCode = "NE1", StationName = "SengKang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE2", StationName = "Kovan", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE3", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE4", StationName = "BoonKeng", OpeningDate = string.Empty},
                new RawStationData {StationCode = "CC1", StationName = "Lorang", OpeningDate = string.Empty},
                new RawStationData {StationCode = "CC2", StationName = "Serangoon", OpeningDate = string.Empty},
                new RawStationData {StationCode = "CC3", StationName = "Bishan", OpeningDate = string.Empty},
            };
        }
    }
}
using System.Collections.Generic;

namespace ShortestPath.UnitTests
{
    public interface IStationDataReader
    {
        List<RawStationData> GetRawStaionRecords();
    }
}
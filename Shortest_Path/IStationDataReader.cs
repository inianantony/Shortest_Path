using System.Collections.Generic;

namespace Shortest_Path
{
    public interface IStationDataReader
    {
        List<RawStationData> GetRawStaionRecords();
    }
}
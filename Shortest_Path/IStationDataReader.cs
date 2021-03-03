using System.Collections.Generic;
using Shortest_Path.Models;

namespace Shortest_Path
{
    public interface IStationDataReader
    {
        List<RawStationData> GetRawStaionRecords();
    }
}
using System.Collections.Generic;
using Shortest_Path.Models;

namespace Shortest_Path.Reader
{
    public interface IStationDataReader
    {
        List<RawStationData> GetRawStationRecords(string filePath);
    }
}
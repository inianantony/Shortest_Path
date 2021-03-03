using CsvHelper.Configuration;
using Shortest_Path.Models;

namespace Shortest_Path.Mapper
{
    public sealed class StationDataMap : ClassMap<RawStationData>
    {
        public StationDataMap()
        {
            Map(m => m.StationCode).Name("Station Code", "StationCode");
            Map(m => m.StationName).Name("Station Name", "StationName");
            Map(m => m.OpeningDate).Name("Opening Date", "OpeningDate");
        }
    }
}
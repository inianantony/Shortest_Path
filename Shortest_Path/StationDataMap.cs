using CsvHelper.Configuration;

namespace Shortest_Path
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
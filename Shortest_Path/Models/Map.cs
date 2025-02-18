using System.Collections.Generic;
using Shortest_Path.Mapper;

namespace Shortest_Path.Models
{
    public class Map
    {
        public List<Station> Stations { get; }
        public Dictionary<string, List<Station>> MrtLines { get; }

        public Map()
        {
            Stations = new List<Station>();
        }

        public Map(List<RawStationData> rawRecords)
        {
            var rawStationConvertor = new RawStationConvertor();
            Stations = rawStationConvertor.Convert(rawRecords);
            MrtLines = rawStationConvertor.GroupStationsByLines(rawRecords, Stations);
        }

        public Map LinkStations()
        {
            Stations.ForEach(a => a.ConnectNearByStations(MrtLines));
            return this;
        }
    }
}
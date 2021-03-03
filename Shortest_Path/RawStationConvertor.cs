using System.Collections.Generic;
using System.Linq;
using Shortest_Path.Models;

namespace Shortest_Path
{
    public class RawStationConvertor
    {
        public List<Station> Convert(List<RawStationData> rawRecords)
        {
            var stations = new List<Station>();
            foreach (var rawStationData in rawRecords)
            {
                var station = new Station(rawStationData.StationName);
                if (stations.Exists(a => a.StationName == rawStationData.StationName))
                {
                    station = stations.First(a => a.StationName == rawStationData.StationName);
                }
                else
                {
                    stations.Add(station);
                }

                station.AddStationCode(rawStationData.StationCode);
                station.AddLine(rawStationData.Line);
            }
            return stations;
        }

        public Dictionary<string, List<Station>> GroupStationsByLines(List<RawStationData> rawRecords,
            List<Station> stations)
        {
            return rawRecords.GroupBy(
                    a => a.Line,
                    b => stations.First(c => c.StationName.Equals(b.StationName)))
                .ToDictionary(
                    a => a.Key,
                    b => b.ToList());
        }
    }
}

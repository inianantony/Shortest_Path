using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ShortestPath.UnitTests
{
    public class StationRecords
    {
        public StationRecords(List<Station> stations)
        {
            StationRecordList = stations;
        }

        private List<Station> StationRecordList { get; set; }

        public List<Station> LinkStations(List<Station> stations, Dictionary<string, List<Station>> mrtLines)
        {
            StationRecordList.ForEach(a => a.ConnectNearByStations(stations, mrtLines));
            return StationRecordList;
        }
    }
}
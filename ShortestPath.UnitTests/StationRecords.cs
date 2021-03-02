using System.Collections.Generic;

namespace ShortestPath.UnitTests
{
    public class StationRecords
    {
        public StationRecords(List<Station> stations)
        {
            StationRecordList = stations;
            
        }

        private List<Station> StationRecordList { get; set; }

        public List<Station> GetStations()
        {
            throw new System.NotImplementedException();
        }

        public Map GetMap()
        {
            throw new System.NotImplementedException();
        }
    }
}
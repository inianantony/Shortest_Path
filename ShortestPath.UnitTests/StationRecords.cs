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

        public List<Station> GetLinkedStations()
        {
            // var groupedStationsByLine = StationRecordList.GroupBy(a=>a.)
            // foreach (var stationBase in StationRecordList)
            // {
            //     List<Edge> linkedStaions = new List<Edge>();
            //     foreach (var line in StationRecordList)
            //     {
            //         for (int i = 0; i < line.Count(); i++)
            //         {
            //             if (stationBase.StationName.Equals(line[i].Name))
            //             {
            //                 if (i == 0)
            //                 {
            //                     var station = nodes.First(a => a.Name == line[i + 1].Name);
            //                     AddLinkStation(nodes, linkedStaions, station);
            //                 }
            //                 else if (i == (line.Count() - 1))
            //                 {
            //                     var station = nodes.First(a => a.Name == line[i - 1].Name);
            //                     AddLinkStation(nodes, linkedStaions, station);
            //
            //                 }
            //                 else
            //                 {
            //                     var station = nodes.First(a => a.Name == line[i + 1].Name);
            //                     AddLinkStation(nodes, linkedStaions, station);
            //                     station = nodes.First(a => a.Name == line[i - 1].Name);
            //                     AddLinkStation(nodes, linkedStaions, station);
            //                 }
            //             }
            //         }
            //     }
            // }
            
            return StationRecordList;
        }
    }
}
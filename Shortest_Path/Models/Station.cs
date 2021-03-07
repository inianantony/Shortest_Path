using System.Collections.Generic;
using System.Linq;

namespace Shortest_Path.Models
{
    public class Station
    {
        public Station(string stationName)
        {
            StationName = stationName;
            Lines = new List<string>();
            StationCodes = new List<string>();
            Connections = new List<Edge>();
        }

        public string StationName { get; }
        public List<string> Lines { get; }
        public List<string> StationCodes { get; }
        public List<Edge> Connections { get; set; }
        public Station NearestToStart { get; set; }
        public decimal? MinimumCost { get; set; }
        public bool Visited { get; set; }

        public Station AddStationCode(string stationCode)
        {
            if (string.IsNullOrEmpty(stationCode)) return this;

            if (!StationCodes.Contains(stationCode))
                StationCodes.Add(stationCode);
            return this;
        }

        public Station AddLine(string lineName)
        {
            if (string.IsNullOrEmpty(lineName)) return this;

            if (!Lines.Contains(lineName))
                Lines.Add(lineName);

            return this;
        }

        public Station ConnectNearByStations(Dictionary<string, List<Station>> mrtLines)
        {
            var nearbyStations = new List<Station>();
            foreach (var line in mrtLines.Values)
            {
                nearbyStations.AddRange(GetNearbyStations(line));
            }

            Connections = nearbyStations.Distinct().Select(a => new Edge { ConnectedStation = a, Cost = 1, Length = 1 }).ToList();

            return this;
        }

        private List<Station> GetNearbyStations(List<Station> aMrtLine)
        {
            var nearbyStations = new List<Station>();
            for (var i = 0; i < aMrtLine.Count; i++)
            {
                if (!IsSameAs(aMrtLine[i])) continue;

                var firstStationIndex = i == 0;
                var lastStationIndex = i == aMrtLine.Count - 1;
                var nextStationIndex = i + 1;
                var previousStationIndex = i - 1;

                if (firstStationIndex)
                {
                    nearbyStations.Add(aMrtLine[nextStationIndex]);
                }
                else if (lastStationIndex)
                {
                    nearbyStations.Add(aMrtLine[previousStationIndex]);
                }
                else
                {
                    nearbyStations.Add(aMrtLine[nextStationIndex]);
                    nearbyStations.Add(aMrtLine[previousStationIndex]);
                }
            }

            return nearbyStations;
        }

        public bool IsSameAs(string stationName)
        {
            return StationName == stationName;
        }

        public bool IsSameAs(Station station)
        {
            return StationName == station.StationName;
        }
    }
}
using System;
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
        public double? MinimumCost { get; set; }
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

        public Station ConnectNearByStations(List<Station> stations, Dictionary<string, List<Station>> mrtLines)
        {
            var nearbyStations = new List<Station>();
            foreach (var line in mrtLines)
            {
                var nearbyIndices = GetNearbyStationIndices(line);
                nearbyStations.AddRange(nearbyIndices.Select(a => stations.First(b => b.IsSameAs(line.Value[a]))).ToList());
            }

            Connections = nearbyStations.Distinct().Select(a => new Edge { ConnectedStation = a, Cost = 1, Length = 1 }).ToList();

            return this;
        }

        private List<int> GetNearbyStationIndices(KeyValuePair<string, List<Station>> line)
        {
            var nearbyIndices = new List<int>();
            for (var i = 0; i < line.Value.Count; i++)
            {
                if (!IsSameAs(line.Value[i])) continue;

                var firstStation = i == 0;
                var lastStation = i == line.Value.Count - 1;
                if (firstStation)
                {
                    nearbyIndices.Add(i + 1);
                }
                else if (lastStation)
                {
                    nearbyIndices.Add(i - 1);
                }
                else
                {
                    nearbyIndices.Add(i + 1);
                    nearbyIndices.Add(i - 1);
                }
            }

            return nearbyIndices;
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
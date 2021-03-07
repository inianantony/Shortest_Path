using System.Collections.Generic;
using System.Linq;
using Shortest_Path.Algorithm.CostCalculator;

namespace Shortest_Path.Models
{
    public class Edge
    {
        public decimal Cost { get; set; }
        public Station ConnectedStation { get; set; }

        private List<string> CommonStations(Station station)
        {
            return ConnectedStation.Lines.Intersect(station.Lines).ToList();
        }
        private List<string> AllStations(Station station)
        {
            return ConnectedStation.Lines.Union(station.Lines).ToList();
        }

        public bool IsInterchanged(Station station)
        {
            return !CommonStations(station).Any();
        }

        public bool IsInNeOrNs(Station station)
        {
            return AllStations(station).Intersect(CostCalculationConfigs.NeNsLines).Any();
        }

        public bool IsInDtTe(Station station)
        {
            return AllStations(station).Intersect(CostCalculationConfigs.DtTeLines).Any();
        }

        public bool IsInTe(Station station)
        {
            return AllStations(station).Intersect(CostCalculationConfigs.TeLine).Any();
        }
    }
}
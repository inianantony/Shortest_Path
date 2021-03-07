using System.Collections.Generic;
using System.Linq;
using Shortest_Path.Algorithm.CostCalculator;

namespace Shortest_Path.Models
{
    public class Edge
    {
        public decimal Cost { get; set; }
        public Station ConnectedStation { get; set; }

        public List<string> CommonStations(Station station)
        {
            return ConnectedStation.Lines.Intersect(station.Lines).ToList();
        }

        public bool IsInterchange(Station station)
        {
            return !CommonStations(station).Any();
        }

        public bool IsInNeOrNs(Station station)
        {
            return CommonStations(station).Intersect(CostCalculationConfigs.NeNsLines).Any();
        }

        public List<string> AllStations(Station station)
        {
            return ConnectedStation.Lines.Union(station.Lines).ToList();
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
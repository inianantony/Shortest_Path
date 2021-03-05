using System;
using System.Collections.Generic;
using System.Linq;
using Shortest_Path.Algorithm.CostCalculator;
using Shortest_Path.Models;

namespace Shortest_Path.Algorithm
{
    public class DijkstraSearch : ISearchAlgorithm
    {
        public List<Station> FillShortestPath(List<Station> stations, Options option)
        {
            var start = stations.First(a => a.IsSameAs(option.StartStation));
            var end = stations.First(a => a.IsSameAs(option.EndStation));
            start.MinimumCost = 0;
            var priorityQueue = new List<Station> { start };
            do
            {
                priorityQueue = priorityQueue.OrderBy(x => x.MinimumCost ?? 0).ToList();
                var station = priorityQueue.First();
                priorityQueue.Remove(station);
                foreach (var cnn in station.Connections.OrderBy(x => x.Cost))
                {
                    var connectedStation = cnn.ConnectedStation;
                    if (connectedStation.Visited)
                        continue;
                    if (IsLineClosed(option, cnn, station))
                        continue;

                    var eachStationCost = EachStationCost(option, cnn, station);

                    if (connectedStation.MinimumCost == null || station.MinimumCost + eachStationCost < connectedStation.MinimumCost)
                    {
                        connectedStation.MinimumCost = station.MinimumCost + eachStationCost;
                        connectedStation.NearestToStart = station;
                        if (!priorityQueue.Contains(connectedStation))
                            priorityQueue.Add(connectedStation);
                    }
                }
                station.Visited = true;
                if (station == end)
                    return stations;
            } while (priorityQueue.Any());

            return stations;
        }

        private static double EachStationCost(Options option, Edge cnn, Station station)
        {
            var costCalculator = new InterchangingAtNonPeak(
                new InterchangingAtNight(
                    new InterchangingAtPeakHour(
                        new NightInTe(
                            new PeakHourInNeNs(
                                new PeakHourInOtherLines(
                                    new NonPeakInAllLines(
                                        new NonPeakInDtTe(
                                            new BaseCostCalculator()))))))));
            return costCalculator.GetCost(option, cnn, station);
        }
        private static bool IsLineClosed(Options option, Edge cnn, Station station)
        {
            if (option.JourneyTime.IsDisabled()) return false;

            var getLies = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var isInDtCgCe = getLies.Intersect(new List<string> { "DT", "CG", "CE" }).Any();
            var isNight = option.JourneyTime.IsNight();
            var onlyDtCgCeAvailable = cnn.ConnectedStation.Lines.Count ==1 && cnn.ConnectedStation.Lines.Intersect(new List<string> { "DT", "CG", "CE" }).Any();

            return isNight && (isInDtCgCe || onlyDtCgCeAvailable);
        }
    }
}
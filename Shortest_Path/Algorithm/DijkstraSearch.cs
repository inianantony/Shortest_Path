using Shortest_Path.Algorithm.CostCalculator;
using Shortest_Path.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shortest_Path.Algorithm
{
    public class DijkstraSearch : ISearchAlgorithm
    {
        public List<Station> FillShortestPath(List<Station> stations, InputOption inputOption)
        {
            var start = stations.First(a => a.IsSameAs(inputOption.StartStation));
            var end = stations.First(a => a.IsSameAs(inputOption.EndStation));
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
                    if (IsLineClosed(inputOption, cnn, station))
                        continue;

                    var eachStationCost = EachStationCost(inputOption, cnn, station);

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

        private static decimal EachStationCost(InputOption inputOption, Edge cnn, Station station)
        {
            var costCalculator = new DisabledJourneyTime(
                new InterchangingAtNonPeak(
                    new InterchangingAtNight(
                        new InterchangingAtPeakHour(
                            new NightInTe(
                                new NightInOtherLines(
                                    new PeakHourInNeNs(
                                        new PeakHourInOtherLines(
                                            new NonPeakInAllLines(
                                                new NonPeakInDtTe(
                                                    new BaseCostCalculator()))))))))));
            return costCalculator.GetCost(inputOption, cnn, station);
        }
        private static bool IsLineClosed(InputOption inputOption, Edge cnn, Station station)
        {
            if (inputOption.JourneyTime.IsDisabled()) return false;

            var getLies = cnn.ConnectedStation.Lines.Intersect(station.Lines).ToList();
            var closedLines = new List<string> { "DT", "CG", "CE" };
            var isInDtCgCe = getLies.Intersect(closedLines).Any();
            var isNight = inputOption.JourneyTime.IsNight();
            var onlyDtCgCeOptionAvailable = cnn.ConnectedStation.Lines.Count == 1 && cnn.ConnectedStation.Lines.Intersect(closedLines).Any();

            return isNight && (isInDtCgCe || onlyDtCgCeOptionAvailable);
        }
    }
}
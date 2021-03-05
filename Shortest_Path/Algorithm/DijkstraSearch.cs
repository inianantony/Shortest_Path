using System.Collections.Generic;
using System.Linq;
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


                    var addedCost = 0;
                    if (!option.JourneyTime.IsDisabled)
                    {
                        var commonStations = connectedStation.Lines.Intersect(station.Lines).ToList();
                        var getLies = connectedStation.Lines.Union(station.Lines).ToList();
                        var isInNeOrNs = commonStations.Intersect(new List<string> { "NE", "NS" }).Any();
                        var isInDtCgCe = getLies.Intersect(new List<string> { "DT", "CG", "CE" }).Any();
                        var isInDtTe = getLies.Intersect(new List<string> { "DT", "TE" }).Any();
                        var isInTe = getLies.Intersect(new List<string> { "TE" }).Any();
                        var isPeakHour = option.JourneyTime.IsPeak();
                        var isNight = option.JourneyTime.IsNight();
                        var isNonPeak = option.JourneyTime.IsNonPeak();
                        var interchange = !commonStations.Any();

                        if (isNight && isInDtCgCe)
                        {
                            continue;
                        }
                        if (isNonPeak && interchange)
                        {
                            addedCost = 10;
                        }
                        else if (isNonPeak && !isInDtTe)
                        {
                            addedCost = 10;
                        }
                        else if (isNonPeak)
                        {
                            addedCost = 8;
                        }
                        else if (isNight && interchange)
                        {
                            addedCost = 10;
                        }
                        else if (isNight && isInTe)
                        {
                            addedCost = 8;
                        }
                        else if (interchange && isPeakHour)
                        {
                            addedCost = 15;
                        }
                        else if (isInNeOrNs && isPeakHour)
                        {
                            addedCost = 12;
                        }
                        else if (!isInNeOrNs && isPeakHour)
                        {
                            addedCost = 10;
                        }
                    }

                    if (connectedStation.MinimumCost == null || station.MinimumCost + cnn.Cost + addedCost < connectedStation.MinimumCost)
                    {
                        connectedStation.MinimumCost = station.MinimumCost + cnn.Cost + addedCost;
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
    }
}
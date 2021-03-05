using System.Collections.Generic;
using System.Linq;
using Shortest_Path.Models;

namespace Shortest_Path.Algorithm
{
    public class DijkstraSearch : ISearchAlgorithm
    {
        public List<Station> FillShortestPath(List<Station> stations, Station startStation, Station endStation)
        {
            var start = stations.First(a => a.IsSameAs(startStation));
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
                    if (connectedStation.MinimumCost == null || station.MinimumCost + cnn.Cost < connectedStation.MinimumCost)
                    {
                        connectedStation.MinimumCost = station.MinimumCost + cnn.Cost;
                        connectedStation.NearestToStart = station;
                        if (!priorityQueue.Contains(connectedStation))
                            priorityQueue.Add(connectedStation);
                    }
                }
                station.Visited = true;
                if (station == endStation)
                    return stations;
            } while (priorityQueue.Any());

            return stations;
        }
    }
}
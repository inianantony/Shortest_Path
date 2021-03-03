using System.Collections.Generic;
using System.Linq;

namespace Shortest_Path
{
    public class RouteInfo
    {
        private readonly List<Station> _shortestPath;
        private readonly Station _start;
        private readonly Station _end;

        public string JourneyTitle => $"Travel from {_start.StationName} to {_end.StationName}";
        public string StationsTravelled => $"Stations travelled: {_shortestPath.Count}";

        public string Route
        {
            get
            {
                var preIntersect = string.Empty;
                var routes = new List<string>();
                for (int i = 0; i < _shortestPath.Count - 1; i++)
                {
                    var current = _shortestPath[i];
                    var next = _shortestPath[i + 1];
                    var getIntersectingStation = current.Lines.Intersect(next.Lines).First();

                    var switchingLines = preIntersect != getIntersectingStation && preIntersect != string.Empty;
                    if (switchingLines)
                    {
                        routes.Add(current.StationCodes.Find(a => a.StartsWith(preIntersect)));
                    }

                    routes.Add(current.StationCodes.Find(a => a.StartsWith(getIntersectingStation)));

                    var closeToLastStation = i == _shortestPath.Count - 2;
                    if (closeToLastStation)
                    {
                        routes.Add(next.StationCodes.Find(a => a.StartsWith(getIntersectingStation)));
                    }

                    preIntersect = getIntersectingStation;
                }

                return $"Route : ('{string.Join("', '", routes) }')";
            }
        }

        public List<string> Journey
        {
            get
            {
                var preIntersect = string.Empty;
                List<string> routes = new List<string>();
                for (int i = 0; i < _shortestPath.Count - 1; i++)
                {
                    var current = _shortestPath[i];
                    var next = _shortestPath[i + 1];
                    var getIntersectingStation = current.Lines.Intersect(next.Lines).First();
                    if (preIntersect != getIntersectingStation && preIntersect != string.Empty)
                    {
                        routes.Add($"Change from {preIntersect} line to {getIntersectingStation} line");
                    }

                    preIntersect = getIntersectingStation;
                    routes.Add($"Take {getIntersectingStation} line from {current.StationName} to {next.StationName}");
                }

                return routes;
            }
        }

        public RouteInfo(List<Station> shortestPath, Station start, Station end)
        {
            _shortestPath = shortestPath;
            _start = start;
            _end = end;
        }

    }
}
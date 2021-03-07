using System.Collections.Generic;
using System.Linq;

namespace Shortest_Path.Models
{
    public class RouteInfo
    {
        private readonly List<Station> _shortestPath;
        private readonly Station _start;
        private readonly Station _end;
        private bool _notEnoughRoute;

        public RouteInfo(List<Station> shortestPath, Station start, Station end)
        {
            _shortestPath = shortestPath;
            _start = start;
            _end = end;
        }

        private bool NotEnoughRoute => _shortestPath.Count < 2;

        public string JourneyTitle => $"Travel from {_start.StationName} to {_end.StationName}{(NotEnoughRoute ? " is \"Not\" possible" : string.Empty)}";

        public string StationsTraveled => NotEnoughRoute ? string.Empty : $"Stations traveled: {_shortestPath.Count}";

        public string Route
        {
            get
            {
                var preIntersectStationCode = string.Empty;
                var routes = new List<string>();
                for (var i = 0; i < _shortestPath.Count - 1; i++)
                {
                    var currentStation = _shortestPath[i];
                    var nextStation = _shortestPath[i + 1];
                    var getIntersectingStationCode = currentStation.Lines.Intersect(nextStation.Lines).First();

                    var switchingLines = preIntersectStationCode != getIntersectingStationCode && preIntersectStationCode != string.Empty;
                    if (switchingLines)
                    {
                        routes.Add(currentStation.StationCodes.Find(a => a.StartsWith(preIntersectStationCode)));
                    }

                    routes.Add(currentStation.StationCodes.Find(a => a.StartsWith(getIntersectingStationCode)));

                    var closeToLastStation = i == _shortestPath.Count - 2;
                    if (closeToLastStation)
                    {
                        routes.Add(nextStation.StationCodes.Find(a => a.StartsWith(getIntersectingStationCode)));
                    }

                    preIntersectStationCode = getIntersectingStationCode;
                }
                if (routes.Any())
                    return $"Route : ('{string.Join("', '", routes) }')";
                return string.Empty;
            }
        }

        public List<string> Journey
        {
            get
            {
                var preIntersectStationCode = string.Empty;
                var routes = new List<string>();
                for (int i = 0; i < _shortestPath.Count - 1; i++)
                {
                    var currentStation = _shortestPath[i];
                    var nextStation = _shortestPath[i + 1];
                    var getIntersectingStationCode = currentStation.Lines.Intersect(nextStation.Lines).First();
                    if (preIntersectStationCode != getIntersectingStationCode && preIntersectStationCode != string.Empty)
                    {
                        routes.Add($"Change from {preIntersectStationCode} line to {getIntersectingStationCode} line");
                    }

                    preIntersectStationCode = getIntersectingStationCode;
                    routes.Add($"Take {getIntersectingStationCode} line from {currentStation.StationName} to {nextStation.StationName}");
                }

                return routes;
            }
        }
    }
}
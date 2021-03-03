using System;

namespace Shortest_Path
{
    public class ConsolePrinter : IPrinter
    {
        private readonly RouteInfo _routeInfo;

        public ConsolePrinter(RouteInfo routeInfo)
        {
            _routeInfo = routeInfo;
        }

        public void PrintJourneyTitle()
        {
            Console.WriteLine(_routeInfo.JourneyTitle);
        }

        public void PrintStations()
        {
            Console.WriteLine(_routeInfo.StationsTravelled);
        }

        public void PrintRoute()
        {
            Console.WriteLine(_routeInfo.Route);
        }

        public void PrintJourney()
        {
            Console.WriteLine(string.Join(Environment.NewLine, _routeInfo.Journey));
        }
    }
}
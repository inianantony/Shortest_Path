using System;
using System.Linq;
using Shortest_Path.Models;

namespace Shortest_Path.Printer
{
    public class ConsolePrinter : IPrinter
    {
        private RouteInfo _routeInfo;

        private void PrintJourneyTitle()
        {
            Console.WriteLine(_routeInfo.JourneyTitle);
        }

        private void PrintStations()
        {
            Console.WriteLine(_routeInfo.StationsTraveled);
        }

        private void PrintRoute()
        {
            Console.WriteLine(_routeInfo.Route);
        }

        private void PrintJourney()
        {
            Console.WriteLine(string.Join(Environment.NewLine, _routeInfo.Journey));
        }

        public IPrinter With(RouteInfo routeInfo)
        {
            _routeInfo = routeInfo;
            return this;
        }

        public void DisplayRoutes()
        {

            PrintJourneyTitle();
            PrintStations();
            PrintRoute();
            PrintJourney();
        }
    }
}
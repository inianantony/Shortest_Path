using System.Collections.Generic;
using NUnit.Framework;
using Shortest_Path;

namespace ShortestPath.UnitTests
{
    [TestFixture]
    public class CoverageTest
    {
        [Test]
        public void FullCoverageTest()
        {
            var records = new StationRecords();
            var map = records.GetMap();

            var start = new Station { Name = "Ubi" };
            var end = new Station{Name = "Kovan"};
            var route = map.GetRouteFor(start, end);

            Direction direction = new Direction(route);
            IPrinter printer = new ConsolePrinter(direction);
            printer.PrintJourneyTitle();
            printer.PrintStations();
            printer.PrintRoute();
            printer.PrintJourney();
        }

    }

    public class ConsolePrinter : IPrinter
    {
        public ConsolePrinter(Direction direction)
        {
            throw new System.NotImplementedException();
        }

        public void PrintJourneyTitle()
        {
            throw new System.NotImplementedException();
        }

        public void PrintStations()
        {
            throw new System.NotImplementedException();
        }

        public void PrintRoute()
        {
            throw new System.NotImplementedException();
        }

        public void PrintJourney()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IPrinter
    {
        void PrintJourneyTitle();
        void PrintStations();
        void PrintRoute();
        void PrintJourney();
    }

    public class Direction
    {
        public Direction(List<Station> route)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Station
    {
        public string Name { get; set; }
    }

    public class Map
    {
        public List<Station> GetRouteFor(Station start, Station end)
        {
            throw new System.NotImplementedException();
        }
    }

    public class StationRecords
    {
        public Map GetMap()
        {
            throw new System.NotImplementedException();
        }
    }
}
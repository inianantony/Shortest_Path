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
            IStationDataReader reader = new TestStationDataReader();
            var rawRecords = reader.GetRawStaionRecords();

            var stationRecords = new StationRecords();
            var stations = stationRecords.GetStations();

            var map = Map.BuildMapFor(stations);

            var start = new Station { StationName = "Ubi" };
            var end = new Station { StationName = "Kovan" };
            var route = map.GetRouteFor(start, end);

            Direction direction = new Direction(route);
            IPrinter printer = new ConsolePrinter(direction);
            printer.PrintJourneyTitle();
            printer.PrintStations();
            printer.PrintRoute();
            printer.PrintJourney();
        }


    }



    public class TestStationDataReader : IStationDataReader
    {
        public StationRecords GetRawStaionRecords()
        {
            return new StationRecords
            {
                StationRecordList = new List<RawStationData>
                {
                    new RawStationData{StationCode  = "NE1", StationName =  "SengKang" , OpeningDate = string.Empty },
                    new RawStationData{StationCode  = "NE2", StationName =  "Kovan" , OpeningDate = string.Empty },
                    new RawStationData{StationCode  = "NE3", StationName =  "Serangoon" , OpeningDate = string.Empty },
                    new RawStationData{StationCode  = "CC1", StationName =  "Lorang" , OpeningDate = string.Empty },
                    new RawStationData{StationCode  = "CC2", StationName =  "Serangoon" , OpeningDate = string.Empty },
                    new RawStationData{StationCode  = "CC3", StationName =  "Bishan" , OpeningDate = string.Empty },
                }
            };
        }
    }

    public class CSVStationDataReader : IStationDataReader
    {
        public StationRecords GetRawStaionRecords()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IStationDataReader
    {
        StationRecords GetRawStaionRecords();
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
        public Direction(List<RawStationData> route)
        {
            throw new System.NotImplementedException();
        }
    }


    public class Station
    {
        public string StationCode { get; set; }
        public string StationName { get; set; }
    }

    public class RawStationData
    {
        public string StationCode { get; set; }
        public string StationName { get; set; }
        public object OpeningDate { get; set; }
    }

    public class Map
    {
        public List<RawStationData> GetRouteFor(Station start, Station end)
        {
            throw new System.NotImplementedException();
        }

        public static Map BuildMapFor(List<Station> stations)
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

        public List<RawStationData> StationRecordList { get; set; }

        public List<Station> GetStations()
        {
            throw new System.NotImplementedException();
        }
    }
}
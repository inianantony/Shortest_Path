using System;
using CommandLine;

namespace Shortest_Path
{
    public class Options
    {
        [Option('s', "start", Required = false, HelpText = "Enter the start")]
        public string Start { get; set; }
        [Option('e', "end", Required = false, HelpText = "Enter the destination")]
        public string End { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var option = GetOptions(args);

            IStationDataReader reader = new CsvStationDataReader(@"StationMap.csv");
            var rawRecords = reader.GetRawStaionRecords();

            var rawStationConvertor = new RawStationConvertor();
            var stations = rawStationConvertor.Convert(rawRecords);
            var mrtLines = rawStationConvertor.GroupStationsByLines(rawRecords, stations);

            var map = new Map().LinkStations(stations, mrtLines);

            var start = new Station(option.Start);
            var end = new Station(option.End);

            ISearchAlgorithm algorithm = new DijkstraSearch();
            Direction direction = new Direction(algorithm, start, end);
            var routeInfo = direction.PrepareRouteInfoFrom(map);

            IPrinter printer = new ConsolePrinter(routeInfo);
            printer.PrintJourneyTitle();
            printer.PrintStations();
            printer.PrintRoute();
            printer.PrintJourney();
        }

        private static Options GetOptions(string[] args)
        {
            Options option;
            string start = string.Empty;
            string end = string.Empty;
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    if (!string.IsNullOrEmpty(o.Start))
                        start = o.Start;
                    else
                    {
                        Console.WriteLine($"Enter the Starting Station");
                        start = Console.ReadLine();
                    }

                    if (!string.IsNullOrEmpty(o.End))
                        end = o.End;
                    else
                    {
                        Console.WriteLine($"Enter the destination Station");
                        end = Console.ReadLine();
                    }
                });
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            {
                throw new Exception("Invalid Start or destination! Program Terminates!");
            }
            else
            {
                option = new Options
                {
                    Start = start,
                    End = end
                };
            }

            return option;
        }
    }
}

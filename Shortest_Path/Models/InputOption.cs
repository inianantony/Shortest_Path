using System;
using CommandLine;

namespace Shortest_Path.Models
{
    public class InputOption
    {
        [Option('s', "start", Required = false, HelpText = "Enter the start")]
        public string Start { get; set; }

        [Option('e', "end", Required = false, HelpText = "Enter the destination")]
        public string End { get; set; }

        [Option('c', "csvpath", Required = false, HelpText = "Enter the csv path")]
        public string CsvPath { get; set; }

        [Option('t', "starttime", Required = false, HelpText = "Enter the start time")]
        public DateTime StartTime { get; set; }


        public Station StartStation => new Station(Start);

        public Station EndStation => new Station(End);

        public InputOption()
        {
            JourneyTime = new JourneyTime(this);
        }

        public JourneyTime JourneyTime;

        public static InputOption Get(string[] args)
        {
            var options = new InputOption();
            Parser.Default.ParseArguments<InputOption>(args).WithParsed(o => options = o);
            options.Validate();
            return options;
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Start) || string.IsNullOrEmpty(End))
            {
                throw new Exception("Invalid Start or destination! Program Terminates!");
            }

            if (string.IsNullOrEmpty(CsvPath))
            {
                throw new Exception("Invalid CSV Path! Program Terminates!");
            }
        }

        public void ValidateStations(Map map)
        {
            if (!map.Stations.Exists(a => a.IsSameAs(Start)))
            {
                throw new Exception("Invalid Start! Program Terminates!");
            }

            if (!map.Stations.Exists(a => a.IsSameAs(End)))
            {
                throw new Exception("Invalid End! Program Terminates!");
            }
        }
    }
}
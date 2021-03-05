using System;
using CommandLine;

namespace Shortest_Path.Models
{
    public class Options
    {
        [Option('s', "start", Required = false, HelpText = "Enter the start")]
        public string Start { get; set; }

        [Option('e', "end", Required = false, HelpText = "Enter the destination")]
        public string End { get; set; }

        [Option('c', "csvpath", Required = false, HelpText = "Enter the csv path")]
        public string CsvPath { get; set; }

        public Station StartStation => new Station(Start);
        public Station EndStation => new Station(End);

        public static Options GetInputOptions(string[] args)
        {
            var options = new Options();
            Parser.Default.ParseArguments<Options>(args).WithParsed(o => options = o);
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
    }
}
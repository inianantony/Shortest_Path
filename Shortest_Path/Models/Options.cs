﻿using System;
using CommandLine;

namespace Shortest_Path.Models
{
    public class Options
    {
        [Option('s', "start", Required = false, HelpText = "Enter the start")]
        public string Start { get; set; }
        [Option('e', "end", Required = false, HelpText = "Enter the destination")]
        public string End { get; set; }

        public Station StartStation => new Station(Start); 
        public Station EndStation => new Station(End);

        public static Options GetOptions(string[] args)
        {
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

            var option = new Options
            {
                Start = start,
                End = end
            };

            return option;
        }
    }
}
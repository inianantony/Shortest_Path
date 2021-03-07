using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Models
{
    class OptionsTests
    {
        [Test]
        public void GetOptions_Should_GetParsedOptions()
        {
            var options = InputOption.Get(new[] {"--start=Ubi", "--end=Kovan", @"--csvpath=c:\Station.csv" });
            var expected = new InputOption
            {
                Start = "Ubi",
                End = "Kovan",
                CsvPath = @"c:\Station.csv"
            };
            expected.Should().BeEquivalentTo(options);
        }

        [Test]
        public void Validate_ShouldThrowException_If_Start_Is_NotPassed()
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                InputOption.Get(new[] { "--end=Kovan", @"--csvpath=c:\Station.csv" });
            });
            Assert.AreEqual("Invalid Start or destination! Program Terminates!", ex.Message);
        }

        [Test]
        public void Validate_ShouldThrowException_If_End_Is_NotPassed()
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                InputOption.Get(new[] { "--start=Ubi",  @"--csvpath=c:\Station.csv" });
            });
            Assert.AreEqual("Invalid Start or destination! Program Terminates!", ex.Message);
        }

        [Test]
        public void Validate_ShouldThrowException_If_Csv_Path_Is_NotPassed()
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                InputOption.Get(new[] { "--start=Ubi", "--end=Kovan" });
            });
            Assert.AreEqual("Invalid CSV Path! Program Terminates!", ex.Message);
        }

        [Test]
        public void ValidateStations_ShouldThrowException_If_Start_Station_Is_Not_In_Map()
        {
            var sengkang = "Sengkang";
            var kovan = "Kovan";
            var rawRecords = new List<RawStationData>
            {
                new RawStationData {StationCode = "NE1", StationName = sengkang, OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE2", StationName = kovan, OpeningDate = string.Empty},
            };

            //Act
            var map = new Map(rawRecords).LinkStations();
            var ex = Assert.Throws<Exception>(() =>
            {
                new InputOption{Start = "UbiNew"}.ValidateStations(map);
            });

            Assert.AreEqual("Invalid Start! Program Terminates!", ex.Message);
        }

        [Test]
        public void ValidateStations_ShouldThrowException_If_End_Station_Is_Not_In_Map()
        {
            var sengkang = "Sengkang";
            var kovan = "Kovan";
            var rawRecords = new List<RawStationData>
            {
                new RawStationData {StationCode = "NE1", StationName = sengkang, OpeningDate = string.Empty},
                new RawStationData {StationCode = "NE2", StationName = kovan, OpeningDate = string.Empty},
            };

            //Act
            var map = new Map(rawRecords).LinkStations();
            var ex = Assert.Throws<Exception>(() =>
            {
                new InputOption { Start = "Sengkang", End = "UbiNew"}.ValidateStations(map);
            });

            Assert.AreEqual("Invalid End! Program Terminates!", ex.Message);
        }
    }
}

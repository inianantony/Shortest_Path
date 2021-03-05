using System;
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
            var options = Options.GetInputOptions(new[] {"--start=Ubi", "--end=Kovan", @"--csvpath=c:\Station.csv" });
            var expected = new Options
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
                Options.GetInputOptions(new[] { "--end=Kovan", @"--csvpath=c:\Station.csv" });
            });
            Assert.AreEqual("Invalid Start or destination! Program Terminates!", ex.Message);
        }

        [Test]
        public void Validate_ShouldThrowException_If_End_Is_NotPassed()
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                Options.GetInputOptions(new[] { "--start=Ubi",  @"--csvpath=c:\Station.csv" });
            });
            Assert.AreEqual("Invalid Start or destination! Program Terminates!", ex.Message);
        }

        [Test]
        public void Validate_ShouldThrowException_If_Csv_Path_Is_NotPassed()
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                Options.GetInputOptions(new[] { "--start=Ubi", "--end=Kovan" });
            });
            Assert.AreEqual("Invalid CSV Path! Program Terminates!", ex.Message);
        }
    }
}

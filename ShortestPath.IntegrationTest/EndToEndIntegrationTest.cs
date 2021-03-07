using System;
using System.Runtime.Intrinsics.X86;
using Moq;
using NUnit.Framework;
using Shortest_Path;
using Shortest_Path.Models;
using Shortest_Path.Printer;

namespace ShortestPath.IntegrationTest
{
    class EndToEndIntegrationTest
    {
        [Test]
        public void Program_Tests()
        {
            Assert.DoesNotThrow(() =>
            {
                var start = "Expo";
                var end = "Tampines";
                Program.Main(new[] { $"--start={start}", $"--end={end}", "--csvpath=../../../StationMap.csv" });
            });
        }

        [Test]
        public void Journey_With_TE_Line_When_Time_Is_Not_Considered()
        {
            var start = "Bugis";
            var end = "Little India";
            var printer = new Mock<IPrinter>();
            printer.Setup(a => a.DisplayRoutes());
            RouteInfo routeInfo = null;
            printer.Setup(a => a.With(It.IsAny<RouteInfo>())).Callback<RouteInfo>(r => routeInfo = r).Returns(printer.Object);
            Program.Printer = printer.Object;
            Program.Main(new[] { $"--start={start}", $"--end={end}", "--csvpath=../../../StationMap.csv" });
            Assert.AreEqual("Route : ('DT14', 'DT13', 'DT12')", routeInfo.Route);
        }

        [Test]
        public void Journey_With_DT_Line_During_Night_Hours()
        {
            var start = "Bugis";
            var end = "Little India";
            var printer = new Mock<IPrinter>();
            printer.Setup(a => a.DisplayRoutes());
            RouteInfo routeInfo = null;
            printer.Setup(a => a.With(It.IsAny<RouteInfo>())).Callback<RouteInfo>(r => routeInfo = r).Returns(printer.Object);
            Program.Printer = printer.Object;
            Program.Main(new[] { $"--start={start}", $"--end={end}", "--csvpath=../../../StationMap.csv", "--starttime=2021-03-05T23:00" });
            Assert.AreEqual("Route : ('EW12', 'EW13', 'NS25', 'NS24', 'NE6', 'NE7')", routeInfo.Route);
        }
    }
}

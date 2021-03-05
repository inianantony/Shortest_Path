using NUnit.Framework;
using Shortest_Path;

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
    }
}

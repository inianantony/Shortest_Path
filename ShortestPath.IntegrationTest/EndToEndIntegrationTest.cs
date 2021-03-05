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
                Program.Main(new[] { "--start=Ubi", "--end=Kovan", "--csvpath=../../../StationMap.csv" });
            });
        }
    }
}

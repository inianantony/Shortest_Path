using System.Collections.Generic;

namespace ShortestPath.UnitTests
{
    public class Station
    {
        public string StationName { get; set; }
        public List<string> Lines { get; set; }
        public List<string> StationCodes { get; set; }
    }
}
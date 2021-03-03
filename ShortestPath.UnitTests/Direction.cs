using System.Collections.Generic;

namespace ShortestPath.UnitTests
{
    public class Direction
    {
        private readonly Station _start;
        private readonly Station _end;

        public Direction(Station start, Station end)
        {
            _start = start;
            _end = end;
        }

        public List<Station> PrepareRouteFrom(Map map)
        {
            return null;
        }
    }
}
using System.Collections.Generic;

namespace Shortest_Path
{
    public class Map
    {
        public List<Station> Stations { get; private set; }

        public Map()
        {
            Stations = new List<Station>();
        }

        public Map LinkStations(List<Station> stations, Dictionary<string, List<Station>> mrtLines)
        {
            stations.ForEach(a => a.ConnectNearByStations(stations, mrtLines));
            Stations = stations;
            return this;
        }
    }
}
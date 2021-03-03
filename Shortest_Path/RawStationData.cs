namespace Shortest_Path
{
    public class RawStationData
    {
        public string StationCode { get; set; }
        public string StationName { get; set; }
        public string OpeningDate { get; set; }

        public string Line => StationCode.Substring(0, 2);
    }
}
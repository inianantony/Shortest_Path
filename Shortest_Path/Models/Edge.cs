namespace Shortest_Path.Models
{
    public class Edge
    {
        public int Length { get; set; }
        public decimal Cost { get; set; }
        public Station ConnectedStation { get; set; }
    }
}
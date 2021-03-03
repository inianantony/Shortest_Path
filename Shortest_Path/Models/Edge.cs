namespace Shortest_Path.Models
{
    public class Edge
    {
        public double Length { get; set; }
        public double Cost { get; set; }
        public Station ConnectedStation { get; set; }

        public override string ToString()
        {
            return "-> " + ConnectedStation;
        }
    }
}
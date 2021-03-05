using Shortest_Path.Models;

namespace Shortest_Path.Writer
{
    public interface IPrinter
    {
        IPrinter With(RouteInfo routeInfo);
        void DisplayRoutes();
    }
}
using Shortest_Path.Models;

namespace Shortest_Path.Printer
{
    public interface IPrinter
    {
        IPrinter With(RouteInfo routeInfo);
        void DisplayRoutes();
    }
}
using OficinaDias.Endpoints;

namespace OficinaDias.Extensions;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapServiceOrderEndpoints();
    }
}

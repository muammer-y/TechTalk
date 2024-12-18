namespace Api.Endpoints.Abstract;

public interface IEndpoint
{
    void MapEndpoints(IEndpointRouteBuilder builder);
}

using Application.Abstractions.Messaging;
using Application.Mapping;
using Infrastructure.Data;
using Shared.Result;

namespace Application.Features.Users.Queries;

public sealed record GetUsersQuery : IQuery<List<GetUsersResponse>>;

public sealed record GetUsersResponse(int Id, string Email, string FirstName, string LastName);

internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<GetUsersResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetUsersResponse>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        var testUser = users.ToList().FirstOrDefault(p => p.Id == 1);
        // TODO: .Net 9 sonrası collection spesifik Find methodu, FirstOrDefault'a göre daha düşük performand sergiliyor. bkz:Span usage on base IEnumarable methods
        // Sonar kuralları güncellenince düzelecektir.

        var response = users.ToGetUsersResponse();

        return response;
    }
}

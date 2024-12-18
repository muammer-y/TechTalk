using Application.Abstractions.Messaging;
using Application.Mapping;
using Infrastructure.Data;
using Shared.Result;

namespace Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(int Id, string FirstName, string LastName) : ICommand<UpdateUserResponse>;

public record UpdateUserResponse(int Id, string FirstName, string LastName);

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (user is null)
        {
            return UserErrors.User_NotFound;
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return user.ToUpdateResponse();
    }
}

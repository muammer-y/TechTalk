using Application.Abstractions.Messaging;
using Infrastructure.Data;
using Shared.Result;

namespace Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : ICommand;

internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (user is null)
        {
            return UserErrors.User_NotFound;
        }

        _unitOfWork.UserRepository.Delete(user);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

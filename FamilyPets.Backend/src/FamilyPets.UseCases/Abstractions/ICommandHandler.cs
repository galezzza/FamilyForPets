
using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.UseCases.Abstractions
{
    public interface ICommandHandler<in TCommand, TResponse>
        where TCommand : ICommand
    {
        Task<Result<TResponse, ErrorList>> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<UnitResult<ErrorList>> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommand
    {
    }
}

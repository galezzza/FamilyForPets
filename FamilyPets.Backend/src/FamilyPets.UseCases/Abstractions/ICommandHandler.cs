
using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.UseCases.Abstractions
{
    public interface ICommandHandler<in TCommand, TResponse>
        where TCommand : ICommand
    {
        Task<Result<TResponse, Error>> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<UnitResult<Error>> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommand
    {
    }
}

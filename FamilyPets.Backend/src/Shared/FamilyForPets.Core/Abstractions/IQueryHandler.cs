using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Core.Abstractions
{
    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery
    {
        Task<Result<TResponse, ErrorList>> HandleAsync(
            TQuery query,
            CancellationToken cancellationToken);
    }

    public interface IQueryHandler<in TQuery>
        where TQuery : IQuery
    {
        Task<UnitResult<ErrorList>> HandleAsync(
            TQuery query,
            CancellationToken cancellationToken);
    }

    public interface IQuery
    {
    }
}

using System.Data.Common;

namespace FamilyForPets.Core.Database
{
    public interface IUnitOfWork
    {
        Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default);

        Task SaveChanges(CancellationToken cancellationToken = default);

    }
}

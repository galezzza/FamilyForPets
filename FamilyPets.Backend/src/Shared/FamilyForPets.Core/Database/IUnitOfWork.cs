using System.Data;
using System.Data.Common;

namespace FamilyForPets.Core.Database
{
    public interface IUnitOfWork
    {
        Task<DbTransaction> BeginTransaction(
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            CancellationToken cancellationToken = default);

        Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default);

        Task SaveChanges(CancellationToken cancellationToken = default);

    }
}

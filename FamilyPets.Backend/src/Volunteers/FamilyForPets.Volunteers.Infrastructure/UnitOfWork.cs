using System.Data.Common;
using FamilyForPets.Core.Database;
using FamilyForPets.Volunteers.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace FamilyForPets.Volunteers.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VolunteerWriteDbContext _dbContext;

        public UnitOfWork(VolunteerWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DbTransaction> BeginTransaction(
            CancellationToken cancellationToken = default)
        {
            var transaction = await _dbContext.Database
                .BeginTransactionAsync(cancellationToken);

            return transaction.GetDbTransaction();
        }

        public async Task SaveChanges(
            CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

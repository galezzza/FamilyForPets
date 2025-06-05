using CSharpFunctionalExtensions;
using FamilyForPets.Shared;
using FamilyForPets.Shared.ValueObjects;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FamilyForPets.Volunteers.UseCases;
using Microsoft.EntityFrameworkCore;

namespace FamilyForPets.Volunteers.Infrastructure.Repositories
{
    public class VolunteersRepository : IVolunteerRepository
    {
        private readonly VolunteerDbContext _dbContext;

        public VolunteersRepository(VolunteerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid, Error>> Add(Volunteer volunteer, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(volunteer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success<Guid, Error>(volunteer.Id.Value);
        }

        public async Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken)
        {
            Volunteer? volunteer = await _dbContext.Volunteers
                .Include(v => v.AllPets)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (volunteer == null)
            {
                return Result.Failure<Volunteer, Error>(Errors.General.NotFound(new (nameof(id), id)));
            }

            return Result.Success<Volunteer, Error>(volunteer);
        }

        public async Task<Result<Volunteer, Error>> GetByEmail(EmailAdress emailAdress, CancellationToken cancellationToken)
        {
            Volunteer? volunteer = await _dbContext.Volunteers
                .Include(v => v.AllPets)
                .FirstOrDefaultAsync(v => v.Email == emailAdress);

            if (volunteer == null)
            {
                return Result.Failure<Volunteer, Error>(Errors.General.NotFound(new(nameof(emailAdress), emailAdress)));
            }

            return Result.Success<Volunteer, Error>(volunteer);
        }

        public async Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken cancellationToken)
        {
            _dbContext.Volunteers.Attach(volunteer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success<Guid, Error>(volunteer.Id.Value);
        }
    }
}

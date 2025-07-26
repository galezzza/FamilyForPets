using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;

namespace FamilyForPets.Volunteers.UseCases
{
    public interface IVolunteerRepository
    {
        Task<Result<Guid, Error>> Add(Volunteer volunteer, CancellationToken cancellationToken);

        Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken);

        Task<Result<Volunteer, Error>> GetByEmail(EmailAdress email, CancellationToken cancellationToken);

        Task<Result<Guid, Error>> Delete(Volunteer volunteer, CancellationToken cancellationToken);
    }
}

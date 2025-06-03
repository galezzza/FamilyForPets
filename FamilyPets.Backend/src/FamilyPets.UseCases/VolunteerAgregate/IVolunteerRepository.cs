using CSharpFunctionalExtensions;
using FamilyForPets.Domain.SharedValueObjects;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Shared;

namespace FamilyForPets.UseCases.VolunteerAgregate
{
    public interface IVolunteerRepository
    {
        Task<Result<Guid, Error>> Add(Volunteer volunteer, CancellationToken cancellationToken);

        Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken);

        Task<Result<Volunteer, Error>> GetByEmail(EmailAdress email, CancellationToken cancellationToken);

        Task<Result<Guid, Error>> Save(Volunteer volunteer, CancellationToken cancellationToken);
    }
}

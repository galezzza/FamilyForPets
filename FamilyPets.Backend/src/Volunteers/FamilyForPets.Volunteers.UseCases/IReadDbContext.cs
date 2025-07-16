using FamilyForPets.Volunteers.Contracts.DTOs;
using FamilyForPets.Volunteers.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace FamilyForPets.Volunteers.UseCases
{
    public interface IReadDbContext
    {
        public IQueryable<VolunteerDTO> Volunteers { get; }

        public IQueryable<PetDTO> Pets { get; }
    }
}

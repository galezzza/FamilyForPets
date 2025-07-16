using System.Collections.Generic;
using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;
using FamilyForPets.SharedKernel;
using FamilyForPets.Volunteers.Contracts.Responses;
using FamilyForPets.Volunteers.Domain.Entities;

namespace FamilyForPets.Volunteers.UseCases.Queries.GetVolunteers
{
    public class GetVolunteersHandler : IQueryHandler<
        GetVolunteersQuery, IReadOnlyList<VolunteerDTO>>
    {
        private readonly IReadDbContext _readDbContext;

        public GetVolunteersHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<IReadOnlyList<VolunteerDTO>, ErrorList>> HandleAsync(
            GetVolunteersQuery query,
            CancellationToken cancellationToken)
        {
            IReadOnlyList<Volunteer> volunteers = [];
            ErrorList error = Errors.General.Failure().ToErrorList();

            var some = _readDbContext.Volunteers
                .Where(v => v.Id != Guid.Empty);

            IReadOnlyList<VolunteerDTO> result = volunteers
                .Select(VolunteerDTO.CreateFromEntity)
                .ToArray();

            return Result.Success<IReadOnlyList<VolunteerDTO>, ErrorList>(result);
        }
    }
}

using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;
using FamilyForPets.Volunteers.Contracts.Responses;
using FamilyForPets.Volunteers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

            var result = await _readDbContext.Volunteers
                .GroupJoin(
                    _readDbContext.Pets,
                    v => v.Id,
                    p => p.VolunteerId,
                    (v, p) => new
                    {
                        v,
                        p = p.Select(p => p.Id),
                    })
                .Select(vp => vp.v.AppendPets(vp.p.ToArray()))
                .ToListAsync();

            return Result.Success<IReadOnlyList<VolunteerDTO>, ErrorList>(result);
        }
    }
}

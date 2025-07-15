using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.UseCases.Queries.GetVolunteers
{
    public class GetVolunteersHandler : IQueryHandler<GetVolunteersQuery>
    {
        public async Task<UnitResult<ErrorList>> HandleAsync(
            GetVolunteersQuery query,
            CancellationToken cancellationToken)
        {
            return UnitResult.Success<ErrorList>();
        }
    }
}

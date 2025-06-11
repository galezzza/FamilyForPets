using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;
using FamilyForPets.Volunteers.Contracts.Requests.CreateVolunteer;
using FamilyForPets.Volunteers.Contracts.Requests.DeleteVolunteer;
using FamilyForPets.Volunteers.Contracts.Requests.UpdateVolunteer;
using FamilyForPets.Volunteers.Domain.Entities;

namespace FamilyForPets.Volunteers.Contracts
{
    public interface IVolunteersContract
    {
        public Task<Result<Guid, ErrorList>> Create(
            CreateVolunteerRequest request,
            CancellationToken cancellationToken);

        //public Task<Result<Volunteer, ErrorList>> GetById(
        //    Guid id,
        //    CancellationToken cancellationToken);

        public Task<Result<Guid, ErrorList>> UpdateSocialNewtworks(
            Guid id,
            UpdateVolunteerSocialNetworksRequest request,
            CancellationToken cancellationToken);

        public Task<Result<Guid, ErrorList>> UpdateDetailsForPayment(
            Guid id,
            UpdateVolunteerDetailsForPaymentRequest request,
            CancellationToken cancellationToken);

        public Task<Result<Guid, ErrorList>> UpdateContactData(
            Guid id,
            UpdateVolunteerContactDataRequest request,
            CancellationToken cancellationToken);

        public Task<Result<Guid, ErrorList>> UpdateMainInfoData(
            Guid id,
            UpdateVolunteerMainInfoRequest request,
            CancellationToken cancellationToken);

        public Task<Result<Guid, ErrorList>> Update(
            Guid id,
            UpdateVolunteerRequest request,
            CancellationToken cancellationToken);

        public Task<Result<Guid, ErrorList>> Delete(
            Guid id,
            DeleteVolunteerRequest request,
            CancellationToken cancellationToken);
    }
}

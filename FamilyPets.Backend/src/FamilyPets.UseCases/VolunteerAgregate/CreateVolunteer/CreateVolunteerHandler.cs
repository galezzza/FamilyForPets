
using CSharpFunctionalExtensions;
using FamilyForPets.Domain.SharedValueObjects;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Shared;
using FamilyForPets.UseCases.Abstractions;
using FluentValidation;

namespace FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer
{
    public class CreateVolunteerHandler : ICommandHandler<CreateVolunteerCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;

        public CreateVolunteerHandler(
            IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }

        public async Task<Result<Guid, Error>> HandleAsync(
            CreateVolunteerCommand command,
            CancellationToken cancellationToken)
        {
            // inputs shoud be validated before in calling method (aka in controller)
            FullName fullname = FullName.Create(command.FullName.Name, command.FullName.Surname, command.FullName.AdditionalName).Value;
            EmailAdress emailAdress = EmailAdress.Create(command.Email).Value;
            PhoneNumber phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
            DetailsForPayment detailsForPaymentess = DetailsForPayment.Create(command.PaymentDetails.CardNumber, command.PaymentDetails.OtherPaymentDetails).Value;

            // validate buisness logic
            Result<Volunteer, Error> volunteerFoundedByEmail = await _volunteerRepository.GetByEmail(emailAdress);
            if (volunteerFoundedByEmail.IsSuccess)
                return Result.Failure<Guid, Error>(Errors.Volunteer.ConflictAlreadyExists(nameof(EmailAdress)));

            // create domain entity
            Result<Volunteer, Error> volunteerToCreateResult = Volunteer.Create(
                fullname,
                emailAdress,
                command.ExperienceInYears,
                phoneNumber,
                detailsForPaymentess);

            if (volunteerToCreateResult.IsFailure)
                return Result.Failure<Guid, Error>(volunteerToCreateResult.Error);

            Volunteer volunteer = volunteerToCreateResult.Value;

            // database operations
            Result<Guid, Error> dbResult = await _volunteerRepository.Add(volunteer, cancellationToken);
            if (dbResult.IsFailure)
                return Result.Failure<Guid, Error>(dbResult.Error);

            // return
            return Result.Success<Guid, Error>(dbResult.Value);
        }
    }
}

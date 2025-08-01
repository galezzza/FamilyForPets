﻿using CSharpFunctionalExtensions;
using FamilyForPets.Core.DTOs;
using FamilyForPets.SharedKernel;
using FamilyForPets.Volunteers.Contracts;
using FamilyForPets.Volunteers.Contracts.Requests.CreateVolunteer;
using FamilyForPets.Volunteers.Contracts.Requests.UpdateVolunteer;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.UseCases.Commands.CreateVolunteer;
using FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerHard;
using FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerSoft;
using FamilyForPets.Volunteers.UseCases.Commands.GetVolunteerById;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerContactData;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerDetailsForPayment;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerMainInfo;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerSocialNetworks;

namespace FamilyForPets.Volunteers.API
{
    public class VolunteersContract : IVolunteersContract
    {
        private readonly GetVolunteerByIdHandler _getVolunteerByIdHandler;
        private readonly CreateVolunteerHandler _createVolunteerHandler;
        private readonly UpdateVolunteerMainInfoHandler _updateVolunteerMainInfoHandler;
        private readonly UpdateVolunteerContactDataHandler _updateVolunteerContactDataHandler;
        private readonly UpdateVolunteerSocialNetworksHandler _updateVolunteerSocialNetworksHandler;
        private readonly UpdateVolunteerDetailsForPaymentHandler _updateVolunteerDetailsForPaymentHandler;
        private readonly UpdateVolunteerHandler _updateVolunteerHandler;
        private readonly HardDeleteVolunteerHandler _hardDeleteVolunteerHandler;
        private readonly SoftDeleteVolunteerHandler _softDeleteVolunteerHandler;

        public VolunteersContract(
            GetVolunteerByIdHandler getVolunteerByIdHandler,
            CreateVolunteerHandler createVolunteerHandler,
            UpdateVolunteerMainInfoHandler updateVolunteerMainInfoHandler,
            UpdateVolunteerContactDataHandler updateVolunteerContactDataHandler,
            UpdateVolunteerSocialNetworksHandler updateVolunteerSocialNetworksHandler,
            UpdateVolunteerDetailsForPaymentHandler updateVolunteerDetailsForPaymentHandler,
            UpdateVolunteerHandler updateVolunteerHandler,
            HardDeleteVolunteerHandler hardDeleteVolunteerHandler,
            SoftDeleteVolunteerHandler softDeleteVolunteerHandler)
        {
            _getVolunteerByIdHandler = getVolunteerByIdHandler;
            _createVolunteerHandler = createVolunteerHandler;
            _updateVolunteerMainInfoHandler = updateVolunteerMainInfoHandler;
            _updateVolunteerContactDataHandler = updateVolunteerContactDataHandler;
            _updateVolunteerSocialNetworksHandler = updateVolunteerSocialNetworksHandler;
            _updateVolunteerDetailsForPaymentHandler = updateVolunteerDetailsForPaymentHandler;
            _updateVolunteerHandler = updateVolunteerHandler;
            _hardDeleteVolunteerHandler = hardDeleteVolunteerHandler;
            _softDeleteVolunteerHandler = softDeleteVolunteerHandler;
        }

        public async Task<Result<Guid, ErrorList>> Create(
            CreateVolunteerRequest request,
            CancellationToken cancellationToken)
        {
            CreateVolunteerCommand command = new CreateVolunteerCommand(
                new FullNameDto(
                    request.Name,
                    request.Surname,
                    request.AdditionalName),
                request.Email,
                request.ExperienceInYears,
                request.PhoneNumber,
                new PaymentDetailsDto(
                    request.CardNumber,
                    request.OtherPaymentDetails));

            return await _createVolunteerHandler.HandleAsync(command, cancellationToken);
        }

        // public async Task<Result<Volunteer, ErrorList>> GetById(
        //    Guid id,
        //    CancellationToken cancellationToken)
        // {
        //    GetVolunteerByIdCommand command = new(id);
        //    return await _getVolunteerByIdHandler.HandleAsync(command, cancellationToken);
        // }
        public async Task<Result<Guid, ErrorList>> UpdateContactData(
            Guid id, UpdateVolunteerContactDataRequest request,
            CancellationToken cancellationToken)
        {
            UpdateVolunteerContactDataCommand command = new UpdateVolunteerContactDataCommand(
                id, request.Email, request.PhoneNumber);

            return await _updateVolunteerContactDataHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<Guid, ErrorList>> UpdateDetailsForPayment(
            Guid id,
            UpdateVolunteerDetailsForPaymentRequest request,
            CancellationToken cancellationToken)
        {
            UpdateVolunteerDetailsForPaymentCommand command = new UpdateVolunteerDetailsForPaymentCommand(
                id, new(request.CardNumber, request.OtherDetails));

            return await _updateVolunteerDetailsForPaymentHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<Guid, ErrorList>> UpdateMainInfoData(
            Guid id,
            UpdateVolunteerMainInfoRequest request,
            CancellationToken cancellationToken)
        {
            UpdateVolunteerMainInfoCommand command = new UpdateVolunteerMainInfoCommand(
                id, new(request.Name, request.Surname, request.AdditionalName), request.Description);

            return await _updateVolunteerMainInfoHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<Guid, ErrorList>> UpdateSocialNewtworks(
            Guid id,
            UpdateVolunteerSocialNetworksRequest request,
            CancellationToken cancellationToken)
        {
            UpdateVolunteerSocialNetworksCommand command = new UpdateVolunteerSocialNetworksCommand(
                id, request.SocialNetworks);

            return await _updateVolunteerSocialNetworksHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<Guid, ErrorList>> Update(
            Guid id, UpdateVolunteerRequest request,
            CancellationToken cancellationToken)
        {
            UpdateVolunteerCommand command = new UpdateVolunteerCommand(
                id,
                request.SocialNetworks,
                new PaymentDetailsDto(request.CardNumber, request.OtherDetails),
                new FullNameDto(
                    request.Name, request.Surname, request.AdditionalName),
                request.Description,
                request.PhoneNumber, request.Email);

            return await _updateVolunteerHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<Guid, ErrorList>> DeleteSoft(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _hardDeleteVolunteerHandler.HandleAsync(new(id), cancellationToken);
        }

        public async Task<Result<Guid, ErrorList>> DeleteHard(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _softDeleteVolunteerHandler.HandleAsync(new(id), cancellationToken);
        }
    }
}

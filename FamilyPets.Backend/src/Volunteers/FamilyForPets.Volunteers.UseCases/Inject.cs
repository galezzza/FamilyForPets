﻿using FamilyForPets.Core.Abstractions;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.UseCases.CreateVolunteer;
using FamilyForPets.Volunteers.UseCases.DeleteVolunteer.DeleteVolunteerHard;
using FamilyForPets.Volunteers.UseCases.DeleteVolunteer.DeleteVolunteerSoft;
using FamilyForPets.Volunteers.UseCases.GetVolunteerById;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerContactData;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerDetailsForPayment;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerMainInfo;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerSocialNetworks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Volunteers.UseCases
{
    public static class Inject
    {
        public static IServiceCollection AddVolunteersUseCases(this IServiceCollection services)
        {
            services.AddScoped
                <ICommandHandler<CreateVolunteerCommand, Guid>, CreateVolunteerHandler>();

            services.AddScoped
                <ICommandHandler<GetVolunteerByIdCommand, Volunteer>, GetVolunteerByIdHandler>();

            services.AddScoped
                <ICommandHandler<UpdateVolunteerSocialNetworksCommand, Guid>,
                UpdateVolunteerSocialNetworksHandler>();

            services.AddScoped
                <ICommandHandler<UpdateVolunteerContactDataCommand, Guid>,
                UpdateVolunteerContactDataHandler>();

            services.AddScoped
                <ICommandHandler<UpdateVolunteerMainInfoCommand, Guid>,
                UpdateVolunteerMainInfoHandler>();

            services.AddScoped<ICommandHandler<UpdateVolunteerDetailsForPaymentCommand, Guid>,
                UpdateVolunteerDetailsForPaymentHandler>();

            services.AddScoped<ICommandHandler<UpdateVolunteerCommand, Guid>,
                UpdateVolunteerHandler>();

            services.AddScoped<ICommandHandler<HardDeleteVolunteerCommand, Guid>,
                HardDeleteVolunteerHandler>();

            services.AddScoped<ICommandHandler<SoftDeleteVolunteerCommand, Guid>,
                SoftDeleteVolunteerHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}

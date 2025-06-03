using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer;
using FamilyForPets.UseCases.VolunteerAgregate.GetVolunteerById;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerContactData;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerDetailsForPayment;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerMainInfo;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerSocialNetworks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.UseCases
{
    public static class Inject
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
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



            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}

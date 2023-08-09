using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Account;
using CampX.Code.Base;
using CampX.Common.ViewModels;
using System;
using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Trips;
using CampX.BusinessLogic.Implementations.Reviews;
using CampX.BusinessLogic.Implementations.Requests;

namespace CampX.Code.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ControllerDependencies>();

            return services;
        }

        public static IServiceCollection AddCampXBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<CamperAccountService>();
            services.AddScoped<CampsiteService>();
            services.AddScoped<TripService>();
            services.AddScoped<ReviewService>();
            services.AddScoped<RequestService>();
            // add new services

            return services;
        }

        public static IServiceCollection AddCampXCurrentCamper(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                var claims = httpContext.User.Claims;

                var camperIdClaim = claims?.SingleOrDefault(c => c.Type == "Id")?.Value;
                var isParsingSuccessful = int.TryParse(camperIdClaim, out int id);
                var camperEmailClaim = claims?.SingleOrDefault(c => c.Type == "Email")?.Value;
                var camperFirstNameClaim = claims?.SingleOrDefault(c => c.Type == "FirstName")?.Value;
                var camperLastNameClaim = claims?.SingleOrDefault(c => c.Type == "LastName")?.Value;
                return new CurrentCamperDTO
                {
                    Id = id
                    ,IsAuthenticated = httpContext.User.Identity.IsAuthenticated
                    ,Email = camperEmailClaim
                    ,FirstName =  camperFirstNameClaim
                    ,LastName =  camperLastNameClaim
                };
            });

            return services;
        }
    }
}

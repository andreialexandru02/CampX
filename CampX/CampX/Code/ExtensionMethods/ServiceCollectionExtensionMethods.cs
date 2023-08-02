using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Account;
using CampX.Code.Base;
using CampX.Common.ViewModels;
using System;


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

                var camperIdClaim = claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
                var isParsingSuccessful = int.TryParse(camperIdClaim, out int id);
                  
                return new CurrentCamperDTO
                {
                    Id = id,
                    IsAuthenticated = httpContext.User.Identity.IsAuthenticated,
                    Email = httpContext.User.Identity.Name
                };
            });

            return services;
        }
    }
}

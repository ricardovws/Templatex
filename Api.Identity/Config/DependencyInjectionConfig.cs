using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Identity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Identity.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            // Resolve another dependencies here!
            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }
    }
}

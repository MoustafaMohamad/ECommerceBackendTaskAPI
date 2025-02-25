using FluentValidation.AspNetCore;
using System.Reflection;

namespace ECommerceBackendTaskAPI.Configurations.DependencyInjection
{
    public static class FluentValidationConfig
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services, Assembly assembly)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}

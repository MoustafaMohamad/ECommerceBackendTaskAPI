namespace ECommerceBackendTaskAPI.Configurations.DependencyInjection
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });
            return services;
        }
    }
}

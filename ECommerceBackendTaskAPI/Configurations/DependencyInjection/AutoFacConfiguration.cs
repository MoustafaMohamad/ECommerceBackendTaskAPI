using Autofac.Extensions.DependencyInjection;

namespace ECommerceBackendTaskAPI.Configurations.DependencyInjection
{
    public static class AutoFacConfiguration
    {
        public static IHostBuilder UseAutoFac(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            hostBuilder.ConfigureContainer<ContainerBuilder>(builder =>
                builder.RegisterModule(new AutoFacModule()));
            return hostBuilder;
        }
    }
}

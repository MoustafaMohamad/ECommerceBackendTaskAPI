namespace ECommerceBackendTaskAPI.Common
{
    public class AutoFacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RequestParameters<>)).InstancePerLifetimeScope();
            builder.RegisterType<Context>().InstancePerLifetimeScope();
            builder.RegisterType<OrchestratorParameters>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationParameters>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}

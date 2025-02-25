
namespace ECommerceBackendTaskAPI.Configurations
{
    public static class SwagerConfigration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ECommerce Backend Task API",
                    Description = "An API for managing e-commerce backend tasks",

                });

                // Include XML comments if available
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }


                c.EnableAnnotations();
            });

            services.AddSwaggerExamplesFromAssemblyOf<Program>();  // Registers example providers


            return services;
        }
    }
}

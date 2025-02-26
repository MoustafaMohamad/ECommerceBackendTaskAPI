using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;

namespace ECommerceBackendTaskAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwagger()
                .AddMediatR()
                .AddAutoMapper(typeof(MappingProfile))
            .AddFluentValidation(Assembly.GetExecutingAssembly());

            builder.Host.UseAutoFac();

            builder.Logging.ClearProviders();

            #region Serilog Configuration 
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
               .WriteTo.MSSqlServer(connectionString: configuration.GetConnectionString("Default"), restrictedToMinimumLevel: LogEventLevel.Information,
               sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true, AutoCreateSqlDatabase = true })
               .WriteTo.Seq("http://localhost:5341/")
               .CreateLogger();

            builder.Host.UseSerilog();
            #endregion

            var app = builder.Build();
            MapperHelper.Mapper = app.Services.GetService<IMapper>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

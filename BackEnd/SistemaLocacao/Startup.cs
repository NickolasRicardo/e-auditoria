using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Repositories;

namespace SistemaLocacao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(
                options =>
                    options.AddPolicy(
                        "CorsPolicy",
                        builder =>
                        {
                            builder
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .SetIsOriginAllowed((host) => true)
                                .AllowCredentials();
                        }
                    )
            );
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            var connectionString = Configuration.GetConnectionString("default");

            services.AddDbContext<DatabaseContext>(
                dbContextOptions =>
                    dbContextOptions
                        .UseMySql(
                            connectionString,
                            serverVersion,
                            options => options.EnableRetryOnFailure()
                        )
                        .EnableDetailedErrors()
            );

            services.AddControllers();
            services
                .AddControllers()
                .AddJsonOptions(
                    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<ClienteRepositories>();
            services.AddScoped<FilmeRepositories>();
            services.AddScoped<LocacaoRepositories>();
            services.AddScoped<RelatoriosRepositories>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}

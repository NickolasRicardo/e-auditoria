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
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            var connectionString = Configuration.GetConnectionString("default");

            services.AddDbContext<DatabaseContext>(
                dbContextOptions =>
                    dbContextOptions
                        .UseMySql(connectionString, serverVersion)
                        .EnableDetailedErrors()
            );

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<ClienteRepositories>();
            services.AddScoped<FilmeRepositories>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}

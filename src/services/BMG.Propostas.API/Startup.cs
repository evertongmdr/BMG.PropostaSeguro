using BMG.Propostas.API.Configuration;
using BMG.WebAPI.Core.Extensions;

namespace BMG.Propostas.API
{
    public class Startup : IAppStartup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);

            services.AddSwaggerConfiguration();

            services.RegisterServices();

        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(env);
        }
    }
}

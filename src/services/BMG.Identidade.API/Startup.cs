using BMG.Identidade.API.Configuration;
using BMG.Identidade.Application.AutoMapper;
using BMG.WebAPI.Core.Extensions;

namespace BMG.Identidade.API
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

            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.RegisterServices();


        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(env);
        }
    }
}

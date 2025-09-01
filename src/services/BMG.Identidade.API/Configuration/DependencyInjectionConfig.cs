using BMG.Core.Notifications;
using BMG.Identidade.Application.Interfaces;
using BMG.Identidade.Application.Sevices;
using BMG.Identidade.Domain.Interfaces.Repositories;
using BMG.Identidade.Infra.Data;
using BMG.Identidade.Infra.Data.Repositories;

namespace BMG.Identidade.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<NotificationContext>();

            // Application
            services.AddScoped<IIdentidadeService, IdentidadeService>();

            // Data
            services.AddScoped<IdentidadeContext>();
            services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}

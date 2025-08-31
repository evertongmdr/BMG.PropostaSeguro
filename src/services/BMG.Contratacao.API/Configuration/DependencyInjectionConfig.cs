using BMG.Contratacao.Application.Interfaces;
using BMG.Contratacao.Application.Services;
using BMG.Contratacao.Domain.Interfaces.Repositories;
using BMG.Contratacao.Infra.Data;
using BMG.Contratacao.Infra.Data.Repositories;
using BMG.Core.Notifications;

namespace BMG.Contratacao.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<NotificationContext>();

            // Application
            services.AddScoped<IContratacaoService, ContratacaoService>();

            // Data
            services.AddScoped<ContratacaoContext>();
            services.AddScoped<IContratacaoRepository, ContratacaoRepository>();

        }
    }
}

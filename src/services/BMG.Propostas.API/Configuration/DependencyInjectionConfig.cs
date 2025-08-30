using BMG.Core.Notifications;
using BMG.Propostas.Application.Interfaces;
using BMG.Propostas.Application.Services;
using BMG.Propostas.Domain.Interfaces.Repositories;
using BMG.Propostas.Infra.Data;

namespace BMG.Propostas.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<NotificationContext>();

            // Application
            services.AddScoped<IPropostaService, PropostaService>();

            // Data
            services.AddScoped<PropostaContext>();
            services.AddScoped<IPropostaRepository, PropostaRepository>();

        }
    }
}


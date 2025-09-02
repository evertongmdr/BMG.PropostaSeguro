using BMG.Bff.Seguros.Application;
using BMG.Bff.Seguros.Services;
using BMG.Core.Notifications;

namespace BMG.Bff.Seguros.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();

            //Application
            services.AddScoped<IPropostaAppService, PropostaAppService>();
            services.AddScoped<IContratacaoAppService, ContratacaoAppService>();
            services.AddScoped<IIdentidadeAppService, IdentidadeAppService>();

            //Service
            services.AddHttpClient<IPropostaService, PropostaService>();
            services.AddHttpClient<IContratacaoService, ContratacaoService>();
            services.AddHttpClient<IIdentidadeService, IdentidadeService>();

        }
    }
}

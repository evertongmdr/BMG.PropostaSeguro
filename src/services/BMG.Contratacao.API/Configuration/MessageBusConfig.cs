
using BMG.Contratacao.Application.Services;
using BMG.Core.Utils;
using BMG.MessageBus;

namespace BMG.Contratacao.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<ContratacaoIntegrationHandler>();
        }
    }
}

using BMG.Contratacao.Domain.Entities;
using BMG.Contratacao.Domain.Interfaces.Repositories;
using BMG.Core.DomainObjects;
using BMG.Core.Messages.Integrations;
using BMG.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BMG.Contratacao.Application.Services
{
    public class ContratacaoIntegrationHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _bus;

        public ContratacaoIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetConsumers();
            return Task.CompletedTask;
        }

        private void SetConsumers()
        {
            _bus.Consumer<RealizarContratacaoIntegrationEvent>("ContratacaoSeguro",
                async request => await RealizarContratacao(request));

        }

        private async Task RealizarContratacao(RealizarContratacaoIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var contratacaoRepository = scope.ServiceProvider.GetRequiredService<IContratacaoRepository>();

            var contratacao = new ContratacaoSeguro
            {
                PropostaId = message.PropostaId,
                ContratenteId = message.ContratenteId,
                DataContratacao = DateTime.UtcNow
            };

            contratacaoRepository.Adicionar(contratacao);

            if (!await contratacaoRepository.UnitOfWork.Commit())
            {
                throw new DomainException($"Falha ao salvar a contratação, proposta: {message.PropostaId}, contratante: {message.ContratenteId}.");
            }

        }
    }
}

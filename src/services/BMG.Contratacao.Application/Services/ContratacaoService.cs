using BMG.Contratacao.Application.Interfaces;
using BMG.Contratacao.Domain.DTOs;
using BMG.Contratacao.Domain.Entities;
using BMG.Contratacao.Domain.Interfaces.Repositories;
using BMG.Core.DTOs;
using BMG.Core.Messages.Integrations;
using BMG.Core.Notifications;
using BMG.MessageBus;
using Microsoft.Extensions.Logging;

namespace BMG.Contratacao.Application.Services
{
    public class ContratacaoService : ErrorNotifier, IContratacaoService
    {
        private readonly ILogger<ContratacaoService> _logger;
        private readonly IMessageBus _bus;
        private readonly IContratacaoRepository _contratacaoRepository;
        public ContratacaoService(NotificationContext notificationContext, ILogger<ContratacaoService> logger, IMessageBus bus, IContratacaoRepository contratacaoRepository) : base(notificationContext)
        {
            _logger = logger;
            _bus = bus;

            _contratacaoRepository = contratacaoRepository;
        }

        public async Task<PagedResult<ContratacaoSeguro>> ObterContratacoesAsync(ContratacaoQueryParametersDTO contracaoQueryParameters)
        {
            return await _contratacaoRepository.ObterContratacoesAsync(contracaoQueryParameters);
        }

        public async Task ContratarPropostaAsync(CriarContratacaoDTO contratacao)
        {

            _logger.LogInformation("Etapa 1 - Validando a contratação.");

            if (contratacao.PropostaId == Guid.Empty)
            {
                _notificationContext.AddNotification($"A proposta de seguro deve ser informado.");
                return;
            }

            if (contratacao.ContratenteId == Guid.Empty)
            {
                _notificationContext.AddNotification($"O contratante do seguro deve ser informado.");
                return;
            }

            _logger.LogInformation("Validação concluída com sucesso.");


            var realizarContratacaoIntegrationEvent = new RealizarContratacaoIntegrationEvent
            {
                PropostaId = contratacao.PropostaId,
                ContratenteId = contratacao.ContratenteId
            };

            _logger.LogInformation("Etapa 2 - Publicando contratação na fila.");

            await _bus.EnqueueAsync("ContratacaoSeguro", realizarContratacaoIntegrationEvent);
        }
    }
}

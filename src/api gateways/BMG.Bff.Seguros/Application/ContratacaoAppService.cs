using BMG.Bff.Seguros.Models.Contratacao;
using BMG.Bff.Seguros.Models.Proposta;
using BMG.Bff.Seguros.Services;
using BMG.Core.DTOs;
using BMG.Core.Notifications;

namespace BMG.Bff.Seguros.Application
{
    public interface IContratacaoAppService
    {
        public Task ContratarPropostaAsync(RegistrarContratacaoDTO contratacao);
        public Task<PagedResult<ContratacaoDTO>> ObterContratacoesAsync(ContratacaoQueryParametersDTO contracaoQueryParameters);
    }
    public class ContratacaoAppService : ErrorNotifier, IContratacaoAppService
    {
        private readonly IContratacaoService _contratacaoService;
        private readonly IPropostaService _propostaService;
        private readonly IIdentidadeService _identidadeService;

        public ContratacaoAppService(NotificationContext notificationContext, IContratacaoService contratacaoService, IPropostaService propostaService, IIdentidadeService identidadeService) : base(notificationContext)
        {
            _contratacaoService = contratacaoService;
            _propostaService = propostaService;
            _identidadeService = identidadeService;
        }

        public async Task ContratarPropostaAsync(RegistrarContratacaoDTO contratacao)
        {
            var respostaApiIdentidade = await _identidadeService.ObterUsuarioPorIdAsync(contratacao.ContratenteId);

            if (!respostaApiIdentidade.Success)
            {

                if (respostaApiIdentidade.ResponseResult.Status == StatusCodes.Status404NotFound)
                {
                    _notificationContext.AddNotification("O Contratente da proposta não foi encontrado.");
                    return;
                }

                _notificationContext.AddNotification(respostaApiIdentidade.ResponseResult);
                return;
            }

            var respostaApiProposta = await _propostaService.ObterPropostaPorIdAsync(contratacao.PropostaId);

            if (!respostaApiProposta.Success)
            {
                _notificationContext.AddNotification(respostaApiProposta.ResponseResult);
                return;
            }

            var proposta = respostaApiProposta.Data;

            if (proposta.Status != PropostaStatus.Aprovada)
            {
                _notificationContext.AddNotification("A proposta só pode ser contratada quando estiver com status 'Aprovada'.");
                return;
            }

            var respotaApiContratacao = await _contratacaoService.ContratarPropostaAsync(contratacao);

            if (!respotaApiContratacao.Success)
            {
                _notificationContext.AddNotification(respotaApiContratacao.ResponseResult);
                return;
            }

        }

        public async Task<PagedResult<ContratacaoDTO>> ObterContratacoesAsync(ContratacaoQueryParametersDTO contracaoQueryParameters)
        {
            var respostaApiContratacao = await _contratacaoService.ObterContratacoesAsync(contracaoQueryParameters);

            if (!respostaApiContratacao.Success)
            {
                _notificationContext.AddNotification(respostaApiContratacao.ResponseResult);
                return null;
            }

            return respostaApiContratacao.Data;
        }
    }
}

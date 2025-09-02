using BMG.Bff.Seguros.Models.Proposta;
using BMG.Bff.Seguros.Services;
using BMG.Core.DTOs;
using BMG.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace BMG.Bff.Seguros.Application
{

    public interface IPropostaAppService
    {
        Task<Guid> CriarPropostaAsync([FromBody] RegistrarPropostaDTO proposta);
        public Task AtualizarStatusPropostaAsync(Guid propostaid, AtualizarStatusPropostaDTO atualizarStatusPropostaDTO);
        public Task<PagedResult<PropostaDTO>> ObterPropostasAsync(PropostaQueryParametersDTO propostaQueryParameters);

    }

    public class PropostaAppService : ErrorNotifier, IPropostaAppService
    {
        private readonly IPropostaService _propostaService;
        private readonly IIdentidadeService _identidadeService;

        public PropostaAppService(NotificationContext notificationContext, IPropostaService propostaService, IIdentidadeService identidadeService) : base(notificationContext)
        {
            _propostaService = propostaService;
            _identidadeService = identidadeService;

        }

        public async Task<Guid> CriarPropostaAsync([FromBody] RegistrarPropostaDTO proposta)
        {

            var respostaApiUsuario = await _identidadeService.ObterUsuarioPorIdAsync(proposta.CriadoPorUsuarioId);

            if (!respostaApiUsuario.Success)
            {
                if (respostaApiUsuario.ResponseResult.Status == StatusCodes.Status404NotFound)
                {
                    _notificationContext.AddNotification("O Usuário que está criando a proposta não foi encontrado.");
                    return Guid.Empty;
                }

                _notificationContext.AddNotification(respostaApiUsuario.ResponseResult);
                return Guid.Empty;
            }

            var respostaApiProposta = await _propostaService.CriarProposta(proposta);

            if (!respostaApiProposta.Success)
            {
                _notificationContext.AddNotification(respostaApiProposta.ResponseResult);
                return Guid.Empty;
            }

            return respostaApiProposta.Data;
        }

        public async Task AtualizarStatusPropostaAsync(Guid propostaid, AtualizarStatusPropostaDTO atualizarStatusPropostaDTO)
        {
            var respostaApiProposta = await _propostaService.AtualizarStatusPropostaAsync(propostaid, atualizarStatusPropostaDTO);

            if (!respostaApiProposta.Success)
            {
                _notificationContext.AddNotification(respostaApiProposta.ResponseResult);
                return;
            }

            return;
        }

        public async Task<PagedResult<PropostaDTO>> ObterPropostasAsync(PropostaQueryParametersDTO propostaQueryParameters)
        {
            var respostaApiProposta = await _propostaService.ObterPropostasAsync(propostaQueryParameters);

            if (!respostaApiProposta.Success)
            {
                _notificationContext.AddNotification(respostaApiProposta.ResponseResult);
                return null;
            }

            return respostaApiProposta.Data;
        }
    }
}

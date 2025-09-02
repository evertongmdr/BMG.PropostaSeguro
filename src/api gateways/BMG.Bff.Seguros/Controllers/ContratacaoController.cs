using BMG.Bff.Seguros.Application;
using BMG.Bff.Seguros.Models.Contratacao;
using BMG.Bff.Seguros.Models.Proposta;
using BMG.Core.Communication;
using BMG.Core.DTOs;
using BMG.Core.Notifications;
using BMG.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BMG.Bff.Seguros.Controllers
{
    public class ContratacaoController : MainController
    {
        private readonly IContratacaoAppService _contratacaoAppService;

        public ContratacaoController(NotificationContext notificationContext, IContratacaoAppService contratacaoAppService) : base(notificationContext)
        {
            _contratacaoAppService = contratacaoAppService;
        }

        [HttpPost("seguro/contratar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ContratarPropostaAsync([FromBody] RegistrarContratacaoDTO contratacao)
        {
            await _contratacaoAppService.ContratarPropostaAsync(contratacao);

            return CustomResponse();
        }

        [HttpGet("seguro/contrato/lista")]
        [ProducesResponseType(typeof(PagedResult<ContratacaoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ObterContracoes([FromQuery] ContratacaoQueryParametersDTO contracaoQueryParameters)
        {
            return CustomResponse(await _contratacaoAppService.ObterContratacoesAsync(contracaoQueryParameters));
        }
    }
}

using BMG.Bff.Seguros.Application;
using BMG.Bff.Seguros.Models.Proposta;
using BMG.Core.Communication;
using BMG.Core.DTOs;
using BMG.Core.Notifications;
using BMG.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BMG.Bff.Seguros.Controllers
{
    public class PropostaController : MainController
    {
        private readonly IPropostaAppService _propostaAppService;

        public PropostaController(
            NotificationContext notificationContext,
            IPropostaAppService propostaAppService)
            : base(notificationContext)
        {
            _propostaAppService = propostaAppService;
        }

        [HttpPost("seguro/proposta")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarProposta([FromBody] RegistrarPropostaDTO proposta)
        {
            var propostaId = await _propostaAppService.CriarPropostaAsync(proposta);

            return CustomResponse(propostaId);
        }

        [HttpPut("seguro/proposta/atualizar-status/{propostaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarStatus(Guid propostaId, [FromBody] AtualizarStatusPropostaDTO atualizarStatusDTO)
        {
            await _propostaAppService.AtualizarStatusPropostaAsync(propostaId, atualizarStatusDTO);

            return CustomResponse(propostaId);
        }

        [HttpGet("proposta/lista")]
        [ProducesResponseType(typeof(PagedResult<PropostaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ObterPropostas([FromQuery] PropostaQueryParametersDTO propostaQueryParameters)
        {
            return CustomResponse(await _propostaAppService.ObterPropostasAsync(propostaQueryParameters));
        }

    }
}

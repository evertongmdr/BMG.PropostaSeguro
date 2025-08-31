using BMG.Core.Communication;
using BMG.Core.DTOs;
using BMG.Core.Notifications;
using BMG.Propostas.Application.Interfaces;
using BMG.Propostas.Application.Validators;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;
using BMG.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BMG.Propostas.API.Controllers
{
    public class PropostaController : MainController
    {

        private readonly IPropostaService _propostaService;
        public PropostaController(NotificationContext notificationContext, IPropostaService propostaService) : base(notificationContext)

        {
            _propostaService = propostaService;
        }

        [HttpPost("proposta")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CriarProposta([FromBody] CriarPropostaRequestDTO proposta)
        {
            var validator = new CriarPropostaDtoValidator();
            var validatorResult = await validator.ValidateAsync(proposta);

            if (!validatorResult.IsValid)
                return CustomResponse(validatorResult);

            var propostaId = await _propostaService.CriarPropostaAsync(proposta);

            return CustomResponse(propostaId);
        }

        [HttpGet("proposta/lista")]
        [ProducesResponseType(typeof(PagedResult<Proposta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ObterPropostas([FromQuery]PropostaQueryParametersDTO propostaQueryParameters)
        {
            return CustomResponse(await _propostaService.ObterPropostasAsync(propostaQueryParameters));
        }

        [HttpPut("proposta/atualizar-status/{propostaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarStatus(Guid propostaId, [FromBody] AtualizarStatusPropostaDTO proposta)
        {

            if (propostaId == Guid.Empty)
            {
                AdicionarErroProcessamento("O Id da proposta é inválido.");
                return CustomResponse();
            }

            var validator = new AtualizarStatusPropostaDtoValidator();
            var validatorResult = await validator.ValidateAsync(proposta);

            if (!validatorResult.IsValid)
                return CustomResponse(validatorResult);

            await _propostaService.AtualizarStatusPropostaAsync(propostaId, proposta);

            return CustomResponse();

        }
    }
}

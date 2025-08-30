using BMG.Core.Notifications;
using BMG.Propostas.Application.Interfaces;
using BMG.Propostas.Application.Validators;
using BMG.Propostas.Domain.DTOs;
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
        public async Task<IActionResult> CriarProposta([FromBody] CriarPropostaRequestDTO proposta)
        {
            var validator = new CriarPropostaDtoValidator();
            var validatorResult = await validator.ValidateAsync(proposta);

            if (!validatorResult.IsValid)
                return CustomResponse(validatorResult);

            var propostaId = _propostaService.CriarProposta(proposta);

            return CustomResponse(propostaId);
        }

        [HttpPut("proposta/atualizar-status/{propostaId}")]
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

            await _propostaService.AtualizarStatusProposta(propostaId, proposta);

            return CustomResponse();

        }
    }
}

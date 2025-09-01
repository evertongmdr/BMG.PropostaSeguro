using BMG.Contratacao.Application.Interfaces;
using BMG.Contratacao.Application.Validators;
using BMG.Contratacao.Domain.DTOs;
using BMG.Contratacao.Domain.Entities;
using BMG.Core.Communication;
using BMG.Core.DTOs;
using BMG.Core.Notifications;
using BMG.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BMG.Contratacao.API.Controllers
{
    public class ContratacaoController : MainController
    {
        private readonly IContratacaoService _contratacaoService;
        public ContratacaoController(NotificationContext notificationContext, IContratacaoService contratacaoService) : base(notificationContext)
        {
            _contratacaoService = contratacaoService;
        }

        [HttpPost("contratacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ContratarProposta([FromBody] CriarContratacaoDTO contratacao)
        {
            var validator = new CriarContratacaoDtoValidator();
            var validatorResult = await validator.ValidateAsync(contratacao);

            if (!validatorResult.IsValid)
                return CustomResponse(validatorResult);

            await _contratacaoService.ContratarPropostaAsync(contratacao);

            return CustomResponse();
        }

        [HttpGet("contratacao/lista")]
        [ProducesResponseType(typeof(PagedResult<ContratacaoSeguro>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ObterPropostas([FromQuery] ContratacaoQueryParametersDTO contracaoQueryParameters)
        {
            return CustomResponse(await _contratacaoService.ObterContratacoesAsync(contracaoQueryParameters));
        }
    }
}

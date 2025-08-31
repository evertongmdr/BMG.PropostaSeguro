using BMG.Contratacao.Application.Interfaces;
using BMG.Contratacao.Application.Validators;
using BMG.Contratacao.Domain.DTOs;
using BMG.Core.Communication;
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

        public async Task<IActionResult> ContratarProposta([FromBody] CriarContratacaoRequestDTO contratacao)
        {
            var validator = new CriarContratacaoDtoValidator();
            var validatorResult = await validator.ValidateAsync(contratacao);

            if (!validatorResult.IsValid)
                return CustomResponse(validatorResult);

            await _contratacaoService.ContratarPropostaAsync(contratacao);

            return CustomResponse();
        }
    }
}

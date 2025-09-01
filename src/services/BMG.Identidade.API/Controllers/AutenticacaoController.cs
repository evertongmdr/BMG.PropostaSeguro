using BMG.Core.Communication;
using BMG.Core.DTOs;
using BMG.Core.Notifications;
using BMG.Identidade.Application.Interfaces;
using BMG.Identidade.Application.Validators;
using BMG.Identidade.Domain.DTOs;
using BMG.Identidade.Domain.Entities;
using BMG.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BMG.Identidade.API.Controllers
{
    [Route("api/identidade")]
    public class AutenticacaoController : MainController
    {
        private readonly IIdentidadeService _IdentidadeService;
        public AutenticacaoController(NotificationContext notificationContext, IIdentidadeService IdentidadeService) : base(notificationContext)
        {
            _IdentidadeService = IdentidadeService;
        }

        [HttpPost("nova-conta")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Registrar(RegistrarUsuarioDTO usuario)
        {

            var validator = new RegistrarUsuarioDtoValidator();
            var validatorResult = await validator.ValidateAsync(usuario);

            if (!validatorResult.IsValid)
                return CustomResponse(validatorResult);

            var propostaId = await _IdentidadeService.RegistrarUsuario(usuario);

            return CustomResponse(propostaId);
        }

        [HttpGet("usuario/{id}")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> ObterUsuario(Guid id)
        {
            var usuario = await _IdentidadeService.ObterPorIdAsync(id);

            return usuario == null ? NotFound() : CustomResponse(usuario);
        }

        [HttpGet("usuario/lista")]
        [ProducesResponseType(typeof(PagedResult<Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ObterUsuarios([FromQuery] UsuarioQueryParametersDTO propostaQueryParameters)
        {
            return CustomResponse(await _IdentidadeService.ObterUsuariosAsync(propostaQueryParameters));
        }
    }
}

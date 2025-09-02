using BMG.Bff.Seguros.Application;
using BMG.Bff.Seguros.Models.Identidade;
using BMG.Core.Communication;
using BMG.Core.DTOs;
using BMG.Core.Notifications;
using BMG.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BMG.Bff.Seguros.Controllers
{
    public class IdentidadeController : MainController
    {
        private readonly IIdentidadeAppService _identidadeAppService;

        public IdentidadeController(NotificationContext notificationContext, IIdentidadeAppService identidadeAppService) : base(notificationContext)
        {
            _identidadeAppService = identidadeAppService;
        }

        [HttpPost("seguro/usuario/nova-conta")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDTO usuario)
        {
            var usuarioId = await _identidadeAppService.RegistrarUsuarioAsync(usuario);

            return CustomResponse(usuarioId);
        }

        [HttpGet("seguro/usuario/lista")]
        [ProducesResponseType(typeof(PagedResult<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ObterUsuarios([FromQuery] UsuarioQueryParametersDTO usuarioQueryParameters)
        {
            return CustomResponse(await _identidadeAppService.ObterUsuariosAsync(usuarioQueryParameters));
        }
    }
}

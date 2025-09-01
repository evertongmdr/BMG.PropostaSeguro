using BMG.Bff.Seguros.Models.Proposta;
using BMG.Bff.Seguros.Services;
using BMG.Core.Communication;
using BMG.Core.Notifications;
using BMG.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BMG.Bff.Seguros.Controllers
{
    public class PropostaController : MainController
    {
        private readonly IPropostaService _propostaService;
        private readonly IIdentidadeService _identidadeService;

        public PropostaController(
            NotificationContext notificationContext, 
            IPropostaService propostaService, 
          
            IIdentidadeService identidadeService) 
            : base(notificationContext)
        {
            _propostaService = propostaService;
            _identidadeService = identidadeService;
        }

        [HttpPost("seguro/proposta")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CriarProposta([FromBody] RegistrarPropostaDTO proposta)
        {
           
            var usuarioExiste = await _identidadeService.ObterUsuarioPorIdAsync(proposta.CriadoPorUsuarioId); 

            if (usuarioExiste == null)
            {
                AdicionarErroProcessamento("Usuário não encontrado.");
                return CustomResponse();
            }

            var resposta = await _propostaService.CriarProposta(proposta);

            if (!resposta.Success)
                return CustomResponse(resposta.ResponseResult);

            return CustomResponse(resposta.Data);
        }
    }
}

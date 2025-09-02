using BMG.Bff.Seguros.Models.Identidade;
using BMG.Bff.Seguros.Services;
using BMG.Core.DTOs;
using BMG.Core.Notifications;

namespace BMG.Bff.Seguros.Application
{

    public interface IIdentidadeAppService
    {
        Task<Guid> RegistrarUsuarioAsync(RegistrarUsuarioDTO registrarUsuario);
        Task<PagedResult<UsuarioDTO>> ObterUsuariosAsync(UsuarioQueryParametersDTO usuarioQueryParameters);
    }

    public class IdentidadeAppService : ErrorNotifier, IIdentidadeAppService
    {
        private readonly IIdentidadeService _identidadeService;
        public IdentidadeAppService(NotificationContext notificationContext, IIdentidadeService identidadeService) : base(notificationContext)
        {
            _identidadeService = identidadeService;
        }


        public async Task<Guid> RegistrarUsuarioAsync(RegistrarUsuarioDTO registrarUsuario)
        {
            var respostaApiIdentidade = await _identidadeService.RegistrarUsuario(registrarUsuario);

            if (!respostaApiIdentidade.Success)
            {
                _notificationContext.AddNotification(respostaApiIdentidade.ResponseResult);
                return Guid.Empty;
            }

            return respostaApiIdentidade.Data;
        }

        public async Task<PagedResult<UsuarioDTO>> ObterUsuariosAsync(UsuarioQueryParametersDTO usuarioQueryParameters)
        {
            var respostaApiIdentidade = await _identidadeService.ObterUsuariosAsync(usuarioQueryParameters);

            if (!respostaApiIdentidade.Success)
            {
                _notificationContext.AddNotification(respostaApiIdentidade.ResponseResult);
                return null;
            }

            return respostaApiIdentidade.Data;
        }
    }
}

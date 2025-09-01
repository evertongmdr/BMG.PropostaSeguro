using BMG.Bff.Seguros.Models.Identidade;
using BMG.Core.DTOs;

namespace BMG.Bff.Seguros.Services
{

    public interface IIdentidadeService
    {
        Task<UsuarioDTO> ObterUsuarioPorIdAsync(Guid id);
        Task<PagedResult<UsuarioDTO>> ObterUsuariosAsync(UsuarioQueryParametersDTO usuarioQueryParameters);
        Task<Guid> RegistrarUsuario(RegistrarUsuarioDTO registrarUsuario);
    }

    public class IdentidadeService
    {
    }
}

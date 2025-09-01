using BMG.Core.DTOs;
using BMG.Identidade.Domain.DTOs;
using BMG.Identidade.Domain.Entities;

namespace BMG.Identidade.Application.Interfaces
{
    public interface IIdentidadeService
    {
        Task<Usuario> ObterPorIdAsync(Guid id);
        Task<PagedResult<Usuario>> ObterUsuariosAsync(UsuarioQueryParametersDTO usuarioQueryParameters);
        Task<Guid> RegistrarUsuario(RegistrarUsuarioDTO registrarUsuario);
    }
}

using BMG.Core.Data;
using BMG.Core.DTOs;
using BMG.Identidade.Domain.DTOs;
using BMG.Identidade.Domain.Entities;

namespace BMG.Identidade.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<Usuario>
    {
        Task<Usuario> ObterPorIdAsync(Guid id);
        Task<Usuario> ObterPorLoginAsync(string login);

        Task<PagedResult<Usuario>> ObterUsuariosAsync(UsuarioQueryParametersDTO usuarioQueryParameters);
        void Adicionar(Usuario usuario);
    }
}

using BMG.Core.Data;
using BMG.Core.DTOs;
using BMG.Identidade.Domain.DTOs;
using BMG.Identidade.Domain.Entities;
using BMG.Identidade.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BMG.Identidade.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentidadeContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(IdentidadeContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> ObterPorLoginAsync(string login)
        {
            return await _context.Usuarios
                             .FirstOrDefaultAsync(p => p.Login == login);
        }

        public async Task<PagedResult<Usuario>> ObterUsuariosAsync(UsuarioQueryParametersDTO usuarioQueryParameters)
        {
            var query = _context.Usuarios.AsNoTracking().AsQueryable();

            var quantidadeTotal = await query.CountAsync();

            int numeroPagina = usuarioQueryParameters.NumeroPagina,
                tamanhoPagina = usuarioQueryParameters.TamanhoPagina;

            var items = await query
                .Skip((numeroPagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return new PagedResult<Usuario>(items, quantidadeTotal, numeroPagina, tamanhoPagina);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

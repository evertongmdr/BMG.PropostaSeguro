using BMG.Core.Data;
using BMG.Core.DTOs;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;
using BMG.Propostas.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BMG.Propostas.Infra.Data
{
    public class PropostaRepository : IPropostaRepository
    {
        private readonly PropostaContext _context;
        public IUnitOfWork UnitOfWork => _context;


        public PropostaRepository(PropostaContext context)
        {
            _context = context;
        }

        public async Task<Proposta> ObterPorIdAsync(Guid id)
        {
            return await _context.Propostas.FindAsync(id);
        }

        public async Task<Proposta> ObterPorNumeroAsync(int numeroProposta)
        {
            return await _context.Propostas
                              .FirstOrDefaultAsync(p => p.NumeroProposta == numeroProposta);
        }

        public async Task<PagedResult<Proposta>> ObterPropostasAsync(PropostaQueryParametersDTO propostaQueryParameters)
        {
            var query = _context.Propostas.AsNoTracking().AsQueryable();

            if (propostaQueryParameters.NumeroProposta .HasValue)
                query = query.Where(p => p.NumeroProposta == propostaQueryParameters.NumeroProposta);

            if (!string.IsNullOrEmpty(propostaQueryParameters.Titulo))
                query = query.Where(p => EF.Functions.Like(p.Titulo, $"%{propostaQueryParameters.Titulo}%"));

            var quantidadeTotal = await query.CountAsync();

            int numeroPagina = propostaQueryParameters.NumeroPagina,
                tamanhoPagina = propostaQueryParameters.TamanhoPagina;

            var items = await query
                .Skip((numeroPagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return new PagedResult<Proposta>(items, quantidadeTotal, numeroPagina, tamanhoPagina);
        }

        public void Adicionar(Proposta proposta)
        {
            _context.Propostas.Add(proposta);
        }

        public void Atualizar(Proposta proposta)
        {
            _context.Propostas.Update(proposta);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

using BMG.Contratacao.Domain.DTOs;
using BMG.Contratacao.Domain.Entities;
using BMG.Contratacao.Domain.Interfaces.Repositories;
using BMG.Core.Data;
using BMG.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BMG.Contratacao.Infra.Data.Repositories
{
    public class ContratacaoRepository : IContratacaoRepository
    {
        private readonly ContratacaoContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ContratacaoRepository(ContratacaoContext context)
        {
            _context = context;
        }

        public async Task<ContratacaoSeguro> ObterPorIdAsync(Guid id)
        {
            return await _context.Contratacoes.FindAsync(id);
        }

        public async Task<PagedResult<ContratacaoSeguro>> ObterContratacoesAsync(ContracaoQueryParametersDTO contratacaoQueryParameters)
        {
            var query = _context.Contratacoes.AsNoTracking().AsQueryable();

            var quantidadeTotal = await query.CountAsync();

            int numeroPagina = contratacaoQueryParameters.NumeroPagina,
                tamanhoPagina = contratacaoQueryParameters.TamanhoPagina;

            var items = await query
                .Skip((numeroPagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return new PagedResult<ContratacaoSeguro>(items, quantidadeTotal, numeroPagina, tamanhoPagina);
        }

        public void Adicionar(ContratacaoSeguro proposta)
        {
            _context.Contratacoes.Add(proposta);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

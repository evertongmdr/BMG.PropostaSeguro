using BMG.Core.Data;
using BMG.Core.DTOs;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;

namespace BMG.Propostas.Domain.Interfaces.Repositories
{
    public interface IPropostaRepository : IRepository<Proposta>
    {
        Task<Proposta> ObterPorIdAsync(Guid id);
        Task<Proposta> ObterPorNumeroAsync(int numeroProposta);
        Task<PagedResult<Proposta>> ObterPropostasAsync(PropostaQueryParametersDTO propostaQueryParameters);
        void Adicionar(Proposta proposta);
        void Atualizar(Proposta proposta);
    }
}

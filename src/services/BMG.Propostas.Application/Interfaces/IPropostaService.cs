using BMG.Core.DTOs;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;

namespace BMG.Propostas.Application.Interfaces
{
    public interface IPropostaService
    {
        public Task<Proposta> ObterPropostaPorIdAsync(Guid id);
        public Task<PagedResult<Proposta>> ObterPropostasAsync(PropostaQueryParametersDTO propostaQueryParameters);
        public Task<Guid> CriarPropostaAsync(CriarPropostaDTO criarPropostaDTO);
        public Task AtualizarStatusPropostaAsync(Guid propostaId, AtualizarStatusPropostaDTO atualizarStatusPropostaDTO);
    }
}

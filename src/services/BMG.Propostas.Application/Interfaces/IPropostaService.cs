using BMG.Core.DTOs;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;

namespace BMG.Propostas.Application.Interfaces
{
    public interface IPropostaService
    {
        // TODO: REMOVER
        public Task<Proposta> ObterProposta(Guid id);
        public Task<PagedResult<Proposta>> ObterPropostas(PropostaQueryParametersDTO propostaQueryParameters);
        public Task<Guid> CriarProposta(CriarPropostaRequestDTO criarPropostaDTO);
        public Task AtualizarStatusProposta(Guid propostaid, AtualizarStatusPropostaDTO atualizarStatusPropostaDTO);
    }
}

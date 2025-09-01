using BMG.Core.DTOs;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;

namespace BMG.Propostas.Domain.Interfaces.Services
{
    public interface IPropostaService
    {
        // TODO: REMOVER
        public Task<Proposta> ObterProposta(Guid id);
        public Task<PagedResult<Proposta>> ObterPropostas(PropostaQueryParametersDTO propostaQueryParameters);
        public Guid CriarProposta(CriarPropostaDTO propostaDTO);
        public bool AtualizarStatusProposta(CriarPropostaDTO propostaDTO);

    }
}

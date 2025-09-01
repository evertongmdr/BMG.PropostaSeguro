using BMG.Contratacao.Domain.DTOs;
using BMG.Contratacao.Domain.Entities;
using BMG.Core.DTOs;

namespace BMG.Contratacao.Application.Interfaces
{
    public interface IContratacaoService
    {
        public Task<PagedResult<ContratacaoSeguro>> ObterContratacoesAsync(ContratacaoQueryParametersDTO contracaoQueryParameters);
        public Task ContratarPropostaAsync(CriarContratacaoDTO contratacao);
    }
}

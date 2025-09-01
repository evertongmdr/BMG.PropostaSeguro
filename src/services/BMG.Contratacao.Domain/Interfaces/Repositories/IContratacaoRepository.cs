using BMG.Contratacao.Domain.DTOs;
using BMG.Contratacao.Domain.Entities;
using BMG.Core.Data;
using BMG.Core.DTOs;

namespace BMG.Contratacao.Domain.Interfaces.Repositories
{
    public interface IContratacaoRepository : IRepository<ContratacaoSeguro>
    {
        Task<ContratacaoSeguro> ObterPorIdAsync(Guid id);

        Task<PagedResult<ContratacaoSeguro>> ObterContratacoesAsync(ContratacaoQueryParametersDTO contratacaoQueryParameters);
        void Adicionar(ContratacaoSeguro contratacao);
    }
}

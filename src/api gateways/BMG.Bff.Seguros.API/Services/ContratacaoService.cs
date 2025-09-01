using BMG.Bff.Seguros.Models.Contratacao;
using BMG.Core.DTOs;

namespace BMG.Bff.Seguros.Services
{
    public interface IContratacaoService
    {
        public Task<PagedResult<ContratacaoDTO>> ObterContratacoesAsync(ContratacaoQueryParametersDTO contracaoQueryParameters);
        public Task ContratarPropostaAsync(RegistrarContratacaoDTO contratacao);
    }

    public class ContratacaoService
    {

    }
}

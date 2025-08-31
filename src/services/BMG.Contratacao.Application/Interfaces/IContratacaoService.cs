using BMG.Contratacao.Domain.DTOs;

namespace BMG.Contratacao.Application.Interfaces
{
    public interface IContratacaoService
    {
        public Task ContratarPropostaAsync(CriarContratacaoRequestDTO contratacao);
    }
}

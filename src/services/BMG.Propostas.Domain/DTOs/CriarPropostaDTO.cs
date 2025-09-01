using BMG.Propostas.Domain.Entities;

namespace BMG.Propostas.Domain.DTOs
{
    public class CriarPropostaDTO
    {
        public int NumeroProposta { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid CriadoPorUsuarioId { get; set; }
    }
}

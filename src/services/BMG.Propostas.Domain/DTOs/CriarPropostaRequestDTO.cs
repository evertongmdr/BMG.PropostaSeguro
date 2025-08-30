using BMG.Propostas.Domain.Entities;

namespace BMG.Propostas.Domain.DTOs
{
    public class CriarPropostaRequestDTO
    {
        public string NumeroProposta { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid IdUsuarioCriou { get; set; }
    }
}

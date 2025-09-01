namespace BMG.Bff.Seguros.Models.Proposta
{
    public class PropostaDTO
    {
        public Guid Id { get; set; }
        public int NumeroProposta { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public PropostaStatus Status { get; set; }
        public Guid CriadoPorUsuarioId { get; set; }
    }

    public enum PropostaStatus
    {
        EmAnalise = 5,
        Aprovada = 10,
        Rejeitada = 15
    }
}

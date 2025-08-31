namespace BMG.Propostas.Domain.Entities
{
    public class Proposta
    {
        public Guid Id { get; set; }
        public int NumeroProposta { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public PropostaStatus Status { get; set; }
        public Guid CriadoPorUsuarioId { get; set; }
    }
}

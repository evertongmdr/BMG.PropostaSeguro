namespace BMG.Contratacao.Domain.Entities
{
    public class ContratacaoSeguro
    {
        public Guid Id { get; set; }
        public Guid PropostaId { get; set; }
        public Guid ContratenteId { get; set; }
        public DateTime DataContratacao { get; set; }
    }
}

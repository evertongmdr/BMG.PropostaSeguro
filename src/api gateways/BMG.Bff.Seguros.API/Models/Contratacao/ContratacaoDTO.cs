namespace BMG.Bff.Seguros.Models.Contratacao
{
    public class ContratacaoDTO
    {
        public Guid Id { get; set; }
        public Guid PropostaId { get; set; }
        public Guid ContratenteId { get; set; }
        public DateTime DataContratacao { get; set; }
    }
}

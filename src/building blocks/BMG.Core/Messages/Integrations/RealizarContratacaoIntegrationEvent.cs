namespace BMG.Core.Messages.Integrations
{
    public class RealizarContratacaoIntegrationEvent : IntegrationEvent
    {
        public Guid PropostaId { get; set; }
        public Guid ContratenteId { get; set; }
    }
}

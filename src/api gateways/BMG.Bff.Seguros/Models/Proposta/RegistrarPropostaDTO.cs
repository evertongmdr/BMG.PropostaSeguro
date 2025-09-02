namespace BMG.Bff.Seguros.Models.Proposta
{
    public class RegistrarPropostaDTO
    {

        public int NumeroProposta { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid CriadoPorUsuarioId { get; set; }
    }
}

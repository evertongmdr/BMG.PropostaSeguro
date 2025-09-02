using BMG.Core.DTOs;

namespace BMG.Bff.Seguros.Models.Proposta
{
    public class PropostaQueryParametersDTO : QueryParameters
    {
        public int? NumeroProposta { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
    }
}

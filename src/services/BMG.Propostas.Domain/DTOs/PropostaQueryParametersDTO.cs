using BMG.Core.DTOs;

namespace BMG.Propostas.Domain.DTOs
{
    public class PropostaQueryParametersDTO : QueryParameters
    {
        public int? NumeroProposta { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
    }

}

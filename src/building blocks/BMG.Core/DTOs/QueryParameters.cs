namespace BMG.Core.DTOs
{
    public class QueryParameters
    {
        const int TamanhoMaximoPagina = 16;
        public int NumeroPagina { get; set; } = 1;

        private int _tamanhoPagina = 16;

        public int TamanhoPagina
        {
            get => _tamanhoPagina;
            set => _tamanhoPagina = value > TamanhoMaximoPagina ? TamanhoMaximoPagina : value;
        }
    }
}

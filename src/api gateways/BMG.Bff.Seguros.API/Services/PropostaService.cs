using BMG.Bff.Seguros.Extensions;
using BMG.Bff.Seguros.Models.Proposta;
using BMG.Core.Communication;
using BMG.Core.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace BMG.Bff.Seguros.Services
{

    public interface IPropostaService
    {
        public Task<ApiResponse<PropostaDTO>> ObterPropostaPorIdAsync(Guid id);
        Task<ApiResponse<Guid>> CriarProposta(RegistrarPropostaDTO registrarProposta);
        public Task<ApiResponse<PagedResult<PropostaDTO>>> ObterPropostasAsync(PropostaQueryParametersDTO propostaQueryParameters);

    }

    public class PropostaService : Service, IPropostaService
    {
        private readonly HttpClient _httpClient;

        public PropostaService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PropostaUrl);
        }

        public async Task<ApiResponse<PropostaDTO>> ObterPropostaPorIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/proposta/{id}");

            return await CriarApiResponse<PropostaDTO>(response);
        }

        public async Task<ApiResponse<Guid>> CriarProposta(RegistrarPropostaDTO registrarProposta)
        {
            var registrarPropostaCotent = ObterConteudo(registrarProposta);

            var response = await _httpClient.PostAsync($"/proposta", registrarPropostaCotent);

            return await CriarApiResponse<Guid>(response);
        }

        public async Task<ApiResponse<PagedResult<PropostaDTO>>> ObterPropostasAsync(PropostaQueryParametersDTO propostaQueryParameters)
        {
            var fromQuery = $"tamanhoPagina={propostaQueryParameters.TamanhoPagina}&numeroPagina={propostaQueryParameters.NumeroPagina}";

            var queryParams = new Dictionary<string, string?>
            {
                ["NumeroPagina"] = propostaQueryParameters.NumeroPagina.ToString(),
                ["TamanhoPagina"] = propostaQueryParameters.TamanhoPagina.ToString(),
                ["NumeroProposta"] = propostaQueryParameters.NumeroProposta?.ToString(),
                ["Titulo"] = propostaQueryParameters.Titulo,
                ["Descricao"] = propostaQueryParameters.Descricao
            };

            var url = QueryHelpers.AddQueryString("/proposta/lista", queryParams);

            var response = await _httpClient.GetAsync(url);

            return await CriarApiResponse<PagedResult<PropostaDTO>>(response);
        }
    }
}

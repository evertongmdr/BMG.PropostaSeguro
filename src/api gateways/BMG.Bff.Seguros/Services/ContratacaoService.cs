using BMG.Bff.Seguros.Extensions;
using BMG.Bff.Seguros.Models.Contratacao;
using BMG.Bff.Seguros.Models.Proposta;
using BMG.Core.Communication;
using BMG.Core.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace BMG.Bff.Seguros.Services
{
    public interface IContratacaoService
    {
        public Task<ApiResponse<PagedResult<ContratacaoDTO>>> ObterContratacoesAsync(ContratacaoQueryParametersDTO contracaoQueryParameters);
        public Task<ApiResponse> ContratarPropostaAsync(RegistrarContratacaoDTO registrarContratacao);
    }

    public class ContratacaoService : Service, IContratacaoService
    {
        private readonly HttpClient _httpClient;

        public ContratacaoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ContratacaoUrl);
        }
        public async Task<ApiResponse> ContratarPropostaAsync(RegistrarContratacaoDTO registrarContratacao)
        {
            var registrarContratacaoCotent = ObterConteudo(registrarContratacao);

            var response = await _httpClient.PostAsync($"/contratacao", registrarContratacaoCotent);

            return await CriarApiResponse(response);
        }

        public async Task<ApiResponse<PagedResult<ContratacaoDTO>>> ObterContratacoesAsync(ContratacaoQueryParametersDTO contracaoQueryParameters)
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["NumeroPagina"] = contracaoQueryParameters.NumeroPagina.ToString(),
                ["TamanhoPagina"] = contracaoQueryParameters.TamanhoPagina.ToString(),
            };

            var url = QueryHelpers.AddQueryString("/contratacao/lista", queryParams);

            var response = await _httpClient.GetAsync(url);

            return await CriarApiResponse<PagedResult<ContratacaoDTO>>(response);
        }
    }
}

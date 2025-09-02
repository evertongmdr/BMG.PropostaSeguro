using BMG.Bff.Seguros.Extensions;
using BMG.Bff.Seguros.Models.Identidade;
using BMG.Bff.Seguros.Models.Proposta;
using BMG.Core.Communication;
using BMG.Core.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace BMG.Bff.Seguros.Services
{

    public interface IIdentidadeService
    {
        Task<ApiResponse<UsuarioDTO>> ObterUsuarioPorIdAsync(Guid id);
        Task<ApiResponse<Guid>> RegistrarUsuario(RegistrarUsuarioDTO registrarUsuario);
        Task<ApiResponse<PagedResult<UsuarioDTO>>> ObterUsuariosAsync(UsuarioQueryParametersDTO usuarioQueryParameters);
    }

    public class IdentidadeService : Service, IIdentidadeService
    {
        private readonly HttpClient _httpClient;

        public IdentidadeService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.IdentidadeUrl);
        }

        public async Task<ApiResponse<UsuarioDTO>> ObterUsuarioPorIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/identidade/usuario/{id}");

            return await CriarApiResponse<UsuarioDTO>(response);
        }

        public async Task<ApiResponse<Guid>> RegistrarUsuario(RegistrarUsuarioDTO registrarUsuario)
        {
            var registrarUsuarioCotent = ObterConteudo(registrarUsuario);

            var response = await _httpClient.PostAsync($"/identidade/nova-conta", registrarUsuarioCotent);

            return await CriarApiResponse<Guid>(response);
        }

        public async Task<ApiResponse<PagedResult<UsuarioDTO>>> ObterUsuariosAsync(UsuarioQueryParametersDTO usuarioQueryParameters)
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["NumeroPagina"] = usuarioQueryParameters.NumeroPagina.ToString(),
                ["TamanhoPagina"] = usuarioQueryParameters.TamanhoPagina.ToString(),
            };

            var url = QueryHelpers.AddQueryString("/identidade/usuario/lista", queryParams);

            var response = await _httpClient.GetAsync(url);

            return await CriarApiResponse<PagedResult<UsuarioDTO>>(response);
        }
    }
}

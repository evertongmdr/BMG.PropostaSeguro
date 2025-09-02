using BMG.Core.Communication;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BMG.Bff.Seguros.Services
{
    public abstract class Service
    {
        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest 
                || response.StatusCode == HttpStatusCode.NotFound) return false;

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected async Task<ApiResponse<T>> CriarApiResponse<T>(HttpResponseMessage response)
        {
            if (!TratarErrosResponse(response))
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            var data = await DeserializarObjetoResponse<T>(response);

            return new ApiResponse<T>
            {
                Success = true,
                Data = data
            };
        }

        protected async Task<ApiResponse> CriarApiResponse(HttpResponseMessage response)
        {
            var sucesso = TratarErrosResponse(response);

            return new ApiResponse
            {
                Success = sucesso,
                ResponseResult = sucesso
                    ? null
                    : await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }


        protected ResponseResult RetornoOk()
        {
            return new ResponseResult();
        }
    }
}

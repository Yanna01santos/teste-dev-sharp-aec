using AEC.Models;
using System.Net.Http.Json;

namespace AEC.Services
{
    public class ViaCepService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ViaCepService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ViaCepResposta?> BuscarEnderecoPorCepAsync(string cep)
        {
            cep = new string(cep.Where(char.IsDigit).ToArray());

            if (cep.Length != 8)
            {
                return null;
            }

            var client = _httpClientFactory.CreateClient();

            var resultado = await client.GetFromJsonAsync<ViaCepResposta>(
                $"https://viacep.com.br/ws/{cep}/json/"
            );

            if (resultado == null || resultado.Erro)
            {
                return null;
            }

            return resultado;
        }
    }
}
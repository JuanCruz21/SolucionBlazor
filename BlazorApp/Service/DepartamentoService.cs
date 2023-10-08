using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorApp.Service
{
    public class DepartamentoService :IDepartamentoServices
    {
        private readonly HttpClient _http;

        public DepartamentoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<DepartamentoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<DepartamentoDTO>>>("api/Departamento/Lista");
            if (result!.Exito == 1)
            {
                return result.Datos;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
    }
}
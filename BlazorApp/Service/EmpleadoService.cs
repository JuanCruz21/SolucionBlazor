using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorApp.Service
{
    public class EmpleadoService :IEmpleadosServer
    {
        private readonly HttpClient _http;
        public EmpleadoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<int> Actualizar(EmpleadoDTO empleado)
        {
            var result = await _http.PutAsJsonAsync($"api/Empleados/Actuualizar/{empleado.IdEmpleado}",empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<EmpleadoDTO>>();

            if (response.Exito == 1)
            {
                return response.Datos.IdEmpleado;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<EmpleadoDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<EmpleadoDTO>>("api/Empleados/buscar");

            if (result.Exito == 1)
            {
                return result.Datos;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        public async Task<int> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Empleados/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<EmpleadoDTO>>();

            if (response.Exito == 1)
            {
                return response.Exito;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<int> Guardar(EmpleadoDTO empleado)
        {
            var result = await _http.PostAsJsonAsync("api/Empleados/Guardar",empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();
            if (response.Exito == 1)
            {
                return response.Datos;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<List<EmpleadoDTO>> Listar()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<EmpleadoDTO>>>("api/Empleados/lista");

            if (result.Exito == 1)
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

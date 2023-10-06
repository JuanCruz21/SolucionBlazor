using BlazorCrud.Shared;

namespace BlazorApp.Service
{
    public interface IEmpleadosServer
    {
        Task<List<EmpleadoDTO>> Listar();
        Task<EmpleadoDTO> Buscar(int id);
        Task<int> Actualizar(EmpleadoDTO empleado);
        Task<int> Eliminar(int id);
        Task<int> Guardar(EmpleadoDTO empleado);  
    }
}

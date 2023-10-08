using BlazorCrud.Shared;

namespace BlazorApp.Service
{
    public interface IDepartamentoServices
    {
        Task<List<DepartamentoDTO>>Lista();
    }
}
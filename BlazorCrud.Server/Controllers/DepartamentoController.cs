using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly CrudblazorContext _dbContext;

        public DepartamentoController(CrudblazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseApi<List<DepartamentoDTO>>();
            var listaDepartamentoDTO = new List<DepartamentoDTO>();
            try
            {
                foreach(var item in await _dbContext.Departamentos.ToListAsync())
                {
                    listaDepartamentoDTO.Add(new DepartamentoDTO
                    {
                        IdDepartamento = item.IdDepartamento,
                        Nombre = item.Nombre
                    });
                    responseApi.Exito = true;
                    responseApi.Datos = listaDepartamentoDTO;
                }
            } catch (Exception ex)
            {
                responseApi.Exito = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

    }
}

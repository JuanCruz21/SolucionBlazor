using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly CrudblazorContext _dbContext;

        public EmpleadosController(CrudblazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseApi<List<EmpleadoDTO>>();
            var listaEmpleadosDTO = new List<EmpleadoDTO>();
            try
            {
                foreach(var item in await _dbContext.Empleados.Include(d=>d.IdDepartamentoNavigation).ToListAsync())
                {
                    listaEmpleadosDTO.Add(new EmpleadoDTO
                    {
                        IdEmpleado  = item.IdEmpleado,
                        NombreCompleto = item.NombreCompleto,
                        IdDepartamento = item.IdDepartamento,
                        FechaContrato = item.FechaContrato,
                        Sueldo = item.Sueldo,
                        Departamento = new DepartamentoDTO
                        {
                            IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
                            Nombre = item.IdDepartamentoNavigation.Nombre
                        }
                        
                    });
                    responseApi.Exito = 1;
                    responseApi.Datos = listaEmpleadosDTO;
                }
            } 
            catch (Exception ex)
            {
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseApi<EmpleadoDTO>();
            var empleadoDTO = new EmpleadoDTO();
            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(x=>x.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    empleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
                    empleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
                    empleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;
                    empleadoDTO.FechaContrato = dbEmpleado.FechaContrato;
                    empleadoDTO.Sueldo = dbEmpleado.Sueldo;

                    responseApi.Exito = 1;
                    responseApi.Datos = empleadoDTO;
                }
                else
                {
                    responseApi.Exito = 0;
                    responseApi.Message = "No encontrado";
                }
                    
            }
            catch (Exception ex)
            {
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(EmpleadoDTO empleado)
        {
            var responseApi = new ResponseApi<int>();
            try
            {
                var dbEmpleado = new Empleado
                {
                    FechaContrato = empleado.FechaContrato,
                    NombreCompleto = empleado.NombreCompleto,
                    IdDepartamento = empleado.IdDepartamento,
                    Sueldo = empleado.Sueldo
                };
                _dbContext.Empleados.Add(dbEmpleado);
                await _dbContext.SaveChangesAsync();
                if (dbEmpleado.IdEmpleado != 0)
                {
                    responseApi.Exito = 1;
                    responseApi.Datos = dbEmpleado.IdEmpleado;
                }
                else 
                { 
                    responseApi.Exito = 0; 
                    responseApi.Message = "No se guardo"; 
                }
            }
            catch (Exception ex)
            {
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(EmpleadoDTO empleado, int id)
        {
            var responseApi = new ResponseApi<int>();
            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e=>e.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    dbEmpleado.NombreCompleto = empleado.NombreCompleto;
                    dbEmpleado.FechaContrato = empleado.FechaContrato;
                    dbEmpleado.Sueldo = empleado.Sueldo;
                    dbEmpleado.IdDepartamento = empleado.IdDepartamento;
                    _dbContext.Empleados.Update(dbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    responseApi.Exito = 1;
                    responseApi.Message = "Se actualizo";
                    responseApi.Datos = dbEmpleado.IdEmpleado;
                }
                else
                {
                    responseApi.Exito = 0;
                    responseApi.Message = "No se guardo";
                }
            }
            catch (Exception ex)
            {
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseApi<int>();
            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

                if (dbEmpleado != null)
                {

                    _dbContext.Empleados.Remove(dbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    responseApi.Exito = 1;
                    responseApi.Message = "Se Elimino correctamente";
                    responseApi.Datos = dbEmpleado.IdEmpleado;
                }
                else
                {
                    responseApi.Exito = 0;
                    responseApi.Message = "No se elimino, porqué no se encontro el empleado";
                }
            }
            catch (Exception ex)
            {
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }
    }
}

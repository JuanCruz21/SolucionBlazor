using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }
        [Required(ErrorMessage ="El campo es {0} requerido")]
        public string NombreCompleto { get; set; } = null!;
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "El campo es {0} requerido")]
        public int IdDepartamento { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo es {0} requerido")]
        public int Sueldo { get; set; }
        
        public DateTime FechaContrato { get; set; }

        public DepartamentoDTO? Departamento { get; set; }
    }
}

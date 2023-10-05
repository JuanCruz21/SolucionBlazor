using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class ResponseApi<T>
    {
        public int Exito { get; set;}
        public T Datos { get; set;}
        public string? Message { get; set;}
        public ResponseApi() 
        {
            Exito = 0;
        }
    }
}

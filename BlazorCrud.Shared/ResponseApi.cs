﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class ResponseApi<T>
    {
        public bool Exito { get; set;} = false;
        public T? Datos { get; set;}
        public string? Message { get; set;}
    }
}

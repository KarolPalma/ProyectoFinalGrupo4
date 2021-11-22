using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalGrupo4.Models
{
    class Descuentos
    {
        public int idDescuento { get; set; }
        public string nombreDescuento { get; set; }
        public double porcentajeDescuento { get; set; }
        public string descripcion => nombreDescuento + " (" + Math.Truncate(porcentajeDescuento) + "%)";
    }
}

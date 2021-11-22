using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalGrupo4.Models
{
    class Impuestos
    {
        public int idImpuesto { get; set; }
        public string nombreImpuesto { get; set; }
        public double porcentajeImpuesto { get; set; }
        public string descripcion => nombreImpuesto + " (" + Math.Truncate(porcentajeImpuesto) + "%)";
    }
}

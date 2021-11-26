using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalGrupo4.Models
{
    class Facturas
    {
        public int idFactura { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public int idPersona { get; set; }
        public string nombre { get; set; }
        public int idUsuario { get; set; }
        public string correo { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string metodoPago { get; set; }
        public double subtotal { get; set; }
        public double total { get; set; }
    }
}

using System;

namespace ProyectoFinalGrupo4.Models
{
    class Bitacora
    {
        public int idRegistro { get; set; }
        public string usuario { get; set; }
        public string tipoDispositivo { get; set; }
        public string sistemaOperativo { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string detalle { get; set; }

        //public DateTime fecha2 => fecha + " (" + Convert.ToDateTime(fecha) + ")";
    }
}

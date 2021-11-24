using System;

namespace ProyectoFinalGrupo4.Models
{
    class Descuentos
    {
        public int idDescuento { get; set; }
        public string nombreDescuento { get; set; }
        public double valorDescuento { get; set; }
        public double porcentajeDescuento { get; set; }
        public string descripcion => nombreDescuento + " (" + Math.Truncate(porcentajeDescuento) + "%)";

        public Descuentos()
        {
        }

        public Descuentos(string nombreDescuento, double valorDescuento)
        {
            this.nombreDescuento = nombreDescuento;
            this.valorDescuento = valorDescuento;
        }

        public Descuentos(int idDescuento, string nombreDescuento, double valorDescuento)
        {
            this.idDescuento = idDescuento;
            this.nombreDescuento = nombreDescuento;
            this.valorDescuento = valorDescuento;
        }
    }
}

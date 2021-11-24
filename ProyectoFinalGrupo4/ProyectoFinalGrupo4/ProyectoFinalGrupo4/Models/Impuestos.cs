using System;

namespace ProyectoFinalGrupo4.Models
{
    class Impuestos
    {
        public int idImpuesto { get; set; }
        public string nombreImpuesto { get; set; }
        public double valorImpuesto { get; set; }
        public double porcentajeImpuesto { get; set; }
        public string descripcion => nombreImpuesto + " (" + Math.Truncate(porcentajeImpuesto) + "%)";

        public Impuestos()
        {
        }

        public Impuestos(string nombreImpuesto, double valorImpuesto)
        {
            this.nombreImpuesto = nombreImpuesto;
            this.valorImpuesto = valorImpuesto;
        }

        public Impuestos(int idImpuesto, string nombreImpuesto, double valorImpuesto)
        {
            this.idImpuesto = idImpuesto;
            this.nombreImpuesto = nombreImpuesto;
            this.valorImpuesto = valorImpuesto;
        }
    }
}

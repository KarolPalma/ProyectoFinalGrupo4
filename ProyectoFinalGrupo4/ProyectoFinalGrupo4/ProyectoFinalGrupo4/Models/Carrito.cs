using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalGrupo4.Models
{
    class Carrito
    {
        public int idUsuario { get; set; }
        public int idProducto { get; set; }
        public int unidadesLlevadas { get; set; }
        public int idDescuento { get; set; }

        public Carrito(int idUsuario, int idProducto, int unidadesLlevadas, int idDescuento)
        {
            this.idUsuario = idUsuario;
            this.idProducto = idProducto;
            this.unidadesLlevadas = unidadesLlevadas;
            this.idDescuento = idDescuento;
        }
    }
}

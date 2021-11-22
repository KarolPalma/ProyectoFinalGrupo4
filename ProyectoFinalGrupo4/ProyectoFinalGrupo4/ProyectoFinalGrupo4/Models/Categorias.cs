using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ProyectoFinalGrupo4.Models
{
    public class Categorias
    {
        public int idCategoria { get; set; }
        public string categoria { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
    }
}